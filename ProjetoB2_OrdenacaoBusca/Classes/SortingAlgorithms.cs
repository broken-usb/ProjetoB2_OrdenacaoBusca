using System;

namespace ProjetoB2_OrdenacaoBusca.Classes
{
    public static class SortingAlgorithms
    {
        public static (int[] sortedArray, int comparisons, int swaps) BubbleSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                for (int j = 0; j < sortedArray.Length - i - 1; j++)
                {
                    comparisons++;
                    if (sortedArray[j] > sortedArray[j + 1])
                    {
                        (sortedArray[j], sortedArray[j + 1]) = (sortedArray[j + 1], sortedArray[j]);
                        swaps++;
                    }
                }
            }
            return (sortedArray, comparisons, swaps);
        }
    }
}
