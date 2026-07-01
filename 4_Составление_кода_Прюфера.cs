using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input4.txt");

        int n = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;

            string[] p = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int a = int.Parse(p[0]);
            int b = int.Parse(p[1]);

            if (a > n) n = a;
            if (b > n) n = b;
        }

        int[,] g = new int[n + 1, n + 1];
        int[] deg = new int[n + 1];

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;

            string[] p = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int a = int.Parse(p[0]);
            int b = int.Parse(p[1]);

            g[a, b] = 1;
            g[b, a] = 1;

            deg[a]++;
            deg[b]++;
        }

        string code = "";

        for (int step = 0; step < n - 2; step++)
        {
            int leaf = 1;

            while (deg[leaf] != 1)
            {
                leaf++;
            }

            int next = 1;

            while (g[leaf, next] == 0)
            {
                next++;
            }

            if (code != "") code += " ";
            code += next;

            g[leaf, next] = 0;
            g[next, leaf] = 0;

            deg[leaf]--;
            deg[next]--;
        }

        File.WriteAllText("output4.txt", "Код Прюфера: " + code);
    }
}
