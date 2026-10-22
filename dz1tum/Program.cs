using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Ex1(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Ошибка: Файл по пути '{filePath}' не найден.");
            return;
        }

        try
        {
            string fileContent = File.ReadAllText(filePath);
            List<char> charList = new List<char>(fileContent.ToCharArray());
            string vowelsPattern = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY";
            string consonantsPattern = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩbcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
            int vowelsCount = 0;
            int consonantsCount = 0;

            foreach (char c in charList)
            {
                if (vowelsPattern.Contains(c))
                {
                    vowelsCount++;
                }
                else if (consonantsPattern.Contains(c))
                {
                    consonantsCount++;
                }
            }
            Console.WriteLine($"Количество гласных букв: {vowelsCount}");
            Console.WriteLine($"Количество согласных букв: {consonantsCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при обработке файла: {ex.Message}");
        }
    }

    static void Ex2()
    {
        LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>();
        matrixA.AddLast(new LinkedList<int>(new[] { 1, 2, 3 }));
        matrixA.AddLast(new LinkedList<int>(new[] { 4, 5, 6 }));

        LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>();
        matrixB.AddLast(new LinkedList<int>(new[] { 7, 8 }));
        matrixB.AddLast(new LinkedList<int>(new[] { 9, 1 }));
        matrixB.AddLast(new LinkedList<int>(new[] { 2, 3 }));

        Console.WriteLine("Матрица A:");
        foreach (var row in matrixA)
        {
            foreach (var element in row)
            {
                Console.Write($"{element}\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nМатрица B:");
        foreach (var row in matrixB)
        {
            foreach (var element in row)
            {
                Console.Write($"{element}\t");
            }
            Console.WriteLine();
        }

        int rowsA = matrixA.Count;
        int colsA = matrixA.First.Value.Count;
        int rowsB = matrixB.Count;
        int colsB = matrixB.First.Value.Count;

        if (colsA != rowsB)
        {
            Console.WriteLine("\nОшибка: Количество столбцов матрицы A должно совпадать с количеством строк матрицы B.");
            return;
        }
        var listA = new List<List<int>>();
        foreach (var row in matrixA)
        {
            listA.Add(new List<int>(row));
        }

        var listB = new List<List<int>>();
        foreach (var row in matrixB)
        {
            listB.Add(new List<int>(row));
        }
        LinkedList<LinkedList<int>> resultMatrix = new LinkedList<LinkedList<int>>();

        for (int i = 0; i < rowsA; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum += listA[i][k] * listB[k][j];
                }
                row.AddLast(sum);
            }
            resultMatrix.AddLast(row);
        }
        Console.WriteLine("\nРезультат умножения (Матрица A * Матрица B):");
        foreach (var row in resultMatrix)
        {
            foreach (var element in row)
            {
                Console.Write($"{element}\t");
            }
            Console.WriteLine();
        }
    }
    static void Ex3()
    {
        string[] monthNames = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };
        Dictionary<string, int[]> temperatureData = new Dictionary<string, int[]>();
        Random rand = new Random();

        foreach (string month in monthNames)
        {
            int[] dailyTemperatures = new int[30];
            for (int day = 0; day < 30; day++)
            {
                dailyTemperatures[day] = rand.Next(-20, 31); 
            }
            temperatureData.Add(month, dailyTemperatures);
        }

        Dictionary<string, double> averageTemperatures = new Dictionary<string, double>();
        foreach (var kvp in temperatureData)
        {
            double sum = kvp.Value.Sum();
            double average = sum / 30.0;
            averageTemperatures.Add(kvp.Key, average);
            
        }
        var sortedAverages = averageTemperatures.OrderBy(pair => pair.Value);
        Console.WriteLine("\nОтсортированные средние температуры по возрастанию:");
        foreach (var kvp in sortedAverages)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}°C");
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Упражнение+Домашнее задание 6.1");
        Ex1("..\\..\\file\\file1.txt");
        Console.ReadKey();
        Console.WriteLine("Упражнение+Домашнее задание 6.2");
        Ex2();
        Console.ReadKey();
        Console.WriteLine("Упражнение+Домашнее задание 6.3");
        Ex3();
        Console.ReadKey();





    }
}
