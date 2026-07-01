using System;
using System.IO;

class Program
{
    static int[] Nums(string s)
    {
        string[] parts = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }

    static void Main()
    {
        string[] lines = File.ReadAllLines("input3.txt");

        int[] first = Nums(lines[0]);
        int n = first[0];
        int start = first[1] - 1;

        int[,] g = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            int[] row = Nums(lines[i + 1]);

            for (int j = 0; j < n; j++)
            {
                g[i, j] = row[j];
            }
        }

        int inf = 1000000000;
        int[] d = new int[n];
        bool[] used = new bool[n];

        for (int i = 0; i < n; i++)
        {
            d[i] = inf;
        }

        d[start] = 0;

        for (int step = 0; step < n; step++)
        {
            int v = -1;

            for (int i = 0; i < n; i++)
            {
                if (!used[i] && (v == -1 || d[i] < d[v]))
                {
                    v = i;
                }
            }

            if (v == -1 || d[v] == inf) break;

            used[v] = true;

            for (int to = 0; to < n; to++)
            {
                if (g[v, to] > 0 && d[v] + g[v, to] < d[to])
                {
                    d[to] = d[v] + g[v, to];
                }
            }
        }

        StreamWriter f = new StreamWriter("output3.txt");

        f.WriteLine("Алгоритм Дейкстры");

        for (int i = 0; i < n; i++)
        {
            if (d[i] == inf)
                f.WriteLine("До вершины " + (i + 1) + ": пути нет");
            else
                f.WriteLine("До вершины " + (i + 1) + ": " + d[i]);
        }

        f.Close();
    }
}
