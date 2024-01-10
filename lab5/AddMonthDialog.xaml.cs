using System;
using System.Windows;

namespace lab5
{
    public partial class AddMonthDialog : Window
    {
        public int SelectedYear { get; private set; }
        public int SelectedMonth { get; private set; }

        public AddMonthDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(yearTextBox.Text, out var year) && int.TryParse(monthTextBox.Text, out var month))
            {
                SelectedYear = year;
                SelectedMonth = month;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for year and month.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}