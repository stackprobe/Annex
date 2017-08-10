typedef struct fint_st
{
	/*
		この値の評価 == Val / IMAX
	*/
	__int64 Val;

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
		Val = value;
		Val *= IMAX;
	}
	fint_st(double value)
	{
		LOGPOS();
		Val = (__int64)(value * IMAX);
	}
	fint_st(int numer, int denom)
	{
		LOGPOS();
		Val = numer;
		Val *= IMAX;
		Val /= denom;
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

		__int64 a1 = Val % IMAX;
		__int64 a2 = Val / IMAX;
		__int64 b1 = b.Val % IMAX;
		__int64 b2 = b.Val / IMAX;

		__int64 r1 = (a1 * b1) / IMAX;
		__int64 r2 = (a1 * b2);
		__int64 r3 = (a2 * b1);
		__int64 r4 = (a2 * b2) * IMAX;

		Val = r1 + r2 + r3 + r4;
	}
	void operator*=(int b)
	{
		LOGPOS();
		Val *= b;
	}
	void operator/=(const fint_st &b)
	{
		LOGPOS();

		__int64 a1 = Val % IMAX;
		__int64 a2 = Val / IMAX;

		__int64 r1 = (a1 * IMAX) / b.Val;
		__int64 r2 = (a2 * IMAX) / b.Val;

		Val = r1 + r2 * IMAX;
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
			return (int)((Val - IMAX / 2) / IMAX);
		else
			return (int)((Val + IMAX / 2) / IMAX);
	}
	operator double()
	{
		LOGPOS();
		return (double)Val / IMAX;
	}
}
fint_t;
