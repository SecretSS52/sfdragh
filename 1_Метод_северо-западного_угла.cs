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
        string[] lines = File.ReadAllLines("input1.txt");

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

        int r = 0;
        int col = 0;

        while (r < m && col < n)
        {
            int v;

            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;

            if (a[r] == 0) r++;
            if (col < n && b[col] == 0) col++;
        }

        int sum = 0;

        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];

        StreamWriter f = new StreamWriter("output1.txt");

        f.WriteLine("Метод северо-западного угла");
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
