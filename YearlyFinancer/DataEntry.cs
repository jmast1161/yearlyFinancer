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
                SetField(ref description, value, "Description");
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
                SetField(ref date, value, "Date");
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
                SetField(ref transType, value, "TransType");
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
                SetField(ref entryValue, value, "EntryValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
