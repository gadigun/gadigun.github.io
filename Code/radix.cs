using System;
using System.Collections.Generic;
using System.Linq;

public class RadixSort
{
    /// <summary>
    /// Radix Sort implementation for positive integers
    /// Time Complexity: O(d * (n + k)) where d is the number of digits, n is the number of elements, k is the range of each digit (10 for decimal)
    /// Space Complexity: O(n + k)
    /// </summary>
    public static void Sort(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        // Find the maximum number to know number of digits
        int max = GetMaxValue(arr);
        
        // Do counting sort for every digit
        // exp is 10^i where i is current digit number
        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            CountingSortByDigit(arr, exp);
        }
    }
    
    /// <summary>
    /// Radix Sort implementation that can handle negative numbers
    /// </summary>
    public static void SortWithNegatives(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        // Separate positive and negative numbers
        List<int> negatives = new List<int>();
        List<int> positives = new List<int>();
        
        foreach (int num in arr)
        {
            if (num < 0)
                negatives.Add(-num); // Convert to positive for sorting
            else
                positives.Add(num);
        }
        
        // Sort positives normally
        if (positives.Count > 0)
        {
            int[] posArray = positives.ToArray();
            Sort(posArray);
            positives = posArray.ToList();
        }
        
        // Sort negatives (as positives) then reverse
        if (negatives.Count > 0)
        {
            int[] negArray = negatives.ToArray();
            Sort(negArray);
            negatives = negArray.Reverse().Select(x => -x).ToList();
        }
        
        // Combine results: negatives first, then positives
        int index = 0;
        foreach (int num in negatives)
            arr[index++] = num;
        foreach (int num in positives)
            arr[index++] = num;
    }

    /// <summary>
    /// Counting sort based on the digit represented by exp
    /// </summary>
    private static void CountingSortByDigit(int[] arr, int exp)
    {
        int n = arr.Length;
        int[] output = new int[n]; // Output array
        int[] count = new int[10]; // Count array for digits 0-9

        // Initialize count array
        for (int i = 0; i < 10; i++)
            count[i] = 0;

        // Store count of occurrences of each digit
        for (int i = 0; i < n; i++)
        {
            int digit = (arr[i] / exp) % 10;
            count[digit]++;
        }

        // Change count[i] so that count[i] contains actual position of this digit in output[]
        for (int i = 1; i < 10; i++)
            count[i] += count[i - 1];

        // Build the output array (go from right to left to maintain stability)
        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (arr[i] / exp) % 10;
            output[count[digit] - 1] = arr[i];
            count[digit]--;
        }

        // Copy the output array to arr[], so that arr[] contains sorted numbers according to current digit
        for (int i = 0; i < n; i++)
            arr[i] = output[i];
    }

    /// <summary>
    /// Find the maximum value in the array
    /// </summary>
    private static int GetMaxValue(int[] arr)
    {
        int max = arr[0];
        foreach (int value in arr)
        {
            if (value > max)
                max = value;
        }
        return max;
    }

    /// <summary>
    /// Generic Radix Sort for any IComparable type that can be converted to integer keys
    /// </summary>
    public static void Sort<T>(T[] arr, Func<T, int> keySelector) where T : IComparable<T>
    {
        if (arr == null || arr.Length <= 1)
            return;

        // Create array of key-value pairs
        var keyValuePairs = arr.Select((item, index) => new { Key = keySelector(item), Value = item, Index = index }).ToArray();
        
        // Extract keys for sorting
        int[] keys = keyValuePairs.Select(kvp => kvp.Key).ToArray();
        
        // Sort keys using radix sort
        Sort(keys);
        
        // Reconstruct the original array based on sorted keys
        var sortedPairs = keyValuePairs.OrderBy(kvp => Array.IndexOf(keys, kvp.Key)).ToArray();
        
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = sortedPairs[i].Value;
        }
    }

    /// <summary>
    /// Utility method to print array
    /// </summary>
    public static void PrintArray(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }
}

// Example usage and test cases
public class RadixSortExample
{
    public static void Main()
    {
        Console.WriteLine("=== Radix Sort Examples ===\n");
        
        // Test 1: Basic positive integers
        Console.WriteLine("Test 1: Basic positive integers");
        int[] arr1 = { 170, 45, 75, 90, 2, 802, 24, 66 };
        Console.WriteLine("Original: " + string.Join(", ", arr1));
        RadixSort.Sort(arr1);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr1));
        Console.WriteLine();

        // Test 2: Array with duplicates
        Console.WriteLine("Test 2: Array with duplicates");
        int[] arr2 = { 5, 2, 8, 2, 9, 1, 5, 5 };
        Console.WriteLine("Original: " + string.Join(", ", arr2));
        RadixSort.Sort(arr2);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr2));
        Console.WriteLine();

        // Test 3: Single element
        Console.WriteLine("Test 3: Single element");
        int[] arr3 = { 42 };
        Console.WriteLine("Original: " + string.Join(", ", arr3));
        RadixSort.Sort(arr3);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr3));
        Console.WriteLine();

        // Test 4: Already sorted array
        Console.WriteLine("Test 4: Already sorted array");
        int[] arr4 = { 1, 2, 3, 4, 5 };
        Console.WriteLine("Original: " + string.Join(", ", arr4));
        RadixSort.Sort(arr4);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr4));
        Console.WriteLine();

        // Test 5: Reverse sorted array
        Console.WriteLine("Test 5: Reverse sorted array");
        int[] arr5 = { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        Console.WriteLine("Original: " + string.Join(", ", arr5));
        RadixSort.Sort(arr5);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr5));
        Console.WriteLine();

        // Test 6: With negative numbers
        Console.WriteLine("Test 6: With negative numbers");
        int[] arr6 = { -5, 2, -8, 0, 9, -1, 5, -3 };
        Console.WriteLine("Original: " + string.Join(", ", arr6));
        RadixSort.SortWithNegatives(arr6);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr6));
        Console.WriteLine();

        // Test 7: Large numbers
        Console.WriteLine("Test 7: Large numbers");
        int[] arr7 = { 123456, 789, 12, 345678, 1, 999999 };
        Console.WriteLine("Original: " + string.Join(", ", arr7));
        RadixSort.Sort(arr7);
        Console.WriteLine("Sorted:   " + string.Join(", ", arr7));
        
        Console.WriteLine("\n=== Performance Test ===");
        PerformanceTest();
    }
    
    private static void PerformanceTest()
    {
        const int size = 100000;
        Random rand = new Random(42); // Fixed seed for reproducible results
        
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(0, 1000000);
        }
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        RadixSort.Sort(arr);
        stopwatch.Stop();
        
        Console.WriteLine($"Sorted {size:N0} elements in {stopwatch.ElapsedMilliseconds} ms");
        
        // Verify the array is sorted
        bool isSorted = true;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[i - 1])
            {
                isSorted = false;
                break;
            }
        }
        
        Console.WriteLine($"Array is correctly sorted: {isSorted}");
    }
}
