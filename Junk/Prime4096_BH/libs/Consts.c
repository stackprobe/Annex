#include "all.h"

#define D_VAR(bit) \
	UI ## bit ## _t UI ## bit ## _N0; \
	UI ## bit ## _t UI ## bit ## _N1; \
	UI ## bit ## _t UI ## bit ## _N2; \
	UI ## bit ## _t UI ## bit ## _N3;

D_VAR(64)
D_VAR(128)
D_VAR(256)
D_VAR(512)
D_VAR(1024)
D_VAR(2048)
D_VAR(4096)

#undef D_VAR

UI4096_t UI4096_N10P9;
UI4096_t UI4096_N2P2048;

void Consts_INIT(void)
{
#define D_INIT(bit) \
	UI ## bit ## _N0 = UI ## bit ## _x(0); \
	UI ## bit ## _N1 = UI ## bit ## _x(1); \
	UI ## bit ## _N2 = UI ## bit ## _x(2); \
	UI ## bit ## _N3 = UI ## bit ## _x(3);

	D_INIT(64)
	D_INIT(128)
	D_INIT(256)
	D_INIT(512)
	D_INIT(1024)
	D_INIT(2048)
	D_INIT(4096)

#undef D_INIT

	UI4096_N10P9 = UI4096_x(1000000000);

	UI4096_N2P2048 = UI4096_N0;
	UI4096_N2P2048.H.L.L.L.L.L.L.Value = 1;
}
