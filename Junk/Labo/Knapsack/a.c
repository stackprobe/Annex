#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\bitList.h"

static uint WeightMax;
static autoList_t *Weights;
static autoList_t *Prices;
static autoList_t *Combi;
static autoList_t *MaxCombi;
static uint MaxPrice;

#if 0
static void Next(uint index, uint ww, uint pp)
{
	if(index < getCount(Weights))
	{
		uint w = getElement(Weights, index);
		uint p = getElement(Prices,  index);

		if(ww + w <= WeightMax)
		{
			addElement(Combi, index);
			Next(index + 1, ww + w, pp + p);
			unaddElement(Combi);
		}
		Next(index + 1, ww, pp);
	}
	else if(MaxPrice < pp)
	{
		releaseAutoList(MaxCombi);
		MaxCombi = copyAutoList(Combi);
		MaxPrice = pp;
	}
}
static void Search(void)
{
	Next(0, 0, 0);
}
#else
static void Search(void)
{
	bitList_t *selects = newBitList();
	int forward = 1;
	uint index = 0;
	uint ww = 0;
	uint pp = 0;

	for(; ; )
	{
		if(forward)
		{
			if(index < getCount(Weights))
			{
				uint w = getElement(Weights, index);
				uint p = getElement(Prices,  index);

				if(ww + w <= WeightMax)
				{
					addElement(Combi, index);
					putBit(selects, index, 1);
					index++;
					ww += w;
					pp += p;
				}
				else
				{
					putBit(selects, index, 0);
					index++;
				}
			}
			else
			{
				if(MaxPrice < pp)
				{
					releaseAutoList(MaxCombi);
					MaxCombi = copyAutoList(Combi);
					MaxPrice = pp;
				}
				forward = 0;
				index--;
			}
		}
		else // !forward
		{
			if(refBit(selects, index))
			{
				uint w = getElement(Weights, index);
				uint p = getElement(Prices,  index);

				unaddElement(Combi);
				putBit(selects, index, 0);
				forward = 1;
				index++;
				ww -= w;
				pp -= p;
			}
			else
			{
				if(!index)
					break;

				index--;
			}
		}
	}
	releaseBitList(selects);
}
#endif
static void Main2(void)
{
	WeightMax = toValue(nextArg());
	Weights  = newList();
	Prices   = newList();
	Combi    = newList();
	MaxCombi = newList();
	MaxPrice = 0;

	while(hasArgs(1))
	{
		addElement(Weights, toValue(nextArg()));
		addElement(Prices,  toValue(nextArg()));

		errorCase(IMAX < getLastElement(Weights));
		errorCase(IMAX < getLastElement(Prices));
	}

	{
		uint index;

		cout("WeightMax: %u\n", WeightMax);

		for(index = 0; index < getCount(Weights); index++)
		{
			uint w = getElement(Weights, index);
			uint p = getElement(Prices,  index);

			cout("Weight Price: %u %u\n", w, p);
		}
	}

	Search();

	{
		uint index;
		uint ww = 0;

		cout("\n");
		cout("MaxCombi...\n");

		for(index = 0; index < getCount(MaxCombi); index++)
		{
			uint w = getElement(Weights, getElement(MaxCombi, index));
			uint p = getElement(Prices,  getElement(MaxCombi, index));

			cout("Weight Price: %u %u\n", w, p);

			ww += w;
		}
		cout("----\n");
		cout("Weight Price Total: %u %u\n", ww, MaxPrice);
	}
}
int main(int argc, char **argv)
{
	Main2();
}
