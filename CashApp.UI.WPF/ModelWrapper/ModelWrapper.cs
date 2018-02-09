using System.Runtime.CompilerServices;

namespace CashApp.UI.WPF.ModelWrapper
{
    public class ModelWrapper<T>: NotifyDataErrorBase
    {
        public T Model { get; }
        public ModelWrapper(T model)
        {
            Model = model;
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
        }
    }
}
