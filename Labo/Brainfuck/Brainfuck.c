#include "C:\Factory\Common\all.h"

static autoList_t *Memory;
static uint Ptr;

static uint Increment_Ptr(void)
{
	errorCase(Ptr == UINTMAX);
	Ptr++;
	return 0;
}
static uint Decrement_Ptr(void)
{
	errorCase(!Ptr);
	Ptr--;
	return 0;
}
static uint Increment(void)
{
	errorCase(refElement(Memory, Ptr) == UINTMAX);
	putElement(Memory, Ptr, refElement(Memory, Ptr) + 1);
	return 0;
}
static uint Decrement(void)
{
	errorCase(!refElement(Memory, Ptr));
	putElement(Memory, Ptr, refElement(Memory, Ptr) - 1);
	return 0;
}
static uint PrintChar(void)
{
	uint chr = refElement(Memory, Ptr);

	if(chr == 0x09) // ? Tab
	{
		cout("\t");
	}
	else if(chr == 0x0d || chr == 0x0a) // ? CR || LF
	{
		cout("\n");
	}
	else if(m_isHalf(chr))
	{
		cout("%c", (int)chr);
	}
	else
	{
		cout("[%02x]\n", chr);
	}
	return 0;
}
static uint InputChar(void)
{
	int chr = readChar(stdin);

	if(m_isRange(chr, 0x00, 0xff))
	{
		putElement(Memory, Ptr, (uint)chr);
	}
	else
	{
		cout("[INPUT_ERROR]\n");
	}
	return 0;
}
static uint EnterLoop(void)
{
	return m_01(refElement(Memory, Ptr));
}
static uint End(void)
{
	termination(0);
	return 0; // dummy
}
static uint Never(void)
{
	error(); // never
	return 0; // dummy
}

typedef struct Command_st
{
	uint (*Method)(void);
	struct Command_st *Next[2];
}
Command_t;

static Command_t *GetNeverCommand(void)
{
	static Command_t *i;

	if(!i)
	{
		i = nb(Command_t);

		i->Method = Never;
		i->Next[0] = i;
		i->Next[1] = i;
	}
	return i;
}
static Command_t *MakeCommand(Command_t *p, uint (*method)(void))
{
	Command_t *i = nb(Command_t);

	i->Method = method;
	i->Next[0] = GetNeverCommand();
	i->Next[1] = GetNeverCommand();

	p->Next[0] = i;

	return i;
}
static Command_t *LoadProgram(char *sourceFile)
{
	char *source = readText_b(sourceFile);
	char *p;
	autoList_t *stack = newList();
	Command_t *command;
	Command_t entry;

	command = &entry;

	for(p = source; *p; p++)
	{
		switch(*p)
		{
		case '>': command = MakeCommand(command, Increment_Ptr); break;
		case '<': command = MakeCommand(command, Decrement_Ptr); break;
		case '+': command = MakeCommand(command, Increment); break;
		case '-': command = MakeCommand(command, Decrement); break;
		case '.': command = MakeCommand(command, PrintChar); break;
		case ',': command = MakeCommand(command, InputChar); break;
		case '[':
			command = MakeCommand(command, EnterLoop);
			addElement(stack, (uint)command);
			break;

		case ']':
			command->Next[0] = (Command_t *)unaddElement(stack);
			command = command->Next[0];
			command->Next[1] = command->Next[0];
			break;

		default:
			break;
		}
	}
	errorCase(getCount(stack));

	MakeCommand(command, End);

	memFree(source);
	releaseAutoList(stack);

	return entry.Next[0];
}
int main(int argc, char **argv)
{
	Command_t *i = LoadProgram(getArg(0)); // g

	Memory = newList();

	for(; ; )
	{
		i = i->Next[i->Method()];
	}
}
