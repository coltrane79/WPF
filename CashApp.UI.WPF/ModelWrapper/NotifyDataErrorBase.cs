using CashApp.UI.WPF.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CashApp.UI.WPF.ModelWrapper
{
    public class NotifyDataErrorBase: ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsbyProperty
            = new Dictionary<string, List<string>>();
        public bool HasErrors => _errorsbyProperty.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsbyProperty.ContainsKey(propertyName)
                ? _errorsbyProperty[propertyName]
                : null;
        }
        protected virtual void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                base.OnPropertyChanged(nameof(HasErrors));
            }
        }

        protected void AddErrors(string propertyName, string error)
        {
            if (!_errorsbyProperty.ContainsKey(propertyName))
            {
                _errorsbyProperty[propertyName] = new List<string>();
            }
            if (!_errorsbyProperty[propertyName].Contains(error))
            {
                _errorsbyProperty[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }

        }
        protected void ClearErrors(string propertyName)
        {
            if (_errorsbyProperty.ContainsKey(propertyName))
            {
                _errorsbyProperty.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }       
    }
}
