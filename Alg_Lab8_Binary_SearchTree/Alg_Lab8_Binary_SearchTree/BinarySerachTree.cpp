#include "BinarySearchTree.h"

#include <iostream>
#include <deque>
#include <iomanip>
#include <string>
#include <sstream>
#include <fstream>
#include <stdlib.h>


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

public:	BST()
{
	root = NULL;
};

		void AddElement(int value)
		{
			node * z = new node();
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
			while (y != NULL && x == y->right_child)
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
			if (el->left_child == NULL || el->right_child)
				y = el;
			else
				y = NextElement(el);

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

public:


	void PrintBranches(int branch_len, int node_space_len, int start_len, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level / 2; i++) {
			out << ((i == 0) ? setw(start_len - 1) : setw(node_space_len - 2)) << "" << ((*iter++) ? "/" : " ");
			out << setw(2 * branch_len + 2) << "" << ((*iter++) ? "\\" : " ");
		}
		out << endl;
	}

	string intToString(int val) {
		ostringstream ss;
		ss << val;
		return ss.str();
	}

	void printNodes(int branch_len, int node_space_len, int start_len, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level; i++, iter++) {
			out << ((i == 0) ? setw(start_len) : setw(node_space_len)) << "" << ((*iter && (*iter)->left_child) ? setfill('_') : setfill(' '));
			out << setw(branch_len + 2) << ((*iter) ? intToString((*iter)->value) : "");
			out << ((*iter && (*iter)->right_child) ? setfill('_') : setfill(' ')) << setw(branch_len) << "" << setfill(' ');
		}
		out << endl;
	}

	void printLeaves(int indent_space, int level, int nodes_in_this_level, const deque<node*>& nodesQueue, ostream& out)
	{
		deque<node*>::const_iterator iter = nodesQueue.begin();
		for (int i = 0; i < nodes_in_this_level; i++, iter++) {
			out << ((i == 0) ? setw(indent_space + 2) : setw(2 * level + 2)) << ((*iter) ? intToString((*iter)->value) : "");
		}
		out << endl;
	}

	int maxHeight(node *p) {
		if (!p) return 0;
		int leftHeight = maxHeight(p->left_child);
		int rightHeight = maxHeight(p->right_child);
		return (leftHeight > rightHeight) ? leftHeight + 1 : rightHeight + 1;
	}
public:
	void printPretty(node *root, int level, int indentSpace, ostream& out) {
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