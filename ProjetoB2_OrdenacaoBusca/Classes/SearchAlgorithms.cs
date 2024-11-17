using System;

namespace ProjetoB2_OrdenacaoBusca
{
    public class SearchAlgorithms
    {
        public static int BinarySearchRecursive(int[] array, int target, int left, int right, ref int comparisons)
        {
            if (left > right) return -1;

            int mid = (left + right) / 2;
            comparisons++;

            if (array[mid] == target) return mid;
            if (array[mid] > target)
                return BinarySearchRecursive(array, target, left, mid - 1, ref comparisons);

            return BinarySearchRecursive(array, target, mid + 1, right, ref comparisons);
        }
    }
}
