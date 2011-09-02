using System;
using System.Collections.Generic;
using System.Linq;
using GeniusCode.Components.Factories.Support;

namespace GeniusCode.Components
{
    public class AbstractFactory<T> : IAbstractFactory<T>
        where T : class
    {
        #region Assets

        private readonly LazyRelayObjectSource<List<IFactory<T>>> _factoriesFunc;

        #endregion

        #region Constuctors

        public AbstractFactory(IEnumerable<IFactory<T>> providers)
        {
            _factoriesFunc = new LazyRelayObjectSource<List<IFactory<T>>>(() => CreateFactoryList(providers));
        }

        #endregion

        protected virtual void OnGotInstance<TResult>(IFactoryOutput<T, TResult> result) where TResult : class, T
        {
        }

        protected virtual void AssembleFactoryList(List<IFactory<T>> output)
        {
        }

        #region Interface Implementations

        T IAbstractFactory<T>.GetInstance(object args)
        {
            return GetInstance(args);
        }


        TResult IAbstractFactory<T>.GetInstance<TResult>(object args)
        {
            return GetInstance<TResult>(args);
        }

        #endregion

        #region Helper Methods

        private List<IFactory<T>> CreateFactoryList(IEnumerable<IFactory<T>> providers)
        {
            List<IFactory<T>> sources = (providers ?? new List<IFactory<T>>()).ToList();
            AssembleFactoryList(sources);
            return sources;
        }

        private IFactoryOutput<T, TResult> TryAcquireFromEnumerableDerived<TResult>(object args = null,
                                                                                    bool autoCache = true)
            where TResult : class, T
        {
            IEnumerable<IFactoryOutput<T, TResult>> q = from t in Factories
                                                        let result = t.GetInstance<TResult>(args)
                                                        where result.ResultSuccessful
                                                        select result;

            IFactoryOutput<T, TResult> queryResult = q.FirstOrDefault();

            if (queryResult != null)
            {
                if (autoCache)
                    TryCache(queryResult, args);

                return queryResult;
            }
            return FactoryOutput<T, TResult>.NewFailureInstance();
        }


        /// <summary>
        /// Extension method to ease api usage by allowing the compiler to detect the type of R implicitly.  Result operator
        /// will point to same object as return IAcquireResult object does.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="args"></param>
        /// <param name="autoCache"></param>
        /// <returns></returns>
        private IFactoryOutput<T, TResult> TryAcquireFromEnumerableDerived<TResult>(out TResult result,
                                                                                    object args = null,
                                                                                    bool autoCache = true)
            where TResult : class, T
        {
            IFactoryOutput<T, TResult> output = TryAcquireFromEnumerableDerived<TResult>(args, autoCache);
            result = output.Result;
            return output;
        }


        private static bool TryCache<TResult>(IAcquireResult<TResult> result, object args = null)
            where TResult : class, T
        {
            return false;

            //if (result.ConsiderResultCached) return false;

            //var wasCached = (from t in Factories.OfType<IInstanceCache<T>>()
            //                 where t.PerformCache<R>(result.Result, args) == true
            //                 select t).FirstOrDefault();

            //return wasCached != null;
        }

        #endregion

        #region protected members

        protected List<IFactory<T>> Factories
        {
            get { return _factoriesFunc.Value; }
        }

        protected T GetInstance(object args = null)
        {
            return GetInstance<T>(args);
        }


        protected TResult GetInstance<TResult>(object args) where TResult : class, T
        {
            TResult result;
            IFactoryOutput<T, TResult> acquireResult = TryAcquireFromEnumerableDerived(out result, args);

            if (acquireResult.ResultSuccessful)
            {
                OnGotInstance(acquireResult);
                return result;
            }

            throw new Exception("Value was not acquired");
        }

        #endregion
    }
}