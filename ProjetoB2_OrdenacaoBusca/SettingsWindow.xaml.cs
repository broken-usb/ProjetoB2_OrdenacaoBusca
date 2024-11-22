using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class SettingsWindow : Window
    {
        public int SortingDelay { get; private set; }

        public SettingsWindow(int currentDelay)
        {
            InitializeComponent();

            // Define qual botão estará selecionado com base no delay atual
            if (currentDelay == 0)
                NoDelayRadioButton.IsChecked = true;
            else if (currentDelay <= 50)
                FastRadioButton.IsChecked = true;
            else if (currentDelay <= 200)
                MediumRadioButton.IsChecked = true;
            else
                SlowRadioButton.IsChecked = true;

            // Eventos dos botões
            OkButton.Click += OkButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Define o delay com base na seleção
            if (NoDelayRadioButton.IsChecked == true)
                SortingDelay = 0;
            else if (FastRadioButton.IsChecked == true)
                SortingDelay = 50;
            else if (MediumRadioButton.IsChecked == true)
                SortingDelay = 200;
            else if (SlowRadioButton.IsChecked == true)
                SortingDelay = 500;

            DialogResult = true; // Fecha a janela com sucesso
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Fecha a janela sem salvar alterações
        }

        private void NoDelayRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
