using System;
using System.ComponentModel;
namespace GeniusCode.FactoryModel
{
    /// <summary>
    /// Represents a source for a value or object instance of a particular type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RelayObjectSource<T> : INotifyPropertyChanged
    {
        internal Func<T> _Factory { get; private set; }



        /// <summary>
        /// Creates an instance of the class by specifying a func.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public RelayObjectSource(Func<T> factory)
        {
            _Factory = factory;
        }

        /// <summary>
        /// Gets the value to return when the Value property is being accessed
        /// </summary>
        /// <returns></returns>
        protected virtual T GetReturnValueForSource()
        {
            return _Factory();
        }


        /// <summary>
        /// Property that represents the output of the source.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get
            {
                return GetReturnValueForSource();
            }
        }


        /// <summary>
        /// Raises the notify property changed event for the Value property
        /// </summary>
        public void RaiseNotifyPropertyChangedForValue()
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Value"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
