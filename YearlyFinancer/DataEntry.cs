using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YearlyFinancer
{
    public class DataEntry
    {
        public enum TransactionType
        {
            Income = 0,
            Spending = 1,
            Credit = 2,
        }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransType { get; set; }
        public double Value { get; set; }
    }
}
