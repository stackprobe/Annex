#include "all.h"

typedef struct Node_st
{
	char *IP;
	int Count;
}
Node_t;

static Node_t *CreateNode(char *ip)
{
	Node_t *i = nb(Node_t);

	i->IP = strx(ip);

	return i;
}
static void ReleaseNode(Node_t *i, void *dummy)
{
	memFree(i->IP);
	memFree(i);
}
static int CompNode(Node_t *i1, Node_t *i2, void *dummy)
{
	return strcmp(i1->IP, i2->IP);
}

static oneObject(rbtTree_t, rbtCreateTree((int (*)(void *, void *, void *))CompNode, (void (*)(void *, void *))ReleaseNode, NULL), GetTree)

static Node_t *GetTarget(char *ip)
{
	static Node_t i;

	i.IP = ip;

	return &i;
}
void IncrementIPCount(char *ip)
{
cout("Inc.1: %s %d\n", ip, GetIPCount(ip)); // test
	if(!rbtHasElement(GetTree(), GetTarget(ip)))
	{
		rbtAddElement(GetTree(), CreateNode(ip));
	}
	((Node_t *)rbtGetElement(GetTree(), NULL))->Count++;
cout("Inc.2: %s %d\n", ip, GetIPCount(ip)); // test
}
int GetIPCount(char *ip)
{
	if(!rbtHasElement(GetTree(), GetTarget(ip)))
	{
		return 0;
	}
	return ((Node_t *)rbtGetElement(GetTree(), NULL))->Count;
}
void DecrementIPCount(char *ip)
{
cout("Dec.1: %s %d\n", ip, GetIPCount(ip)); // test
	Node_t *i = (Node_t *)rbtGetElement(GetTree(), GetTarget(ip));

	errorCase(i->Count < 1);
	i->Count--;

	if(i->Count == 0)
	{
		rbtRemoveElement(GetTree(), NULL);
	}
cout("Dec.2: %s %d\n", ip, GetIPCount(ip)); // test
}
