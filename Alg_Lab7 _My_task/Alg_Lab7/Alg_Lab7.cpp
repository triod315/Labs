// Alg_Lab7.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
#include <ostream>
#include <iterator>


using namespace std;

vector <vector <double> > GetMatrics(int n)
{
	vector <vector <double > > matrix(n, vector<double>(n));

	for (int i = 0; i<n; i++)
		for (int j = 0; j<n; j++)
			cin >> matrix[i][j];
	return matrix;
}

void PrintMatrix(vector<vector<double> > &matrix)
{
	cout << "matrix" << endl;
	for (int i = 0; i<matrix.size(); i++)
	{
		copy(matrix[i].begin(), matrix[i].end(), ostream_iterator<double>(cout, " "));
		cout << endl;
	}

}

void find_ribs(vector<vector<double> > &matrix) 
{
	for (int i = 0; i < matrix.size(); i++)
		for (int j = 0; j < i; j++)
			if (matrix[i][j] != matrix[j][i] && matrix[i][j] != 0) cout <<"("<< i<<", "<< j<<"); ";
}

int main()
{
	int n;
	cout << "input count of elements: ";
	cin >> n;
	cout << endl;

	cout << "input matrix\n";
	vector<vector<double> > matrix = GetMatrics(n);
	find_ribs(matrix);
    return 0;
}

