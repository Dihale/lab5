using System;
using System.Windows;

namespace lab5
{
    public partial class CheckLeapYearDialog : Window
    {
        public int SelectedYear { get; private set; }

        public CheckLeapYearDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(yearTextBox.Text, out var year))
            {
                SelectedYear = year;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid year.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}