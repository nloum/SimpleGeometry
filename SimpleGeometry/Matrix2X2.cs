using System;
using System.Collections.Generic;
using System.Linq;

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
    public class Matrix2X2<TNumber> : Matrix<IVector2<TNumber>, TNumber>, IMatrix2X2<TNumber>
    {
        public static Matrix2X2<TNumber> Identity { get; }  = new Matrix2X2<TNumber>(
            NumbersUtility<TNumber>.Get(1), NumbersUtility<TNumber>.Get(0),
            NumbersUtility<TNumber>.Get(0), NumbersUtility<TNumber>.Get(1)
            );

        public Matrix2X2(IEnumerable<TNumber> numbers)
            : base(2, 2, numbers.ToArray())
        {
        }

        public Matrix2X2(
            TNumber r1c1, TNumber r1c2,
            TNumber r2c1, TNumber r2c2)
            : base(2, 2,
                  r1c1, r1c2,
                  r2c1, r2c2)
        {
        }
    }
}
