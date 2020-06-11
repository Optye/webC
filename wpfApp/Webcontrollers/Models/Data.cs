using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcontrollers.Models
{
    public class Data : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int max;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                RaisePropertyChanged("max");
            }
        }

        private void RaisePropertyChanged(string propname)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
