using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace OpenCAD.Kernel.Application
{
    public interface IViewModel:INotifyPropertyChanged
    {
        string ViewName { get; }
    }

    public abstract class BaseViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            OnPropertyChanged(property.GetMemberInfo().Name);
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ViewName { get { return GetType().Name.Replace("ViewModel", "View"); } }
    }

}
