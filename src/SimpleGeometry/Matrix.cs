using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using GenericNumbers;
using GenericNumbers.Arithmetic.Times;
using GenericNumbers.Relational;
using SimpleVectors;

using MoreCollections;

namespace SimpleGeometry
{
    public class Matrix<TVector, TNumber> : MultidimensionalArray<TNumber>, IMatrix<TVector, TNumber>,
        ITimes<IMatrix<TVector, TNumber>>
        where TVector : IVector<TNumber>
    {
        public Matrix(int rows, int cols, params TNumber[] elements)
            : base(new [] { rows, cols }, elements)
        {
            this.Rows = new Subset(this, 0);
            this.Columns = new Subset(this, 1);
        }

        public IReadOnlyList<TVector> Columns { get; }

        public IReadOnlyList<TVector> Rows { get; }
        
        public void Times(IMatrix<TVector, TNumber> input, out IMatrix<TVector, TNumber> output)
        {
            if (Columns.Count != input.Rows.Count) throw new ArgumentException("Invalid dimensions");
            var resultRows = Rows.Count;
            var resultCols = input.Columns.Count;
            var resultIndex = 0;
            var result = new TNumber[resultRows * resultCols];
            for (var resultRow = 0; resultRow < resultRows; resultRow++)
            {
                for (var resultCol = 0; resultCol < resultCols; resultCol++)
                {
                    TNumber sum = NumbersUtility<TNumber>.Zero;
                    for (var sourceIndex = 0; sourceIndex < Columns.Count; sourceIndex++)
                    {
                        sum = sum.Plus(this[resultRow, sourceIndex].Times(input[sourceIndex, resultCol]));
                    }
                    result[resultIndex] = sum;
                    resultIndex++;
                }
            }

            output = new Matrix<TVector, TNumber>(resultRows, resultCols, result);
        }

        private class Subset : IReadOnlyList<TVector>
        {
            private readonly Matrix<TVector, TNumber> source;

            private readonly int varyBy;

            public Subset(Matrix<TVector, TNumber> source, int varyBy)
            {
                this.source = source;
                this.varyBy = varyBy;

                if (varyBy != 0 && varyBy != 1)
                    throw new ArgumentException("This is a 2D matrix only");
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<TVector> GetEnumerator()
            {
                for (var i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Gets the number of elements in the collection.
            /// </summary>
            /// <returns>
            /// The number of elements in the collection. 
            /// </returns>
            public int Count => this.source.Dimensions[this.varyBy];

            /// <summary>
            /// Gets the element at the specified index in the read-only list.
            /// </summary>
            /// <returns>
            /// The element at the specified index in the read-only list.
            /// </returns>
            /// <param name="index">The zero-based index of the element to get. </param>
            public TVector this[int index]
            {
                get
                {
                    if (this.varyBy == 0)
                        return VectorUtil<TVector, TNumber>.Create(this.source.Elements(-1, index));
                    else if (this.varyBy == 1) return VectorUtil<TVector, TNumber>.Create(this.source.Elements(index, -1));
                    else throw new ArgumentException("This is a 2D matrix only");
                }
            }
        }
    }
}
