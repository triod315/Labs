// Alg_Lab8_Binary_SearchTree.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <deque>
#include <iomanip>
#include <string>
#include <sstream>


using namespace std;
struct node 
{
	node * parent;
	node * left_child;
	node * right_child;
	int value;
};

class BST 
{
public:
	node * root;

	BST() 
	{
		root = NULL;
	};
	
	~BST() 
	{
		destroyTree(root);
	}

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

	node * MinElement(node * x) 
	{
		while (x->left_child )
			x = x->left_child;
		return x;
	}

	node * MaxElement(node * x)
	{
		while ( x->right_child)
			x = x->right_child;
		return x;
	}

	node * NextElement(node * x) 
	{
		if (x != NULL)
			return MinElement(x->right_child);
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
		if (!el->left_child || !el->right_child)//перевірка на два нащадки
			y = el;
		else
			y=NextElement(el);

		if (y->left_child)
			x = y->left_child;
		else
			x = y->right_child;
		if (x)
			x->parent = y->parent;

		if (!y->parent)//перевірка чи є вершина коренем
			root = x;
		else
			if (y == y->parent->left_child)
				y->parent->left_child = x;
			else
				y->parent->right_child = x;
		if (y != el)
			el->value = y->value;

		delete y;

	}

	void destroyTree(node * n)
	{
		if (n)
		{
			destroyTree(n->left_child);
			destroyTree(n->right_child);
			delete (n);
		}
	}

	void Walk(node * x) 
	{
		if (x) 
		{
			Walk(x->left_child);
			cout <<x->value<< " ";
			Walk(x->right_child);
		}
	}

	void DrunkenWalk(node * x)
	{
		if (x)
		{
			DrunkenWalk(x->left_child);
			if (!x->left_child && !x->right_child)
				cout << x->value << " ";
			DrunkenWalk(x->right_child);
		}
	}
	
	node * Search(node * x, int value) 
	{
		if (!x || value == x->value)
			return x;
		if (value < x->value)
			return Search(x->left_child, value);
		return Search(x->right_child, value);
	}



};




int main()
{

	BST * tree = new BST();

	cout << "input count of elements" << endl;
	int n;
	cin >> n;
	int c;
	//cout << "input elements";
	for (int i = 0; i < n; i++)
	{
		c = rand() % 100;
		
		tree->AddElement(c);
		

	}

	system("cls");

	cout << "finished";
	system("pause");
	tree->RemoveElement(tree->Search(tree->root,58));

	cout << endl;
	tree->Walk(tree->root);
	cout << endl << "leaf: ";
	tree->DrunkenWalk(tree->root);
	cout << endl;
	delete tree;
	system("pause");

	return 0;
}



