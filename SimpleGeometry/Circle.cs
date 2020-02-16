using System;

using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
    public class Circle<TNumber> : ICircle<TNumber>
    {
        public Circle(IVector2<TNumber> center, TNumber radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public IVector2<TNumber> Center { get; }

        public TNumber Radius { get; }

        public TNumber Diameter => Radius.Times(2.Convert().To<TNumber>());

        public TNumber Circumference => Diameter.Times(Math.PI.Convert().To<TNumber>());
    }
}
