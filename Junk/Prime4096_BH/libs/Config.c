#include "all.h"

// ---- Items ----

uint MillerRabin_K;

// ----

void LoadConfig(void)
{
	autoList_t *lines = readResourceLines_x(changeExt(getSelfFile(), "conf"));
	uint c = 0;

	// ---- Items ----

	MillerRabin_K = toValue(getLine(lines, c++));

	// ----

	errorCase(strcmp("\\e", getLine(lines, c++)));

	// ---- Check Items ----

	errorCase(!m_isRange(MillerRabin_K, 1, IMAX));

	// ----

	releaseDim(lines, 1);
}
