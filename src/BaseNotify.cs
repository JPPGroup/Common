using System.Collections.Generic;
using System.ComponentModel;

namespace Jpp.Common
{
    /// <summary>
    /// Simple implementation of the INotifyPropertyChanged interface
    /// </summary>
    public class BaseNotify : INotifyPropertyChanged
    {
        /// <summary>
        /// Event fired when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
