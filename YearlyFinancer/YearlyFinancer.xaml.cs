using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YearlyFinancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int totalMonths = 12;
        public const int totalWeeks = 52;
        private const string fileFilter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

        private string FileName = string.Empty;

        public ObservableCollection<DataEntry> Data
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            Data = new ObservableCollection<DataEntry>();
        }
        
        private void InsertValue(DataEntry.TransactionType transType)
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            var dataEntry = new DataEntry() { Description = this.Description.Text,
                                              Date = DateTime.Now,
                                              TransType = transType,
                                              EntryValue = value};
            Data.Add(dataEntry);
        }
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEntry();
            UpdateStats();
        }

        private void AddEntry()
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            DataEntry.TransactionType transType = DataEntry.TransactionType.Income;
            if (incomeRadioButton.IsChecked == true)
            {
                transType = DataEntry.TransactionType.Income;
            }
            else if (spendingRadioButton.IsChecked == true)
            {
                transType = DataEntry.TransactionType.Spending;
            }
            else if (creditRadioButton.IsChecked == true)
            {
                transType = DataEntry.TransactionType.Credit;
            }

            InsertValue(transType);
        }

        private void UpdateStats()
        {
            double totalCredit = Data.Where(d => d.TransType == DataEntry.TransactionType.Credit).ToList().Sum(v => v.EntryValue);
            double totalIncome = Data.Where(d => d.TransType == DataEntry.TransactionType.Income).ToList().Sum(v => v.EntryValue);
            double totalSpending = Data.Where(d => d.TransType == DataEntry.TransactionType.Spending).ToList().Sum(v => v.EntryValue);

            TotalCreditValue.Text = totalCredit.ToString();
            AvgMoCredit.Text = (totalCredit / totalMonths).ToString();
            AvgWeekCredit.Text = (totalCredit / totalWeeks).ToString();

            TotalIncomeValue.Text = totalIncome.ToString();
            AvgMoIncome.Text = (totalIncome / totalMonths).ToString();
            AvgWeekIncome.Text = (totalIncome / totalWeeks).ToString();

            TotalSpendingValue.Text = totalSpending.ToString();
            AvgMoSpending.Text = (totalSpending / totalMonths).ToString();
            AvgWeekSpending.Text = (totalSpending / totalWeeks).ToString();

            NetGainLossValue.Text = (totalIncome + totalSpending).ToString();
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                SaveFileAs();
            }
            else
            {
                SaveFile();
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = fileFilter;
            if (ofd.ShowDialog() == true)
            {
                FileName = ofd.FileName;
            }

            var dataVals = File.ReadAllLines(FileName);
            for (int i = 1; i < dataVals.Length - 1; ++i)
            {
                var dataEntry = FromCsv(dataVals[i]);
                Data.Add(dataEntry);
            }
        }

        private DataEntry FromCsv(string csvLine)
        {
            var dataEntry = new DataEntry();
            string[] values = csvLine.Split(',');
            dataEntry.Description = values[0];
            dataEntry.Date = Convert.ToDateTime(values[1]);
            dataEntry.TransType = (DataEntry.TransactionType) Enum.Parse(typeof(DataEntry.TransactionType), values[2], true);
            dataEntry.EntryValue = Convert.ToDouble(values[3]);
            return dataEntry;
        }

        private void SaveFileAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = fileFilter;
            if (sfd.ShowDialog() == true)
            {
                FileName = sfd.FileName;
                SaveFile();
            }
        }

        private void SaveFile()
        {
            ValueDataGrid.SelectAllCells();
            ValueDataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, ValueDataGrid);
            ValueDataGrid.UnselectAllCells();
            string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            File.AppendAllText(FileName, result, UnicodeEncoding.UTF8);            
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var dataCollection = ValueDataGrid.ItemsSource as ObservableCollection<DataEntry>;
            var dataEntry = ValueDataGrid.SelectedItems[0] as DataEntry;
            if (dataCollection != null && dataEntry != null)
            {
                dataCollection.Remove(dataEntry);
            }
            
            UpdateStats();
        }
        
        private void ValueEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddEntry();
                UpdateStats();
            }
        }

        private void ValueDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Description.Text = string.Empty;
            ValueEntry.Text = string.Empty;
            RemoveButton.IsEnabled = true;
        }

        private void ValueDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ValueDataGrid.IsKeyboardFocusWithin)
            {
                RemoveButton.IsEnabled = false;
            }
        }
    }
}
