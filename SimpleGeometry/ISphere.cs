using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleVectors;

namespace SimpleGeometry
{
    public interface ISphere<out TNumber>
    {
        IVector3<TNumber> Center { get; }
        TNumber Radius { get; }
        TNumber Diameter { get; }
        TNumber Circumference { get; }
    }
}
