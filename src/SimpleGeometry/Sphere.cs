using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
    public class Sphere<TNumber> : ISphere<TNumber>
    {
        public Sphere(IVector3<TNumber> center, TNumber radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public IVector3<TNumber> Center { get; }

        public TNumber Radius { get; }

        public TNumber Diameter => Radius.Times(2.Convert().To<TNumber>());

        public TNumber Circumference => Diameter.Times(Math.PI.Convert().To<TNumber>());
    }
}
