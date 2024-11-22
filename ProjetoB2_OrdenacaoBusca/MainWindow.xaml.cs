using ProjetoB2_OrdenacaoBusca.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class MainWindow : Window
    {
        private MetricsCollector metrics = new MetricsCollector();
        private List<int> originalValues;
        private List<int> currentValues;
        private int sortingDelay = 100; // Delay padrão em milissegundos
        private List<SortingStatistics> statistics = new List<SortingStatistics>();

        public MainWindow()
        {
            InitializeComponent();
            // Eventos dos botões
            GenerateValuesButton.Click += GenerateValuesButton_Click;
            SortValuesButton.Click += SortValuesButton_Click;
            OriginalValuesButton.Click += OriginalValuesButton_Click;
            ClearButton.Click += ClearButton_Click;
            ExitButton.Click += ExitButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            StatisticsButton.Click += StatisticsButton_Click;
        }

        private void GenerateValuesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(((ComboBoxItem)QuantityComboBox.SelectedItem).Content.ToString());
                originalValues = RandomListGenerator.GenerateRandomList(quantity, 100);
                currentValues = new List<int>(originalValues);

                ResultsTextBlock.Text = $"Valores Gerados: {string.Join(", ", currentValues)}";
                DrawGraph(currentValues);
            }
            catch
            {
                MessageBox.Show("Selecione uma quantidade válida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SortValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues == null || !currentValues.Any())
            {
                MessageBox.Show("Gere os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string selectedMethod = ((ComboBoxItem)SortMethodComboBox.SelectedItem).Content.ToString();
                var stopwatch = Stopwatch.StartNew();
                int comparisons = 0, swaps = 0;

                switch (selectedMethod)
                {
                    case "Bubble Sort":
                        var resultBubble = await SortingAlgorithms.BubbleSortAsync(currentValues, sortingDelay, DrawGraph);
                        comparisons = resultBubble.Comparisons;
                        swaps = resultBubble.Swaps;
                        break;

                    case "Selection Sort":
                        var resultSelection = await SortingAlgorithms.SelectionSortAsync(currentValues, sortingDelay, DrawGraph);
                        comparisons = resultSelection.Comparisons;
                        swaps = resultSelection.Swaps;
                        break;

                    case "Insertion Sort":
                        var resultInsertion = await SortingAlgorithms.InsertionSortAsync(currentValues, sortingDelay, DrawGraph);
                        comparisons = resultInsertion.Comparisons;
                        swaps = resultInsertion.Swaps;
                        break;

                    case "Quick Sort":
                        var resultQuick = await SortingAlgorithms.QuickSortAsync(currentValues, 0, currentValues.Count - 1, sortingDelay, DrawGraph);
                        comparisons = resultQuick.Comparisons;
                        swaps = resultQuick.Swaps;
                        break;

                    case "Merge Sort":
                        var resultMerge = SortingAlgorithms.MergeSort(currentValues);
                        comparisons = resultMerge.Comparisons;
                        currentValues = resultMerge.SortedArray;
                        break;

                    default:
                        MessageBox.Show("Selecione um método válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;

                ResultsTextBlock.Text = $"Valores Ordenados ({selectedMethod}): {string.Join(", ", currentValues)}\n" +
                                        $"Tempo de execução: {elapsed.TotalMilliseconds:F3} ms\n" +
                                        $"Comparações: {comparisons}, Trocas: {swaps}";

                DrawGraph(currentValues);
                statistics.Add(new SortingStatistics
                {
                    Algorithm = selectedMethod,
                    Comparisons = comparisons,
                    Swaps = swaps,
                    TimeElapsed = elapsed.TotalMilliseconds
                });
            }
            catch
            {
                MessageBox.Show("Erro ao ordenar os valores.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            originalValues = null;
            currentValues = null;
            ResultsTextBlock.Text = "Resultados aparecerão aqui...";
            GraphCanvas.Children.Clear();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(sortingDelay);

            if (settingsWindow.ShowDialog() == true)
            {
                sortingDelay = settingsWindow.SortingDelay;
                ResultsTextBlock.Text = $"Velocidade de Ordenação Alterada: {(sortingDelay == 0 ? "Sem Delay" : $"{sortingDelay} ms")}";
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!statistics.Any())
            {
                MessageBox.Show("Não há estatísticas disponíveis. Realize uma ordenação primeiro.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var statisticsWindow = new StatisticsWindow(statistics);
            statisticsWindow.ShowDialog();
        }

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

                var rect = new Rectangle
                {
                    Width = barWidth - 2,
                    Height = barHeight,
                    Fill = Brushes.SteelBlue
                };

                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetTop(rect, canvasHeight - barHeight);
                GraphCanvas.Children.Add(rect);
            }
        }
    }
}
