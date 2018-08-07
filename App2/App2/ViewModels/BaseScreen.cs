using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App2.ViewModels
{
    public class BaseScreen : Screen
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }
    }
}
