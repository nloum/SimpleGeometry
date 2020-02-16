using System;

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
    public interface IMatrix3X3<out TNumber> : IMatrix<IVector3<TNumber>, TNumber>
    {
    }
}
