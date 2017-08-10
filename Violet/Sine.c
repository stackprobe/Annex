#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	double red;

	for(red = 0.0; red < 1.5; red += 0.01)
		cout("sin( %f ) = %f\n", red, sin(red));
}
