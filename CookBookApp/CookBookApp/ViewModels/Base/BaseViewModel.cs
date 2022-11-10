using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookBookApp.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
    }
}
