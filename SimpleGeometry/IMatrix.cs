using System;
using System.Collections.Generic;
using MoreCollections;
using SimpleVectors;

namespace SimpleGeometry
{
    public interface IMatrix<out TVector, out TNumber> : IMultidimensionalArray<TNumber>
        where TVector : IVector<TNumber>
    {
        IReadOnlyList<TVector> Rows { get; }
        IReadOnlyList<TVector> Columns { get; }
    }
}
