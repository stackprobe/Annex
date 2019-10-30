#include "all.h"

// ---- Items ----

uint64 MillerRabin_Lmt;
uint   MillerRabin_K;

// ----

void LoadConfig(void)
{
	autoList_t *lines = readResourceLines_x(changeExt(getSelfFile(), "conf"));
	uint c = 0;

	// ---- Items ----

	MillerRabin_Lmt = toValue64(getLine(lines, c++));
	MillerRabin_K   = toValue  (getLine(lines, c++));

	// ----

	errorCase(strcmp("\\e", getLine(lines, c++)));

	// ---- Check Items ----

	// MillerRabin_Lmt
	errorCase(!m_isRange(MillerRabin_K, 1, IMAX));

	// ----

	releaseDim(lines, 1);
}
