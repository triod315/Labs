// Lab7_VS.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <iterator>
#include <vector>
#include <stack>
#include <queue>
#include <set>

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
			if ( matrix[i][j] != 0 && matrix[j][i] != 0) cout << "(" << i << ", " << j << "); ";
}

vector<int> BFS(vector<vector<double> > &matrix, int start_pos, int end_pos)
{
	queue<int> q;//que for cheked elements
	q.push(start_pos);
	vector<bool> used(matrix.size());//vector of used points
	vector<int> p(matrix.size());

	used[start_pos] = true;
	p[start_pos] = -1;

	int v;
	while (!q.empty())
	{
		v = q.front();
		q.pop();
		for (int i = 0; i<matrix[v].size(); i++)
		{
			if (!used[i] && matrix[v][i] == 1)
			{
				used[i] = true;
				q.push(i);
				p[i] = v;
			}
		}
	}


	vector<int> path;
	for (int v = end_pos; v != -1; v = p[v])
		path.push_back(v);
	reverse(path.begin(), path.end());
	return path;
}

vector<int> DFS(vector<vector<double> > &matrix, int start_pos, int end_pos)
{
	stack<int> s;
	vector<int> p(matrix.size());
	vector<bool> used(matrix.size());
	s.push(start_pos);
	int v;
	used[start_pos] = true;
	p[start_pos] = -1;
	while (!s.empty())
	{
		v = s.top();
		s.pop();
		for (int i = 0; i<matrix.size(); i++)
		{
			if (!used[i] && matrix[v][i] == 1)
			{
				s.push(i);
				used[v] = true;
				p[i] = v;
			}
		}
	}


	vector<int> path;
	for (int v = end_pos; v != -1; v = p[v])
		path.push_back(v);
	reverse(path.begin(), path.end());
	return path;
}


double DijkstraSearch(vector<vector<double> > &matrix, int start_pos, int end_pos) 
{

	set <int> q;

	vector <double> dist(matrix.size());
	vector <int> prev(matrix.size());
	vector <bool> used(matrix.size());

	/*for (int i = 0; i < matrix.size; i++)
	{
		q.insert(i);
	}*/


	double min;

	/*for (int i = 0; i < matrix.size(); i++)
		matrix[i][i] = INT_MAX;
		*/
	for (int i = 0; i < matrix.size(); i++)
	{
		dist[i] = matrix[start_pos][i];
		used[i] = false;
	}

	dist[start_pos] = 0;
	int u = 0, index = 0;
	for (int i=0;i<matrix.size();i++)
	{
		min = INT_MAX;
		for (int j=0;j<matrix.size();j++)
			if (dist[j] < min && !used[j])
			{ 
				min = dist[j];
				index = j;
			}
		u = index;
		used[u] = true;
		
		for (int j = 0; j < matrix.size(); j++)
		{
			if (matrix[u][j] != INT_MAX && !used[j] && dist[u] != INT_MAX && (dist[u] + matrix[u][j] < dist[j]))
				dist[j] = matrix[u][j] + dist[u];
		}


		
	}


	return (dist[end_pos]!=INT_MAX)?dist[end_pos]:-1;

}


void ChangeMatrix(vector<vector<double> > &matrix) 
{
	for (int i = 0; i < matrix.size(); i++)
		for (int j = 0; j < matrix.size(); j++)
			if (matrix[i][j] == -1) matrix[i][j] = INT_MAX;
}

int main()
{
	/*int n;//count of vertices(points)
	//cout << "input count vertices\n";
	cin >> n;

	int start_point, end_point;
	//cout << "enter start point: ";
	cin >> start_point;
	//cout << "\nenter end point: ";
	cin >> end_point;

	//cout << "Write incedense matrix\n";
	vector <vector <double> > graph = GetMatrics(n);//incedense matrix
	ChangeMatrix(graph);

	cout << DijkstraSearch(graph, start_point-1, end_point-1);



	/*PrintMatrix(graph);

	vector<int> way = BFS(graph, start_point, end_point);
	copy(way.begin(), way.end(), ostream_iterator<int>(cout, " "));
	*/

	int n;//count of vertices(points)
	cout << "input count vertices\n";
	cin >> n;

	cout << "Write adjactency matrix\n";
	vector <vector <double> > graph = GetMatrics(n);//adjactency matrix

	find_ribs(graph);

	return 0;
}

