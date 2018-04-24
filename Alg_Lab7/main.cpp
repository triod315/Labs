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
    vector<double> dist(matrix.size());

    used[start_pos]=true;
    p[start_pos]=-1;

	copy(used.begin(),used.end(),ostream_iterator<bool>(cout," "));

    int v;
    int to;
    while (!q.empty())
    {
        v=q.front();
        q.pop();
        for (int i=0;i<matrix[v].size();i++)
        {
            to=i;
            if (!used[to] && matrix[v][i]==1)
            {
                used[to]=true;
                q.push(to);
                dist[to]=dist[v]+;
                p[to]=v;
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
    vector <vector <double> > graph=GetMatrics(10);
   /* {
        {0,1,1,1,0,0,0,0,0,0},
        {1,0,0,0,1,0,0,0,0,0},
        {1,0,0,0,0,1,1,0,0,0},
        {1,0,0,0,0,0,0,1,0,0},
        {0,1,0,0,0,0,0,0,1,0},
        {0,0,1,0,0,0,0,0,0,1},
        {0,0,1,0,0,0,0,0,0,0},
        {0,0,0,1,0,0,0,0,0,0},
        {0,0,0,0,1,0,0,0,0,0},
        {0,0,0,0,0,1,0,0,0,0}
    };*/
    int n;//count of vertices(points)
    PrintMatrix(graph);

    vector<int> way=BFS(graph,0,9);
    copy(way.begin(),way.end(),ostream_iterator<int>(cout," "));

    return 0;
}
