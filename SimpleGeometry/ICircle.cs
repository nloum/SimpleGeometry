using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleVectors;

namespace SimpleGeometry
{
    public interface ICircle<out TNumber>
    {
        IVector2<TNumber> Center { get; }
        TNumber Radius { get; }
        TNumber Diameter { get; }
        TNumber Circumference { get; }
    }
}
