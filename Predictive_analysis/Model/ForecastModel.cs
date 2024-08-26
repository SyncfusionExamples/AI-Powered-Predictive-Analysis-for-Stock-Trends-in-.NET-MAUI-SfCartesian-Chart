using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictive_analysis
{
    public class ForecastModel : INotifyPropertyChanged
    {
        public double High { get; set; }

        public double Low { get; set; }

        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double Close { get; set; }

        public ForecastModel()
        {

        }
        public ForecastModel(DateTime date, double high, double low, double open, double close)
        {
            Date = date;
            High = high;
            Low = low;
            Open = open;
            Close = close;
        }

        private string name = string.Empty;
        private string desc = string.Empty;
        private double stockPrice;
        private double diffPercent;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return desc; }
            set
            {
                desc = value;
                OnPropertyChanged("Description");
            }
        }

        public double StockPrice
        {
            get { return stockPrice; }
            set
            {
                stockPrice = value;
                OnPropertyChanged("StockPrice");
            }
        }

        public double DifferenceInPercent
        {
            get { return diffPercent; }
            set
            {
                diffPercent = value;
                OnPropertyChanged("DifferenceInPercent");
            }
        }

        private ImageSource? companyIcon;

        public ImageSource? CompanyIcon
        {
            get { return companyIcon; }
            set
            {
                companyIcon = value;
                OnPropertyChanged("CompanyIcon");
            }
        }

        public ForecastModel(string name, string desc, double stkPrice, double percent, ImageSource img)
        {
            Name = name;
            Description = desc;
            StockPrice = stkPrice;
            DifferenceInPercent = percent;
            CompanyIcon = img;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
