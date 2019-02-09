using EFTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.ViewModels
{
    class ColumnViewModel: INotifyPropertyChanged
    {
        private string _name;
        public string Name { get => _name; set => SetField(ref _name, value); }
        private string _dataType;
        public string DataType { get => _dataType; set => SetField(ref _dataType, value); }

        public bool IsValid { get => !string.IsNullOrEmpty(Name); }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //C# 6 null-safe operator. No need to check for event listeners
            //If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // C# 5 - CallMemberName means we don't need to pass the property's name
        protected bool SetField<T>(ref T field, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
