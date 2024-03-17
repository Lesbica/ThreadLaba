using System;
using System.IO;
using System.Threading;
namespace ThreadLaba;

class Program
{
    static void Main(string[] args)
    {
        // Завдання 1
        Console.WriteLine("Enter array elements separated by space:");
        string[] input = Console.ReadLine().Split(' ');
        int[] array = Array.ConvertAll(input, int.Parse);

        Thread maxThread = new Thread(() =>
        {
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            Console.WriteLine("Maximum: " + max);
        });

        Thread minThread = new Thread(() =>
        {
            int min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                    min = array[i];
            }
            Console.WriteLine("Minimum: " + min);
        });

        maxThread.Start();
        minThread.Start();
        maxThread.Join();
        minThread.Join();

        // Завдання 2
        int[] array1 = { 1, 2, 3 };
        int[] array2 = { 4, 5, 6 };
        int[] array3 = { 7, 8, 9 };

        SaveAsThread saveThread1 = new SaveAsThread(array1, "array1.txt");
        SaveAsThread saveThread2 = new SaveAsThread(array2, "array2.txt");
        SaveAsThread saveThread3 = new SaveAsThread(array3, "array3.txt");

        Thread thread1 = new Thread(new ThreadStart(saveThread1.SaveToFile));
        Thread thread2 = new Thread(new ThreadStart(saveThread2.SaveToFile));
        Thread thread3 = new Thread(new ThreadStart(saveThread3.SaveToFile));

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        // Завдання 3
        string[] files = { "AI1.txt", "AI2.txt", "AI3.txt" };

        foreach (string file in files)
        {
            Thread readThread = new Thread(() =>
            {
                Console.WriteLine($"The contents of the file {file}:");

                // Check if the file exists
                if (File.Exists(file))
                {
                    // File exists, read its contents
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            });
            readThread.Start();
            readThread.Join();
        }

        // Завдання 4
        string[] stringsToSort = { "zebra", "apple", "orange", "banana" };

        Thread insertionSortThread = new Thread(() =>
        {
            string[] copy = (string[])stringsToSort.Clone();
            InsertionSort(copy);
            Console.WriteLine("Insert sort: " + string.Join(", ", copy));
        });

        Thread selectionSortThread = new Thread(() =>
        {
            string[] copy = (string[])stringsToSort.Clone();
            SelectionSort(copy);
            Console.WriteLine("Sort by selection: " + string.Join(", ", copy));
        });

        Thread bubbleSortThread = new Thread(() =>
        {
            string[] copy = (string[])stringsToSort.Clone();
            BubbleSort(copy);
            Console.WriteLine("Bubble sorting: " + string.Join(", ", copy));
        });


        void InsertionSort(string[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                string key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j].CompareTo(key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        void SelectionSort(string[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j].CompareTo(arr[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                string temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        void BubbleSort(string[] arr)
        {
            int n = arr.Length;
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (arr[i - 1].CompareTo(arr[i]) > 0)
                    {
                        string temp = arr[i - 1];
                        arr[i - 1] = arr[i];
                        arr[i] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }

        insertionSortThread.Start();
        selectionSortThread.Start();
        bubbleSortThread.Start();

        insertionSortThread.Join();
        selectionSortThread.Join();
        bubbleSortThread.Join();
    }
}