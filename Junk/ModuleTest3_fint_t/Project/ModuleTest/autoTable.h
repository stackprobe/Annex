template <class Element_t>
class autoTable
{
private:
	autoList<autoList<Element_t> *> *Rows;
	Element_t (*CreateCellFunc)(void);
	void (*ReleaseCellFunc)(Element_t);
	Element_t *DummyCell;

public:
	autoTable(Element_t (*createCellFunc)(void), void (*releaseCellFunc)(Element_t), int w = 0, int h = 0)
	{
		errorCase(!createCellFunc);
		errorCase(!releaseCellFunc);

		this->Rows = new autoList<autoList<Element_t> *>();
		this->CreateCellFunc = createCellFunc;
		this->ReleaseCellFunc = releaseCellFunc;
		this->DummyCell = NULL;
		this->Resize(w, h);
	}
	autoTable(const autoTable &source)
	{
		error();
	}
	~autoTable()
	{
		while(this->Rows->GetCount())
		{
			autoList<Element_t> *row = this->Rows->UnaddElement();

			while(row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			delete row;
		}
		delete this->Rows;

		if(this->DummyCell)
		{
			this->ReleaseCellFunc(*this->DummyCell);
			memFree(this->DummyCell);
		}
	}

	void SetCallBack(Element_t (*createCellFunc)(void), void (*releaseCellFunc)(Element_t) = NULL)
	{
		if(createCellFunc)
			this->CreateCellFunc = createCellFunc;

		if(releaseCellFunc)
			this->ReleaseCellFunc = releaseCellFunc;
	}

	void Resize(int w, int h)
	{
		errorCase(w < 0);
		errorCase(h < 0);
		errorCase(h == 0 && 0 < w);

		while(h < this->Rows->GetCount())
		{
			autoList<Element_t> *row = this->Rows->UnaddElement();

			while(row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			delete row;
		}
		while(this->Rows->GetCount() < h)
		{
			this->Rows->AddElement(new autoList<Element_t>());
		}
		for(int rowidx = 0; rowidx < h; rowidx++)
		{
			autoList<Element_t> *row = this->Rows->GetElement(rowidx);

			while(w < row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			while(row->GetCount() < w)
			{
				row->AddElement(this->CreateCellFunc());
			}
		}
	}
	int GetWidth()
	{
		return this->Rows->GetCount() ? this->Rows->GetElement(0)->GetCount() : 0;
	}
	int GetHeight()
	{
		return this->Rows->GetCount();
	}

	Element_t *CellAt(int x, int y)
	{
		errorCase(x < 0 || this->GetWidth() <= x);
		errorCase(y < 0 || this->GetHeight() <= y);

		return this->Rows->GetElement(y)->ElementAt(x);
	}
	Element_t GetCell(int x, int y)
	{
		errorCase(x < 0 || this->GetWidth() <= x);
		errorCase(y < 0 || this->GetHeight() <= y);

		return this->Rows->GetElement(y)->GetElement(x);
	}
	void SetCell(int x, int y, Element_t e)
	{
		errorCase(x < 0 || this->GetWidth() <= x);
		errorCase(y < 0 || this->GetHeight() <= y);

		this->Rows->GetElement(y)->SetElement(x, e);
	}

	Element_t RefCell(int x, int y)
	{
		if(
			x < 0 || this->GetWidth() <= x ||
			y < 0 || this->GetHeight() <= y
			) // ? out of range
		{
			if(!this->DummyCell)
			{
				this->DummyCell = (Element_t *)memAlloc(sizeof(Element_t));
				*this->DummyCell = this->CreateCellFunc();
			}
			return *this->DummyCell;
		}
		return this->GetCell(x, y);
	}
	void PutCell(int x, int y, Element_t e)
	{
		int w = m_max(x + 1, this->GetWidth());
		int h = m_max(y + 1, this->GetHeight());

		this->Resize(w, h);
		this->SetCell(x, y, e);
	}

	void Reset(int x, int y)
	{
		Element_t *p = this->CellAt(x, y);

		this->ReleaseCellFunc(*p);
		*p = this->CreateCellFunc();
	}
	void Reset(int l, int t, int w, int h)
	{
		for(int x = 0; x < w; x++)
		for(int y = 0; y < h; y++)
		{
			this->Reset(l + x, t + y);
		}
	}
	void Reset()
	{
		this->Reset(0, 0, this->GetWidth(), this->GetHeight());
	}

	void Swap(int x1, int y1, int x2, int y2)
	{
		Element_t *p1 = this->CellAt(x1, y1);
		Element_t *p2 = this->CellAt(x2, y2);
		Element_t swap_e = *p1;

		*p1 = *p2;
		*p2 = swap_e;
	}
	void Twist() // [0][0] - [x][x] Çé≤Ç…îΩì]Ç∑ÇÈÅB
	{
		for(int x = 1; x < this->GetWidth(); x++)
		for(int y = 0; y < x; y++)
		{
			this->Swap(x, y, y, x);
		}
	}
	void VTurn() // [0][m] - [x][m] Çé≤Ç…îΩì]Ç∑ÇÈÅB
	{
		this->Rows->Reverse();
	}
	void HTurn() // [m][0] - [m][x] Çé≤Ç…îΩì]Ç∑ÇÈÅB
	{
#if 1
		for(int y = 0; y < this->GetHeight(); y++)
		{
			this->Rows->GetElement(y)->Reverse();
		}
#else // SAME CODE
		this->Twist();
		this->VTurn();
		this->Twist();
#endif
	}
	void Rot() // éûåvâÒÇËÇ… 90 ìxâÒì]
	{
		this->VTurn();
		this->Twist();
	}
	void Rot2() // éûåvâÒÇËÇ… 180 ìxâÒì]
	{
		this->VTurn();
		this->HTurn();
	}
	void Rot3() // éûåvâÒÇËÇ… 270 ìxâÒì]
	{
		this->Twist();
		this->VTurn();
	}
};
