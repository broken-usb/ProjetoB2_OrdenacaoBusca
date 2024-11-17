using System;
using System.Linq;
using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class MainWindow : Window
    {
        private int[] randomList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            randomList = RandomListGenerator.GenerateRandomList(10);
            OutputText.Text = $"Lista Gerada: {string.Join(", ", randomList)}";
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (randomList == null)
            {
                MessageBox.Show("Por favor, gere uma lista primeiro!");
                return;
            }

            var (sortedList, comparisons, swaps) = SortingAlgorithms.BubbleSort(randomList);
            OutputText.Text = $"Lista Ordenada: {string.Join(", ", sortedList)}\n" +
                              $"Comparações: {comparisons}, Trocas: {swaps}";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (randomList == null)
            {
                MessageBox.Show("Por favor, gere uma lista primeiro!");
                return;
            }

            int target = 5; // Substitua por um valor ou insira uma entrada de usuário.
            int comparisons = 0;
            int index = SearchAlgorithms.BinarySearchRecursive(randomList, target, 0, randomList.Length - 1, ref comparisons);

            OutputText.Text = index >= 0
                ? $"Valor {target} encontrado na posição {index} após {comparisons} comparações."
                : $"Valor {target} não encontrado após {comparisons} comparações.";
        }
    }
}
