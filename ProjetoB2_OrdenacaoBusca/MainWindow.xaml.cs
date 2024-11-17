using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class MainWindow : Window
    {
        private List<int> originalValues;
        private List<int> currentValues;

        public MainWindow()
        {
            InitializeComponent();

            // Eventos dos botões
            GenerateValuesButton.Click += GenerateValuesButton_Click;
            SortValuesButton.Click += SortValuesButton_Click;
            OriginalValuesButton.Click += OriginalValuesButton_Click;
            ClearButton.Click += ClearButton_Click;
            ExitButton.Click += ExitButton_Click;
        }

        /// <summary>
        /// Gera valores aleatórios.
        /// </summary>
        private void GenerateValuesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(((ComboBoxItem)QuantityComboBox.SelectedItem).Content.ToString());
                var random = new Random();

                originalValues = Enumerable.Range(1, quantity)
                                           .Select(_ => random.Next(1, 101))
                                           .ToList();

                currentValues = new List<int>(originalValues);
                ResultsTextBlock.Text = $"Valores Gerados: {string.Join(", ", currentValues)}";
                DrawGraph(currentValues);
            }
            catch
            {
                MessageBox.Show("Selecione uma quantidade válida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Ordena os valores e exibe o tempo de execução.
        /// </summary>
        private void SortValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues == null || !currentValues.Any())
            {
                MessageBox.Show("Gere os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string selectedMethod = ((ComboBoxItem)SortMethodComboBox.SelectedItem).Content.ToString();

                var stopwatch = Stopwatch.StartNew(); // Inicia o cronômetro

                switch (selectedMethod)
                {
                    case "Bubble Sort":
                        BubbleSort(currentValues);
                        break;
                    case "Selection Sort":
                        SelectionSort(currentValues);
                        break;
                    case "Insertion Sort":
                        InsertionSort(currentValues);
                        break;
                    case "Quick Sort":
                        QuickSort(currentValues, 0, currentValues.Count - 1);
                        break;
                    case "Merge Sort":
                        currentValues = MergeSort(currentValues);
                        break;
                    default:
                        MessageBox.Show("Selecione um método válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                stopwatch.Stop(); // Para o cronômetro
                TimeSpan elapsed = stopwatch.Elapsed;

                ResultsTextBlock.Text = $"Valores Ordenados ({selectedMethod}): {string.Join(", ", currentValues)}\n" +
                                        $"Tempo de execução: {elapsed.TotalMilliseconds:F3} ms";

                DrawGraph(currentValues); // Atualiza o gráfico
            }
            catch
            {
                MessageBox.Show("Erro ao ordenar os valores.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Exibe os valores originais.
        /// </summary>
        private void OriginalValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalValues == null || !originalValues.Any())
            {
                MessageBox.Show("Gere os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentValues = new List<int>(originalValues);
            ResultsTextBlock.Text = $"Valores Originais: {string.Join(", ", currentValues)}";
            DrawGraph(currentValues);
        }

        /// <summary>
        /// Limpa os valores e resultados.
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            originalValues = null;
            currentValues = null;
            ResultsTextBlock.Text = "Resultados aparecerão aqui...";
            GraphCanvas.Children.Clear(); // Limpa o gráfico
        }

        /// <summary>
        /// Fecha o aplicativo.
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Desenha o gráfico no Canvas.
        /// </summary>
        private void DrawGraph(List<int> values)
        {
            GraphCanvas.Children.Clear();
            if (values == null || !values.Any()) return;

            double canvasWidth = GraphCanvas.ActualWidth;
            double canvasHeight = GraphCanvas.ActualHeight;
            double barWidth = canvasWidth / values.Count;

            double maxValue = values.Max();

            for (int i = 0; i < values.Count; i++)
            {
                double barHeight = (values[i] / maxValue) * canvasHeight;

                // Desenha a barra
                var rect = new Rectangle
                {
                    Width = barWidth - 2, // Espaço entre as barras
                    Height = barHeight,
                    Fill = Brushes.SteelBlue
                };

                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetTop(rect, canvasHeight - barHeight);
                GraphCanvas.Children.Add(rect);

                // Exibe o valor no topo da barra
                var text = new TextBlock
                {
                    Text = values[i].ToString(),
                    FontSize = 12,
                    Foreground = Brushes.Black
                };

                Canvas.SetLeft(text, i * barWidth + (barWidth - text.ActualWidth) / 2);
                Canvas.SetTop(text, canvasHeight - barHeight - 20); // 20px acima da barra
                GraphCanvas.Children.Add(text);
            }
        }

        #region Algoritmos de Ordenação
        private void BubbleSort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    }
                }
            }
        }

        private void SelectionSort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] < list[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    (list[i], list[minIndex]) = (list[minIndex], list[i]);
                }
            }
        }

        private void InsertionSort(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int key = list[i];
                int j = i - 1;

                while (j >= 0 && list[j] > key)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }
        }

        private void QuickSort(List<int> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);

                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        private int Partition(List<int> list, int low, int high)
        {
            int pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (list[j] < pivot)
                {
                    i++;
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }

            (list[i + 1], list[high]) = (list[high], list[i + 1]);
            return i + 1;
        }

        private List<int> MergeSort(List<int> list)
        {
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            var left = MergeSort(list.GetRange(0, mid));
            var right = MergeSort(list.GetRange(mid, list.Count - mid));

            return Merge(left, right);
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            while (left.Any() && right.Any())
            {
                if (left.First() <= right.First())
                {
                    result.Add(left.First());
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right.First());
                    right.RemoveAt(0);
                }
            }

            result.AddRange(left);
            result.AddRange(right);

            return result;
        }
        #endregion
    }
}
