using System.Collections.Generic;
using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(List<SortingStatistics> statistics)
        {
            InitializeComponent();
            StatisticsListView.ItemsSource = statistics;
        }
    }

    public class SortingStatistics
    {
        public string Algorithm { get; set; }
        public int Comparisons { get; set; }
        public int Swaps { get; set; }
    }
}
