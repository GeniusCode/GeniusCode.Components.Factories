using GeniusCode.Components;

namespace GeniusCode.Factory.ProviderModel.Support
{
    internal class FactoryOutput<T, TResult> : IFactoryOutput<T, TResult>
        where T : class
        where TResult : class, T
    {
        public FactoryOutput(TResult result, bool resultSuccessfull, bool considerResultCached, IFactory<T> sourceUsed)
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
        }

        #region IFactoryOutput<T,TResult> Members

        public IFactory<T> FactoryUsed { get; private set; }

        public TResult Result { get; private set; }

        public bool ConsiderResultCached { get; private set; }

        public bool ResultSuccessful { get; private set; }

        #endregion

        public static IFactoryOutput<T, TResult> NewSuccessfulInstance(TResult result, bool considerResultCached,
                                                                       IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T, TResult>(result, true, considerResultCached, sourceUsed);
        }

        public static IFactoryOutput<T, TResult> NewFailureInstance()
        {
            return NewFailureInstance(null, null);
        }

        public static IFactoryOutput<T, TResult> NewFailureInstance(TResult result, IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T, TResult>(result, false, false, sourceUsed);
        }
    }
}