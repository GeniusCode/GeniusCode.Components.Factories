using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniusCode.FactoryModel
{

    public class AbstractFactory<T> : IAbstractFactory<T>
        where T : class
    {

        #region Assets
        private readonly LazyRelayObjectSource<List<IFactory<T>>> _FactoriesFunc;
        #endregion

        #region Constuctors
        public AbstractFactory(IEnumerable<IFactory<T>> providers = null)
        {
            _FactoriesFunc = new LazyRelayObjectSource<List<IFactory<T>>>(() => CreateFactoryList(providers));
        }
        #endregion

        #region Virtual Members

        protected virtual void OnGotInstance<R>(IFactoryOutput<T, R> result) where R : class, T
        {
        }

        protected virtual void AssembleFactoryList(List<IFactory<T>> output)
        {
        }

        #endregion

        #region Interface Implementations


        T IAbstractFactory<T>.GetInstance(object args)
        {
            return GetInstance(args);
        }


        R IAbstractFactory<T>.GetInstance<R>(object args)
        {
            return GetInstance<R>(args);
        }

        #endregion

        #region Helper Methods

        private List<IFactory<T>> CreateFactoryList(IEnumerable<IFactory<T>> providers)
        {
            var sources = (providers ?? new List<IFactory<T>>()).ToList();
            AssembleFactoryList(sources);
            return sources;
        }

        private IFactoryOutput<T, R> TryAcquireFromEnumerableDerived<R>(object args = null, bool autoCache = true)
            where R : class, T
        {

            var q = from t in Factories
                    let result = t.GetInstance<R>(args)
                    where result.ResultSuccessful
                    select result;

            var queryResult = q.FirstOrDefault();

            if (queryResult != null)
            {
                if (autoCache)
                    TryCache<R>(queryResult, args);

                return queryResult;
            }
            return FactoryOutput<T, R>.NewFailureInstance();

        }



        /// <summary>
        /// Extension method to ease api usage by allowing the compiler to detect the type of R implicitly.  Result operator
        /// will point to same object as return IAcquireResult object does.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private IFactoryOutput<T, R> TryAcquireFromEnumerableDerived<R>(out R result, object args = null, bool autoCache = true)
            where R : class, T
        {
            var output = TryAcquireFromEnumerableDerived<R>(args, autoCache);
            result = output.Result;
            return output;
        }


        private bool TryCache<R>(IAcquireResult<R> result, object args = null)
            where R : class, T
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
            get
            {
                return _FactoriesFunc.Value;
            }
        }

        protected T GetInstance(object args = null)
        {
            return GetInstance<T>(args);
        }



        protected R GetInstance<R>(object args) where R : class, T
        {
            R result;
            var acquireResult = TryAcquireFromEnumerableDerived(out result, args);

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
