#include <wincrypt.h>
#pragma comment(lib, "ADVAPI32")

void createKeyContainer(void);
void deleteKeyContainer(void);
void getCryptoBlock_MS(uchar *buffer, int size);

int getCryptoByte(void);
int getCryptoRand16(void);
int getCryptoRand24(void);
int getCryptoRand(void);
void getCryptoBlock(uchar *block, int size);

char *makePw(int radix = 62, int len = 22); // log62(2^128) == 21�`22

// log36(2^128) == 24�`25
#define makePw36() (makePw(36, 25))

// log10(2^128) == 38�`39
#define makePw10() (makePw(10, 39))
