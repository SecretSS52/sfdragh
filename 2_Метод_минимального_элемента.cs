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
        string[] lines = File.ReadAllLines("input2.txt");

        int[] size = Nums(lines[0]);
        int m = size[0];
        int n = size[1];

        int[] a = Nums(lines[1]);
        int[] b = Nums(lines[2]);

        int[,] c = new int[m, n];
        int[,] x = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            int[] row = Nums(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                c[i, j] = row[j];
            }
        }

        while (true)
        {
            int min = 1000000;
            int r = -1;
            int col = -1;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i] > 0 && b[j] > 0 && c[i, j] < min)
                    {
                        min = c[i, j];
                        r = i;
                        col = j;
                    }
                }
            }

            if (r == -1) break;

            int v;

            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;
        }

        int sum = 0;

        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];

        StreamWriter f = new StreamWriter("output2.txt");

        f.WriteLine("Метод минимального элемента");
        f.WriteLine("План:");

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                f.Write(x[i, j] + " ");
            }

            f.WriteLine();
        }

        f.WriteLine("Стоимость = " + sum);
        f.Close();
    }
}
