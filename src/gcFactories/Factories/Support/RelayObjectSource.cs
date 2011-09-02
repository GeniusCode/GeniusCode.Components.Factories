using System;
using System.ComponentModel;

namespace GeniusCode.Components.Factories.Support
{
    /// <summary>
    /// Represents a source for a value or object instance of a particular type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RelayObjectSource<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Creates an instance of the class by specifying a func.
        /// </summary>
        /// <param name="factory">The factory.</param>
        protected RelayObjectSource(Func<T> factory)
        {
            Factory = factory;
        }

        internal Func<T> Factory { get; private set; }


        /// <summary>
        /// Property that represents the output of the source.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return GetReturnValueForSource(); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets the value to return when the Value property is being accessed
        /// </summary>
        /// <returns></returns>
        protected virtual T GetReturnValueForSource()
        {
            return Factory();
        }

        /// <summary>
        /// Raises the notify property changed event for the Value property
        /// </summary>
        public void RaiseNotifyPropertyChangedForValue()
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Value"));
        }
    }
}