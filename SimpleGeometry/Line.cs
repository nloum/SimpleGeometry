using System.Collections;
using System.Collections.Generic;
using SimpleVectors;
using GenericNumbers;

namespace SimpleGeometry
{
	public class Line<TNumber> : IEnumerable<IVector<TNumber>>
	{
		public IVector<TNumber> Point1 { get; private set; }
		public IVector<TNumber> Point2 { get; private set; }

		public Line(IVector<TNumber> start, IVector<TNumber> stop)
		{
			Point1 = start;
			Point2 = stop;
		}

		public IVector<TNumber> Direction()
		{
			return Point2.Minus(Point1);
		}

		public Ray<TNumber> ToRay()
		{
			return new Ray<TNumber>(Point1, Direction());
		}

		public IEnumerator<IVector<TNumber>> GetEnumerator()
		{
			yield return Point1;
			yield return Point2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static bool operator ==(Line<TNumber> l1, Line<TNumber> l2)
		{
			return l2.Point1.IsWithin(l1) && l2.Point2.IsWithin(l1);
		}

		public static bool operator !=(Line<TNumber> l1, Line<TNumber> l2)
		{
			return !(l1 == l2);
		}
	}
}
