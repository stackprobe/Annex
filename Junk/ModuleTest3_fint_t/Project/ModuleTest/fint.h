#define FINT_DENOM 1000

typedef struct fint_st
{
	/*
		この値の評価 == Val / FINT_DENOM
	*/
	int Val;

	fint_st()
	{
		LOGPOS(); // noop
	}
	fint_st(const fint_st &i) // const 入れないとコピーコンストラクタにならない！ fint_t c = fint_t(1, 10); のとき作用しない！
	{
		LOGPOS();
		Val = i.Val;
	}
	fint_st(int value)
	{
		LOGPOS();
		Val = value * FINT_DENOM;
	}
	fint_st(double value)
	{
		LOGPOS();
		Val = (int)(value * FINT_DENOM);
	}
	fint_st(int numer, int denom)
	{
		LOGPOS();
		Val = (int)(((__int64)numer * FINT_DENOM) / denom);
	}

	void operator=(const fint_st &b) // & 入れたら、const 入れないと怒られる！
	{
		LOGPOS();
		Val = b.Val;
	}
	void operator+=(const fint_st &b)
	{
		LOGPOS();
		Val += b.Val;
	}
	void operator-=(const fint_st &b)
	{
		LOGPOS();
		Val -= b.Val;
	}
	void operator*=(const fint_st &b)
	{
		LOGPOS();
		Val = (int)(((__int64)Val * b.Val) / FINT_DENOM);
	}
	void operator*=(int b)
	{
		LOGPOS();
		Val *= b;
	}
	void operator/=(const fint_st &b)
	{
		LOGPOS();
		Val = (int)(((__int64)Val * FINT_DENOM) / b.Val);
	}
	void operator/=(int b)
	{
		LOGPOS();
		Val /= b;
	}

	fint_st operator+(const fint_st &b) // & 入れないとコピーコンストラクタが走る！
	{
		LOGPOS();

		fint_st ret = *this;
		ret += b;
		return ret;
	}
	fint_st operator-(const fint_st &b)
	{
		LOGPOS();

		fint_st ret = *this;
		ret -= b;
		return ret;
	}
	fint_st operator-()
	{
		LOGPOS();

		fint_st ret = *this;
		ret.Val *= -1;
		return ret;
	}
	fint_st operator*(const fint_st &b)
	{
		LOGPOS();

		fint_st ret = *this;
		ret *= b;
		return ret;
	}
	fint_st operator*(int &b)
	{
		LOGPOS();

		fint_st ret = *this;
		ret *= b;
		return ret;
	}
	fint_st operator/(const fint_st &b)
	{
		LOGPOS();

		fint_st ret = *this;
		ret /= b;
		return ret;
	}
	fint_st operator/(int &b)
	{
		LOGPOS();

		fint_st ret = *this;
		ret /= b;
		return ret;
	}

	int operator<(const fint_st &b)
	{
		LOGPOS();
		return Val < b.Val;
	}
	int operator<=(const fint_st &b)
	{
		LOGPOS();
		return Val <= b.Val;
	}
	int operator>(const fint_st &b)
	{
		LOGPOS();
		return Val > b.Val;
	}
	int operator>=(const fint_st &b)
	{
		LOGPOS();
		return Val >= b.Val;
	}

	operator int()
	{
		LOGPOS();

		if(Val < 0)
			return (Val - FINT_DENOM / 2) / FINT_DENOM;
		else
			return (Val + FINT_DENOM / 2) / FINT_DENOM;
	}
	operator double()
	{
		LOGPOS();
		return (double)Val / FINT_DENOM;
	}
}
fint_t;
