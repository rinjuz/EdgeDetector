using System;
using System.ComponentModel;
using System.Linq.Expressions;
using TrainingSetGenerator.ViewModels.Helpers;

namespace TrainingSetGenerator.ViewModels
{
    abstract class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<TVM, T>(TVM vm, Expression<Func<TVM, T>> getProperty)
        {
            OnPropertyChanged(ClassHelper.GetPropertyName(vm, getProperty));
        }

        protected void OnPropertiesChanged<TVM, T1, T2>(TVM vm, Expression<Func<TVM, T1>> getProperty1,
            Expression<Func<TVM, T2>> getProperty2)
        {
            OnPropertyChanged(vm, getProperty1);
            OnPropertyChanged(vm, getProperty2);
        }

        protected void OnPropertiesChanged<TVM, T1, T2, T3>(TVM vm, Expression<Func<TVM, T1>> getProperty1,
            Expression<Func<TVM, T2>> getProperty2, Expression<Func<TVM, T3>> getProperty3)
        {
            OnPropertyChanged(vm, getProperty1);
            OnPropertyChanged(vm, getProperty2);
            OnPropertyChanged(vm, getProperty3);
        }

        protected void OnPropertiesChanged(params string[] propertyNames)
        {
            foreach (var name in propertyNames) OnPropertyChanged(name);
        }

        protected bool IsProperty<TVM, T>(string name, TVM vm, Expression<Func<TVM, T>> getProperty)
        {
            return ClassHelper.GetPropertyName(vm, getProperty) == name;
        }
    }
}
