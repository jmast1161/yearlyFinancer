using System;
using System.Collections.Generic;
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
        private Dictionary<DateTime, double> incomeData;
        private Dictionary<DateTime, double> spendingData;
        private Dictionary<DateTime, double> creditData;

        public MainWindow()
        {
            InitializeComponent();
            incomeData = new Dictionary<DateTime, double>();
            spendingData = new Dictionary<DateTime, double>();
            creditData = new Dictionary<DateTime, double>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            // ... Change Window Title.
            this.Title = textBox.Text +
            "[Length = " + textBox.Text.Length.ToString() + "]";
        }

        private void Credit_Click(object sender, RoutedEventArgs e)
        {
            TotalCreditValue.Text = ValueEntry.Text;
            var value = Convert.ToDouble(ValueEntry.Text);
            creditData.Add(DateTime.Now, value);
        }

        private void Debit_Click(object sender, RoutedEventArgs e)
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            if (value >= 0)
            {
                incomeData.Add(DateTime.Now, value);
                TotalIncomeValue.Text = incomeData.Values.Sum().ToString();
            }
            else
            {
                spendingData.Add(DateTime.Now, value);
                TotalSpendingValue.Text = spendingData.Values.Sum().ToString();
            }

            NetGainLossValue.Text = (incomeData.Values.Sum() + spendingData.Values.Sum()).ToString();
        }
    }
}
