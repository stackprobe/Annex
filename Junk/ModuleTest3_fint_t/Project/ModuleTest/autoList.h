template <class Element_t>
class autoList
{
private:
	int Count;
	int ListSize;
	Element_t *List;

public:
	autoList()
	{
		this->Count = 0;
		this->ListSize = 0;
		this->List = (Element_t *)memAlloc(0);
	}
	autoList(int buffer_size)
	{
		errorCase(buffer_size < 0 || INT_MAX / sizeof(Element_t) < buffer_size);

		this->Count = 0;
		this->ListSize = buffer_size;
		this->List = (Element_t *)memAlloc(buffer_size * sizeof(Element_t));
	}
	autoList(Element_t *list_bind, int count)
	{
		errorCase(!list_bind);
		errorCase(count < 0 || INT_MAX < count);

		this->Count = count;
		this->ListSize = count;
		this->List = list_bind;
	}
	autoList(const autoList &source)
	{
		error();
	}
	~autoList()
	{
		memFree(this->List);
	}

	autoList<Element_t> *GetClone()
	{
		autoList<Element_t> *list_ret = new autoList<Element_t>();

		list_ret->Count = this->Count;
		list_ret->ListSize = this->Count;
		list_ret->List = (Element_t *)memClone(this->List, this->Count * sizeof(Element_t));

		return list_ret;
	}
	Element_t *UnbindBuffer()
	{
		Element_t *list_ret = this->List;

		this->Count = 0;
		this->ListSize = 0;
		this->List = (Element_t *)memAlloc(0);

		return list_ret;
	}

	void Clear()
	{
		this->Count = 0;
	}
	int GetCount()
	{
		return this->Count;
	}

	void SetElement(int index, Element_t element)
	{
		errorCase(index < 0 || this->Count <= index);

		this->List[index] = element;
	}
	Element_t GetElement(int index)
	{
		errorCase(index < 0 || this->Count <= index);

		return this->List[index];
	}
	Element_t *ElementAt(int index)
	{
		if(index == 0)
			return this->List;

		errorCase(index < 0 || this->Count <= index);

		return this->List + index;
	}

	void AddElement(Element_t element)
	{
		if(this->ListSize <= this->Count)
		{
			errorCase((INT_MAX / sizeof(Element_t)) / 2 < this->ListSize);

			if(this->ListSize < 16)
				this->ListSize += 2;
			else
				this->ListSize *= 2;

			this->List = (Element_t *)memRealloc(this->List, this->ListSize * sizeof(Element_t));
		}
		this->List[this->Count] = element;
		this->Count++;
	}
	Element_t UnaddElement()
	{
		errorCase(this->Count < 1);

		this->Count--;
		return this->List[this->Count];
	}
	void InsertElement(int index, Element_t element)
	{
		errorCase(index < 0 || this->Count < index);

		this->AddElement(element); // dummy

		for(int i = this->Count - 1; index < i; i--)
		{
			this->List[i] = this->List[i - 1];
		}
		this->List[index] = element;
	}
	Element_t DesertElement(int index)
	{
		errorCase(index < 0 || this->Count <= index);

		Element_t element = this->List[index];

		this->Count--;

		for(int i = index; i < this->Count; i++)
		{
			this->List[i] = this->List[i + 1];
		}
		return element;
	}
	Element_t FastDesertElement(int index)
	{
		errorCase(index < 0 || this->Count <= index);

		Element_t element = this->List[index];

		this->Count--;
		this->List[index] = this->List[this->Count];

		return element;
	}
	void RemoveElements(int start, int count)
	{
		errorCase(start < 0 || this->Count < start);
		errorCase(count < 0 || this->Count - start < count);

		int index;

		for(index = start; index + count < this->Count; index++)
		{
			this->List[index] = this->List[index + count];
		}
		this->Count = index;
	}

	void CallAllElement(void (*func)(Element_t e))
	{
		for(int index = 0; index < this->Count; index++)
		{
			func(this->GetElement(index));
		}
	}
	void Clear(void (*func)(Element_t e))
	{
		this->CallAllElement(func);
		this->Clear();
	}

	void AddElements(Element_t *list, int count)
	{
		for(int index = 0; index < count; index++)
		{
			this->AddElement(list[index]);
		}
	}
	void AddElements(autoList<Element_t> *list)
	{
		this->AddElements(list->ElementAt(0), list->GetCount());
	}

	void InsertElements(int insPos, Element_t *list, int count) // HACK: �x��
	{
		for(int index = 0; index < count; index++)
		{
			this->InsertElement(insPos + index, list[index]);
		}
	}
	void InsertElements(int insPos, autoList<Element_t> *list)
	{
		this->InsertElements(insPos, list->ElementAt(0), list->GetCount());
	}

	void Swap(int index1, int index2)
	{
		errorCase(index1 < 0 || this->Count <= index1);
		errorCase(index2 < 0 || this->Count <= index2);

		Element_t tmp = this->List[index1];

		this->List[index1] = this->List[index2];
		this->List[index2] = tmp;
	}
	void Reverse()
	{
		int i = 0;
		int j = this->Count - 1;

		while(i < j)
		{
			this->Swap(i, j);
			i++;
			j--;
		}
	}

	void PutElement(int index, Element_t element, Element_t defaultElement)
	{
		errorCase(index < 0);

		if(this->Count <= index)
		{
			while(this->Count < index)
			{
				this->AddElement(defaultElement);
			}
			this->AddElement(element);
		}
		else
			this->SetElement(index, element);
	}
	Element_t RefElement(int index, Element_t defaultElement)
	{
		errorCase(index < 0);

		if(index < this->Count)
		{
			return this->GetElement(index);
		}
		return defaultElement;
	}

	int UnaddRepeat(Element_t e)
	{
		int num = 0;

		while(this->Count && this->List[this->Count - 1] == e)
		{
			this->Count--;
			num++;
		}
		return num;
	}
	void AddRepeat(Element_t e, int num)
	{
		for(int c = 0; c < num; c++)
		{
			this->AddElement(e);
		}
	}
};
