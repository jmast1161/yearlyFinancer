﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void Debit_Click(object sender, RoutedEventArgs e)
        {
            var value = Convert.ToDouble(ValueEntry.Text);
            if (value >= 0)
            {
                TotalIncomeValue.Text = value.ToString();
            }
            else
            {
                TotalSpendingValue.Text = value.ToString();
            }
        }
    }
}
