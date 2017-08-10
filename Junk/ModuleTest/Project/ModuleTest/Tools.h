int getZero(void);

template<class Element_t>
void gnomeSort(autoList<Element_t> *list, int (*comp)(Element_t, Element_t))
{
	for(int rndx = 1; rndx < list->GetCount(); rndx++)
		for(int lndx = rndx; lndx && 0 < comp(list->GetElement(lndx - 1), list->GetElement(lndx)); lndx--)
			list->Swap(lndx - 1, lndx);
}
template<class Element_t>
void combSort(autoList<Element_t> *list, int (*comp)(Element_t, Element_t))
{
	int span = list->GetCount();

	for(; ; )
	{
		span *= 10;
		span /= 13;

		if(span < 2)
			break;

		if(span == 9 || span == 10)
			span = 11;

		for(int lidx = 0, ridx = span; ridx < list->GetCount(); lidx++, ridx++)
			if(0 < comp(list->GetElement(lidx), list->GetElement(ridx)))
				list->Swap(lidx, ridx);
	}
	gnomeSort(list, comp);
}
