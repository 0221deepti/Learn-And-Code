using System;

class PrefixSumQueries
{
    static void Main()
    {
        int[] counts = ReadIntegers();
        int numberOfElements = counts[0];
        int numberOfQueries = counts[1];

        long[] numbers = ReadLongArray(numberOfElements);
        long[] prefixSums = BuildPrefixSumArray(numbers);

        ProcessQueries(prefixSums, numberOfQueries);
    }

    static int[] ReadIntegers()
    {
        return Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }

    static long[] ReadLongArray(int size)
    {
        long[] array = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
        return array;
    }

    static long[] BuildPrefixSumArray(long[] array)
    {
        long[] prefixSums = new long[array.Length + 1];

        for (int i = 1; i <= array.Length; i++)
        {
            prefixSums[i] = prefixSums[i - 1] + array[i - 1];
        }

        return prefixSums;
    }

    static void ProcessQueries(long[] prefixSums, int queryCount)
    {
        for (int i = 0; i < queryCount; i++)
        {
            int[] range = ReadIntegers();
            int left = range[0];
            int right = range[1];

            long sum = CalculateRangeSum(prefixSums, left, right);
            int length = right - left + 1;

            Console.WriteLine(sum / length);
        }
    }

    static long CalculateRangeSum(long[] prefixSums, int left, int right)
    {
        return prefixSums[right] - prefixSums[left - 1];
    }
}
