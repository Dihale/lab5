using System;
using System.Windows;

namespace lab5
{
    public partial class CalculateDurationDialog : Window
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public CalculateDurationDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.SelectedDate.HasValue && endDatePicker.SelectedDate.HasValue)
            {
                StartDate = startDatePicker.SelectedDate.Value;
                EndDate = endDatePicker.SelectedDate.Value;

                if (StartDate <= EndDate)
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("End Date should be greater than or equal to Start Date.");
                }
            }
            else
            {
                MessageBox.Show("Please select both Start Date and End Date.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}