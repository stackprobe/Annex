/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
class bitList
{
private:
	autoList<uint> *List;

public:
	bitList()
	{
		this->List = new autoList<uint>();
	}
	bitList(const bitList &source)
	{
		error();
	}
	~bitList()
	{
		delete this->List;
	}

	void Clear()
	{
		this->List->Clear();
	}

	void PutBit(int index, int value)
	{
		errorCase(index < 0);
		uint c = this->List->RefElement(index / 32, 0);

		if(value)
			c |= 1 << (index % 32);
		else
			c &= ~(1 << (index % 32));

		this->List->PutElement(index / 32, c, 0);
	}
	int RefBit(int index)
	{
		errorCase(index < 0);
		return (this->List->RefElement(index / 32, 0) >> (index % 32)) & 1;
	}

	int GetFinalBitPos() // ret: 最後の 1 になっているビットの位置, 無い場合は -1
	{
		int index;

		for(index = this->List->GetCount() * 32 - 1; 0 < index; index--)
			if(this->RefBit(index))
				break;

		return index;
	}

	autoList<uint> *GetList_DIRECT()
	{
		return this->List;
	}
	void SetList_DIRECT(autoList<uint> *list)
	{
		errorCase(!list);
		delete this->List;
		this->List = list;
	}
	void Overwrite(bitList *src)
	{
		errorCase(!src);
		this->List->Overwrite(src->List);
	}
	void Overwrite_DIRECT(autoList<uint> *list)
	{
		errorCase(!list);
		this->List->Overwrite(list);
	}
};
