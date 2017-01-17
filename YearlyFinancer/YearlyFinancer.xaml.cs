using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YearlyFinancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int totalMonths = 12;
        public const int totalWeeks = 52;        
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            // ... Change Window Title.
            this.Title = textBox.Text +
            "[Length = " + textBox.Text.Length.ToString() + "]";
        }

        private void InsertValue(DataEntry.TransactionType transType)
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            var dataEntry = new DataEntry() { Description = "test",
                                              Date = DateTime.Now,
                                              TransType = transType,
                                              EntryValue = value};
            Data.Add(dataEntry);
        }

        private void Credit_Click(object sender, RoutedEventArgs e)
        {
            InsertValue(DataEntry.TransactionType.Credit);
            var total = Data.Where(d => d.TransType == DataEntry.TransactionType.Credit).ToList().Sum(v => v.EntryValue);            
            TotalCreditValue.Text = total.ToString();
            AvgMoCredit.Text = (total / totalMonths).ToString();
            AvgWeekCredit.Text = (total / totalWeeks).ToString();
        }

        private void Debit_Click(object sender, RoutedEventArgs e)
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            double totalIncome = Data.Where(d => d.TransType == DataEntry.TransactionType.Income).ToList().Sum(v => v.EntryValue);
            double totalSpending = Data.Where(d => d.TransType == DataEntry.TransactionType.Spending).ToList().Sum(v => v.EntryValue);
            if (value >= 0)
            {
                InsertValue(DataEntry.TransactionType.Income);
                totalIncome += Convert.ToDouble(ValueEntry.Text);
                TotalIncomeValue.Text = totalIncome.ToString();
                AvgMoIncome.Text = (totalIncome / totalMonths).ToString();
                AvgWeekIncome.Text = (totalIncome / totalWeeks).ToString();
            }
            else
            {
                InsertValue(DataEntry.TransactionType.Spending);
                totalSpending += Convert.ToDouble(ValueEntry.Text);
                TotalSpendingValue.Text = totalSpending.ToString();
                AvgMoSpending.Text = (totalSpending / totalMonths).ToString();
                AvgWeekSpending.Text = (totalSpending / totalWeeks).ToString();
            }

            NetGainLossValue.Text = (totalIncome + totalSpending).ToString();
        }
    }
}
