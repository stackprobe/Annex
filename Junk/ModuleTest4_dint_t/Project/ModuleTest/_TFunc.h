template <class Var_t>
Var_t TFuncTest(Var_t prm)
{
	return prm;
}

template <class Var1_t, class Var2_t>
void TFuncTest2(Var1_t prm1, Var2_t prm2)
{
	prm1(prm2);
}

void TFuncTest(void);
