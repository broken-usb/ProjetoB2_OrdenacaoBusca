using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class SettingsWindow : Window
    {
        public int NumberOfElements { get; private set; }
        public int MaxValue { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumberOfElementsInput.Text, out int elements) &&
                int.TryParse(MaxValueInput.Text, out int max))
            {
                NumberOfElements = elements;
                MaxValue = max;
                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter valid numbers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
