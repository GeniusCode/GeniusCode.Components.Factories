namespace GeniusCode.Components.Factories.Support
{
    internal class FactoryOutput<T, TResult> : IFactoryOutput<T, TResult>
        where T : class
        where TResult : class, T
    {
        public FactoryOutput(TResult result, bool resultSuccessfull, bool considerResultCached, object argsSupplied, IFactory<T> sourceUsed)
        {
            if (sourceUsed == null)
            {
                Result = default(TResult);
                return;
            }

            Result = result;
            FactoryUsed = sourceUsed;
            ResultSuccessful = resultSuccessfull;
            ConsiderResultCached = considerResultCached;
            ArgsSupplied = argsSupplied;
        }

        #region IFactoryOutput<T,TResult> Members

        public IFactory<T> FactoryUsed { get; private set; }

        public object ArgsSupplied { get; private set; }

        public TResult Result { get; private set; }

        public bool ConsiderResultCached { get; private set; }

        public bool ResultSuccessful { get; private set; }

        #endregion

        public static IFactoryOutput<T, TResult> NewSuccessfulInstance(TResult result, object args, bool considerResultCached,
                                                                       IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T, TResult>(result, true, considerResultCached, args, sourceUsed);
        }

        public static IFactoryOutput<T, TResult> NewFailureInstance(object args)
        {
            return NewFailureInstance(null, args, null);
        }

        public static IFactoryOutput<T, TResult> NewFailureInstance(TResult result, object args, IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T, TResult>(result, false, false, args, sourceUsed);
        }
    }
}