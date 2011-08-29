using System;
using System.Collections.Generic;

namespace GeniusCode.Framework.Composition.Sources
{
    internal interface IMefExportPlaceHolder<TMefContract, TMetadata> where TMetadata : class
    {
        List<Lazy<TMefContract, TMetadata>> ResolvedExports { get; }
    }
}