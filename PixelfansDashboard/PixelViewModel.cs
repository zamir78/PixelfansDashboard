using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PixelfansDashboard
{
    public class PixelViewModel : NotifyViewModelBase
    {
        public PixelViewModel()
        {
            LightCommand = new RelayCommand(() => 
            {
                IsLit = !IsLit;
            });
        }
        private bool _isLit;
        public bool IsLit
        {
            get { return _isLit; }
            set 
            { 
                _isLit = value;
                OnPropertyChanged();
            }
        }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public RelayCommand LightCommand { get; set; }

    }
}
