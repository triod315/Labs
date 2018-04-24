#include <bits/stdc++.h>

using namespace std;

vector <vector <double> > GetMatrics(int n)
{
    vector <vector <double > > matrix(n,vector<double>(n));

    for (int i=0;i<n;i++)
        for (int j=0;j<n;j++)
        cin>>matrix[i][j];
    return matrix;
}

void PrintMatrix(vector<vector<double> > &matrix)
{
    cout<<"matrix"<<endl;
    for (int i=0; i<matrix.size();i++)
        {
        copy(matrix[i].begin(),matrix[i].end(),ostream_iterator<double>(cout," "));
        cout<<endl;
        }

}

vector<int> BFS(vector<vector<double> > &matrix, int start_pos, int end_pos)
{
    queue<int> q;//que for cheked elements
    q.push(start_pos);
    vector<bool> used(matrix.size());//vector of used points
    vector<int> p(matrix.size());

    used[start_pos]=true;
    p[start_pos]=-1;
	
    int v;
    while (!q.empty())
    {
        v=q.front();
        q.pop();
        for (int i=0;i<matrix[v].size();i++)
        {
            if (!used[i] && matrix[v][i]==1)
            {
                used[i]=true;
                q.push(i);
                p[i]=v;
            }
        }
    }


    vector<int> path;
    for (int v=end_pos;v!=-1;v=p[v])
        path.push_back(v);
    reverse(path.begin(),path.end());
    return path;
}

vector<int> DFS(vector<vector<double> > &matrix, int start_pos, int end_pos)
{
	stack<int> s;
	vector<int> p(matrix.size());
	vector<bool> used(matrix.size());
	s.push(start_pos);
	int v;
	used[start_pos]=true;
	p[start_pos]=-1;
	while (!s.empty())
		{
			v=s.top();
			s.pop();
			for (int i=0;i<matrix.size();i++)
				{
					if (!used[i] && matrix[v][i]==1)
					{
                        s.push(i);
						used[v]=true;
						p[i]=v;
					}
				}
		}
		
		
    vector<int> path;
    for (int v=end_pos;v!=-1;v=p[v])
        path.push_back(v);
    reverse(path.begin(),path.end());
	return path;
}

int main()
{
	int n;//count of vertices(points)
	cout<<"input count vertices\n";
	cin>>n;
	cout<<"Write incedense matrix\n";
    vector <vector <double> > graph=GetMatrics(n);//incedense matrix

	int start_point,end_point;
	
	cout<<"enter start point: ";
	cin>>start_point;
	cout<<"\nenter end point: ";
	cin>>end_point;

    PrintMatrix(graph);

    vector<int> way=BFS(graph,start_point,end_point);
    copy(way.begin(),way.end(),ostream_iterator<int>(cout," "));

    return 0;
}
