#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\bitList.h"
#include "C:\Factory\Common\Options\Sequence.h"

static uint WeightMax;
static autoList_t *Weights;
static autoList_t *Prices;
static autoList_t *MaxCombi;

static sint Comp_PricePerWeight(uint a, uint b)
{
	if(getElement(Weights, a) && getElement(Weights, b))
	{
		double va = (double)getElement(Prices, a) / getElement(Weights, a);
		double vb = (double)getElement(Prices, b) / getElement(Weights, b);

		return m_simpleComp(va, vb);
	}
	else if(getElement(Weights, a)) // b is w=0
	{
		return -1; // a < b
	}
	else if(getElement(Weights, b)) // a is w=0
	{
		return 1; // a > b
	}
	else
	{
		uint va = getElement(Prices, a);
		uint vb = getElement(Prices, b);

		return m_simpleComp(va, vb);
	}
}
static void Search(void)
{
	autoList_t *indexes = createSq(getCount(Weights), 0, 1);
	uint index;
	uint index_index;
	uint ww = 0;

	rapidSort(indexes, Comp_PricePerWeight);
	reverseElements(indexes);

	foreach(indexes, index, index_index)
	{
		ww += getElement(Weights, index);

		if(WeightMax < ww)
			break;

		addElement(MaxCombi, index);
	}
	releaseAutoList(indexes);
}
static void Main2(void)
{
	WeightMax = toValue(nextArg());
	Weights  = newList();
	Prices   = newList();
	MaxCombi = newList();

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
		uint pp = 0;

		cout("\n");
		cout("MaxCombi...\n");

		for(index = 0; index < getCount(MaxCombi); index++)
		{
			uint w = getElement(Weights, getElement(MaxCombi, index));
			uint p = getElement(Prices,  getElement(MaxCombi, index));

			cout("Weight Price: %u %u\n", w, p);

			ww += w;
			pp += p;
		}
		cout("----\n");
		cout("Weight Price Total: %u %u\n", ww, pp);
	}
}
int main(int argc, char **argv)
{
	Main2();
}
