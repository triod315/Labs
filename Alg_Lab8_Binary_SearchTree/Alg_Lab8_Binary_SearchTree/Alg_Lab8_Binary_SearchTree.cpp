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

	void RemoveElementRec(node *el)
	{
		if (el->left_child && el->right_child)
		{
			el->value = MinElement(el->right_child)->value;
			RemoveElementRec(MinElement(el->right_child));
		}

		if (el->left_child)
			el->parent = el->left_child;
		if (el->right_child)
			el->parent = el->right_child;
		el->parent = NULL;
		delete el;
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

private:


	static void PrintBranches(int branch_len, int node_space_len, int start_len, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level / 2; i++) {
			out << ((i == 0) ? setw(start_len - 1) : setw(node_space_len - 2)) << "" << ((*iter++) ? "/" : " ");
			out << setw(2 * branch_len + 2) << "" << ((*iter++) ? "\\" : " ");
		}
		out << endl;
	}

	static string intToString(int val) {
		ostringstream ss;
		ss << val;
		return ss.str();
	}

	static void printNodes(int branch_len, int node_space_len, int start_len, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level; i++, iter++) {
			out << ((i == 0) ? setw(start_len) : setw(node_space_len)) << "" << ((*iter && (*iter)->left_child) ? setfill('_') : setfill(' '));
			out << setw(branch_len + 2) << ((*iter) ? intToString((*iter)->value) : "");
			out << ((*iter && (*iter)->right_child) ? setfill('_') : setfill(' ')) << setw(branch_len) << "" << setfill(' ');
		}
		out << endl;
	}

	static void printLeaves(int indent_space, int level, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level; i++, iter++) {
			out << ((i == 0) ? setw(indent_space + 2) : setw(2 * level + 2)) << ((*iter) ? intToString((*iter)->value) : "");
		}
		out << endl;
	}

	static int maxHeight(node *p) {
		if (!p) return 0;
		int leftHeight = maxHeight(p->left_child);
		int rightHeight = maxHeight(p->right_child);
		return (leftHeight > rightHeight) ? leftHeight + 1 : rightHeight + 1;
	}

public:
	static void printPretty(node *root, int level, int indentSpace, ostream& out) {
		int h = maxHeight(root);
		int nodesInThisLevel = 1;

		int branchLen = 2 * ((int)pow(2.0, h) - 1) - (3 - level)*(int)pow(2.0, h - 1);  // eq of the length of branch for each node of each level
		int nodeSpaceLen = 2 + (level + 1)*(int)pow(2.0, h);  // distance between left neighbor node's right arm and right neighbor node's left arm
		int startLen = branchLen + (3 - level) + indentSpace;  // starting space to the first node to print of each level (for the left most node of each level only)

		deque<node*> nodesQueue;
		nodesQueue.push_back(root);
		for (int r = 1; r < h; r++) {
			PrintBranches(branchLen, nodeSpaceLen, startLen, nodesInThisLevel, nodesQueue, out);
			branchLen = branchLen / 2 - 1;
			nodeSpaceLen = nodeSpaceLen / 2 + 1;
			startLen = branchLen + (3 - level) + indentSpace;
			printNodes(branchLen, nodeSpaceLen, startLen, nodesInThisLevel, nodesQueue, out);

			for (int i = 0; i < nodesInThisLevel; i++) {
				node *currNode = nodesQueue.front();
				nodesQueue.pop_front();
				if (currNode) {
					nodesQueue.push_back(currNode->left_child);
					nodesQueue.push_back(currNode->right_child);
				}
				else {
					nodesQueue.push_back(NULL);
					nodesQueue.push_back(NULL);
				}
			}
			nodesInThisLevel *= 2;
		}
		PrintBranches(branchLen, nodeSpaceLen, startLen, nodesInThisLevel, nodesQueue, out);
		printLeaves(indentSpace, level, nodesInThisLevel, nodesQueue, out);
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
	BST::printPretty(tree->root, 1, 4, cout);
	cout << "finished";
	system("pause");
	tree->RemoveElementRec(tree->Search(tree->root,58));
	BST::printPretty(tree->root, 0, 1, cout);
	cout << endl;
	tree->Walk(tree->root);
	cout << endl << "leaf: ";
	tree->DrunkenWalk(tree->root);
	cout << endl;
	delete tree;
	system("pause");

	return 0;
}



