using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PixelfansDashboard
{
    public class NotifyViewModelBase : INotifyPropertyChanged
    {
        public NotifyViewModelBase()
        {

        }

        private string tooltip;
        public string Tooltip
        {
            get { return tooltip; }
            set
            {
                tooltip = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
