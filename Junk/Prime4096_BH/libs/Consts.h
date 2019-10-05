#define D_PRO(bit) \
	extern UI ## bit ## _t UI ## bit ## _N0; \
	extern UI ## bit ## _t UI ## bit ## _N1; \
	extern UI ## bit ## _t UI ## bit ## _N2; \
	extern UI ## bit ## _t UI ## bit ## _N3;

D_PRO(64)
D_PRO(128)
D_PRO(256)
D_PRO(512)
D_PRO(1024)
D_PRO(2048)
D_PRO(4096)

#undef D_PRO

extern UI4096_t UI4096_N10P9;
extern UI4096_t UI4096_N2P2048;

void Consts_INIT(void);
