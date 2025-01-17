using System;

namespace OrderingNumbers.Services
{
    public class Sorting
    {
        public int[] QuickSort(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return numbers;
            }
            PerformQuickSort(numbers, 0, numbers.Length - 1);
            return numbers;
        }

        private void PerformQuickSort(int[] numbers, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(numbers, low, high);
                PerformQuickSort(numbers, low, pi - 1);
                PerformQuickSort(numbers, pi + 1, high);
            }
        }

        private int Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    Swap(numbers, i, j);
                }
            }
            Swap(numbers, i + 1, high);
            return i + 1;
        }

        private void Swap(int[] numbers, int i, int j)
        {
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }
    }
}