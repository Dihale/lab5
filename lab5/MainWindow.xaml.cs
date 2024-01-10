using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Calendar = System.Globalization.Calendar;

namespace lab5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateTreeView();
        }

        private void TreeView_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = GetTreeViewItemUnderMouse(e.GetPosition(treeView));
            if (selectedItem != null)
            {
                var contextMenu = new ContextMenu();

                var addMonthItem = new MenuItem { Header = "Добавить месяц" };
                addMonthItem.Click += AddMonthItem_Click;
                contextMenu.Items.Add(addMonthItem);

                var calculateDurationItem = new MenuItem { Header = "Рассчитать длительность" };
                calculateDurationItem.Click += CalculateDurationItem_Click;
                contextMenu.Items.Add(calculateDurationItem);

                var checkLeapYearItem = new MenuItem { Header = "Проверить високосность года" };
                checkLeapYearItem.Click += CheckLeapYearItem_Click;
                contextMenu.Items.Add(checkLeapYearItem);

                selectedItem.ContextMenu = contextMenu;
            }
        }

        private void AddMonthItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddMonthDialog();
            if (dialog.ShowDialog() == true)
            {
                var year = dialog.SelectedYear;
                var month = dialog.SelectedMonth;

                var yearNode = FindYearNode(year);
                
                if (yearNode == null) return;
                
                var monthNode = CreateMonthNode(year, month);

                if (monthNode == null)
                {
                    return;
                }

                for (var week = 1; week <= 4; week++)
                {
                    monthNode.Items.Add(CreateWeekNode(year, month, week));
                }

                yearNode.Items.Add(monthNode);

                yearNode.Items.Refresh();
            }
        }


        private void CalculateDurationItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CalculateDurationDialog();
            if (dialog.ShowDialog() != true) return;
            var startDate = dialog.StartDate;
            var endDate = dialog.EndDate;

            var duration = new Lab3.Calendar().CalculateDuration(startDate, endDate);

            MessageBox.Show($"Duration: {duration} days");
        }


        private void CheckLeapYearItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CheckLeapYearDialog();
            if (dialog.ShowDialog() != true) return;
            var year = dialog.SelectedYear;

            var isLeapYear = new Lab3.Calendar().IsLeapYear(year);

            MessageBox.Show(isLeapYear ? $"{year} is a leap year." : $"{year} is not a leap year.");
        }


        private TreeViewItem GetTreeViewItemUnderMouse(Point mousePosition)
        {
            var hitTestResult = VisualTreeHelper.HitTest(treeView, mousePosition);
            var obj = hitTestResult.VisualHit;

            while (obj != null && !(obj is TreeViewItem))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            return obj as TreeViewItem;
        }

        private void PopulateTreeView()
        {
            for (var year = 2014; year <= 2077; year++)
            {
                treeView.Items.Add(CreateYearNode(year));
            }
        }

        private TreeViewItem CreateYearNode(int year)
        {
            var yearNode = new TreeViewItem { Header = $"Year {year}" };

            return yearNode;
        }

        private TreeViewItem CreateMonthNode(int year, int month)
        {
            return new TreeViewItem { Header = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}" };
        }

        private TreeViewItem CreateWeekNode(int year, int month, int week)
        {
            var weekNode = new TreeViewItem { Header = $"Week {week}" };

            for (var day = 1; day <= 7; day++)
            {
                weekNode.Items.Add(new TreeViewItem { Header = $"{day}.{month}.{year}" });
            }

            return weekNode;
        }

        private TreeViewItem FindYearNode(int year)
        {
            foreach (TreeViewItem item in treeView.Items)
            {
                if (item.Header.ToString() == $"Year {year}")
                {
                    return item;
                }
            }
            return null;
        }
    }
}
