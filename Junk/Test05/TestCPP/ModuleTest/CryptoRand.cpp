#include "all.h"

void createKeyContainer(void)
{
	HCRYPTPROV hp;

	if(CryptAcquireContext(&hp, 0, 0, PROV_RSA_FULL, CRYPT_NEWKEYSET)) // エラー無視
		CryptReleaseContext(hp, 0);
}
void deleteKeyContainer(void)
{
	HCRYPTPROV hp;

	CryptAcquireContext(&hp, 0, 0, PROV_RSA_FULL, CRYPT_DELETEKEYSET); // エラー無視
}
void getCryptoBlock_MS(uchar *buffer, int size)
{
	HCRYPTPROV hp;

	if(!CryptAcquireContext(&hp, 0, 0, PROV_RSA_FULL, 0) &&
		(GetLastError() != NTE_BAD_KEYSET ||
		!CryptAcquireContext(&hp, 0, 0, PROV_RSA_FULL, CRYPT_NEWKEYSET)))
		error();

	if(!CryptGenRandom(hp, size, buffer))
	{
		CryptReleaseContext(hp, 0);
		error();
	}
	CryptReleaseContext(hp, 0);
}

#define SEEDSIZE 4096
#define BUFFERSIZE 64 // == sha512 hash size

static void GetCryptoBlock(uchar *buffer)
{
	static sha512_t *ctx;

	sha512_evacuate();

	if(!ctx)
	{
		uchar seed[SEEDSIZE];

		getCryptoBlock_MS(seed, SEEDSIZE);

		ctx = sha512_create();
		sha512_update(ctx, seed, SEEDSIZE);
		sha512_makeHash(ctx);
	}
	else
	{
		static uint counter;
		sha512_t *ictx = sha512_copy(ctx);

		sha512_update(ictx, &counter, sizeof(counter));
		sha512_makeHash(ictx);

		counter++;

		if(!counter) // ? カンスト
		{
			sha512_release(ctx);
			ctx = ictx;
		}
		else
			sha512_release(ictx);
	}
	memcpy(buffer, sha512_hash, BUFFERSIZE);
	sha512_unevacuate();
}
int getCryptoByte(void)
{
	static uchar buffer[BUFFERSIZE];
	static uint index = BUFFERSIZE;

	if(BUFFERSIZE <= index)
	{
		GetCryptoBlock(buffer);
		index = 0;
	}
	return buffer[index++];
}
int getCryptoRand16(void)
{
	return getCryptoByte() | getCryptoByte() << 8;
}
int getCryptoRand24(void)
{
	return getCryptoByte() | getCryptoByte() << 8 | getCryptoByte() << 16;
}
int getCryptoRand(void)
{
	return getCryptoByte() | getCryptoByte() << 8 | getCryptoByte() << 16 | getCryptoByte() << 24;
}
void getCryptoBlock(uchar *block, int size)
{
	for(int index = 0; index < size; index++)
	{
		block[index] = getCryptoByte();
	}
}

char *makePw(int radix, int len) // radix: 1 - 62
{
	char *pw = (char *)memAlloc(len + 1);
	int modulo = 256 - 256 % radix;

	for(int index = 0; index < len; index++)
	{
		int chr;
		do {
			chr = getCryptoByte();
		}
		while(modulo <= chr);
		chr %= radix;

		if(36 <= chr)
			chr += 'A' - 36;
		else if(10 <= chr)
			chr += 'a' - 10;
		else
			chr += '0';

		pw[index] = chr;
	}
	pw[len] = '\0';
	return pw;
}
