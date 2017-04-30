using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YearlyFinancer
{
    public class DataEntry : INotifyPropertyChanged
    {
        public enum TransactionType
        {
            Income = 0,
            Spending = 1,
            Credit = 2,
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private TransactionType transType;
        public TransactionType TransType
        {
            get
            {
                return transType;
            }

            set
            {
                transType = value;
                OnPropertyChanged("TransType");
            }
        }

        private double entryValue;
        public double EntryValue
        {
            get
            {
                return entryValue;
            }

            set
            {
                entryValue = value;
                OnPropertyChanged("EntryValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
