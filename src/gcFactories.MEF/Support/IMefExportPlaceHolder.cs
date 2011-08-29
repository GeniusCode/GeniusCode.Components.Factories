using System;
using System.Collections.Generic;

namespace GeniusCode.Components.Support
{
    internal interface IMefExportPlaceHolder<TMefContract, TMetadata> where TMetadata : class
    {
        List<Lazy<TMefContract, TMetadata>> ResolvedExports { get; }
    }
}