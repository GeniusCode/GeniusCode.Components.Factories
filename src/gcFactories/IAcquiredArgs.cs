using System;

namespace GeniusCode.Components
{
    public interface IAcquiredArgs<T>
    {
        Action<T> GetOnAcquiredAction();
    }
}