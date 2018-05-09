// Alg_Lab8_Binary_SearchTree.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

struct node 
{
	node * parent;
	node * left_child;
	node * right_child;
	int value;
};

struct BST 
{
	node * root;

	BST() 
	{
		root = NULL;
	};

	void AddElement(int value) 
	{
		node * z=new node();
		z->value = value;
		node * x = root;
		node * y = NULL;
		while (x != NULL) 
		{
			y = x;
			if (z->value < x->value)
				x = x->left_child;
			else
				x = x->right_child;
		}

		z->parent = y;
		if (y == NULL)
			root = z;
		else
			if (z->value < y->value)
				y->left_child = z;
			else
				y->right_child = z;

	}

	node * MaxElement(node * x) 
	{
		while (x->left_child != NULL)
			x = x->left_child;
		return x;
	}

	node * NextElement(node * x) 
	{
		if (x != NULL)
			return MaxElement(x->right_child);
		node * y = x->parent;
		while (y!=NULL && x==y->right_child)
		{
			x = y;
			y = y->parent;
		}
		return y;
	}

	void RemoveElement(node *el) 
	{
		node * y;
		node * x;
		if (el->left_child==NULL || el->right_child)
			y = el;
		else
			y=NextElement(el);

		if (y->left_child != NULL)
			x = y->left_child;
		else
			x = y->right_child;
		if (x != NULL)
			x->parent = y->parent;
		if (y->parent == NULL)
			root = x;
		else
			if (y == y->parent->left_child)
				y->parent->left_child = x;
			else
				y->parent->right_child = x;
		if (y != el)
			el->value = y->value;
	}
};

int main()
{

	BST tree;
	tree.AddElement(2);
	tree.AddElement(3);
	tree.AddElement(-1);
	tree.AddElement(4);

	tree.RemoveElement(tree.root->right_child);
    return 0;
}

