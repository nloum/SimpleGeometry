using System.Collections;
using System.Collections.Generic;
using SimpleVectors;
using GenericNumbers;

namespace SimpleGeometry
{
	public class LineSegment<TNumber> : IEnumerable<IVector<TNumber>>
	{
		public IVector<TNumber> Start { get; private set; }
		public IVector<TNumber> Stop { get; private set; }

		public LineSegment(IVector<TNumber> start, IVector<TNumber> stop)
		{
			Start = start;
			Stop = stop;
		}

		public IVector<TNumber> Direction()
		{
			return Stop.Minus(Start);
		}

		public Ray<TNumber> ToRay()
		{
			return new Ray<TNumber>(Start, Direction());
		}

		public IEnumerator<IVector<TNumber>> GetEnumerator()
		{
			yield return Start;
			yield return Stop;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
