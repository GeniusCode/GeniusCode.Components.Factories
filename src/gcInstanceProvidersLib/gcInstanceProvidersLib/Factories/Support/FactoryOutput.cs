using System;

namespace GeniusCode.FactoryModel
{
    internal class FactoryOutput<T, R> : IFactoryOutput<T, R>
        where T : class
        where R : class, T
    {

        public static IFactoryOutput<T,R> NewSuccessfulInstance(R result, bool considerResultCached, IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T,R>(result,true,considerResultCached,sourceUsed);
        }

        public static IFactoryOutput<T, R> NewFailureInstance()
        {
            R instance = default(R);
            IFactory<T> sourceUsed = null;
            return NewFailureInstance(instance, sourceUsed);
        }

        public static IFactoryOutput<T, R> NewFailureInstance(R result, IFactory<T> sourceUsed)
        {
            return new FactoryOutput<T, R>(result, false, false, sourceUsed);
        }

        public FactoryOutput(R result, bool resultSuccessfull, bool considerResultCached,  IFactory<T> sourceUsed)
        {
            if (sourceUsed == null)
            {
                Result = default(R);
                return;
            }

            Result = result;
            FactoryUsed = sourceUsed;
            ResultSuccessful = resultSuccessfull;
            ConsiderResultCached = considerResultCached;
        }

        public IFactory<T> FactoryUsed { get; private set; }

        public R Result { get; private set; }

        public bool ConsiderResultCached { get; private set; }

        public bool ResultSuccessful { get; private set; }
    }
}
