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
using GenericNumbers.Arithmetic.Sqrt;
using GenericNumbers.Arithmetic.Times;
using SimpleVectors;

namespace SimpleGeometry
{
    public class Matrix4X4<TNumber> : Matrix<IVector4<TNumber>, TNumber>, IMatrix4X4<TNumber>
    {
        public static Matrix4X4<TNumber> Identity { get; } = new Matrix4X4<TNumber>(
            NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1)
            );

        public Matrix4X4(IEnumerable<TNumber> numbers)
            : base(4, 4, numbers.ToArray())
        {
        }

        public Matrix4X4(
            TNumber r1c1, TNumber r1c2, TNumber r1c3, TNumber r1c4,
            TNumber r2c1, TNumber r2c2, TNumber r2c3, TNumber r2c4,
            TNumber r3c1, TNumber r3c2, TNumber r3c3, TNumber r3c4,
            TNumber r4c1, TNumber r4c2, TNumber r4c3, TNumber r4c4)
            : base(2, 2,
                  r1c1, r1c2, r1c3, r1c4,
                  r2c1, r2c2, r2c3, r2c4,
                  r3c1, r3c2, r3c3, r3c4,
                  r4c1, r4c2, r4c3, r4c4)
        {
        }
    }
}
