using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericNumbers;
using GenericNumbers.Arithmetic.Abs;
using GenericNumbers.Arithmetic.Ceiling;
using GenericNumbers.Arithmetic.DividedBy;
using GenericNumbers.Arithmetic.Floor;
using GenericNumbers.Arithmetic.Minus;
using GenericNumbers.Arithmetic.Plus;
using GenericNumbers.Arithmetic.RaisedTo;
using GenericNumbers.Arithmetic.Remainder;
using GenericNumbers.Arithmetic.Round;
using GenericNumbers.Arithmetic.SpecialNumbers;
using GenericNumbers.Arithmetic.Sqrt;
using GenericNumbers.Arithmetic.Times;
using SimpleVectors;

namespace SimpleGeometry
{
    public class Matrix3X3<TNumber> : Matrix<IVector3<TNumber>, TNumber>, IMatrix3X3<TNumber>
    {
        public static Matrix3X3<TNumber> Identity { get; } = new Matrix3X3<TNumber>(
            NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1)
            );

        public Matrix3X3(IEnumerable<TNumber> numbers)
            : base(3, 3, numbers.ToArray())
        {
        }

        public Matrix3X3(
            TNumber r1c1, TNumber r1c2, TNumber r1c3,
            TNumber r2c1, TNumber r2c2, TNumber r2c3,
            TNumber r3c1, TNumber r3c2, TNumber r3c3)
            : base(2, 2,
                  r1c1, r1c2, r1c3,
                  r2c1, r2c2, r2c3,
                  r3c1, r3c2, r3c3)
        {
        }
    }
}
