using System.Collections;
using System.Collections.Generic;
using SimpleVectors;

namespace SimpleGeometry
{
	public class Triangle<TNumber> : IEnumerable<IVector<TNumber>>
	{
		public IVector<TNumber> Point1 { get; private set; }
		public IVector<TNumber> Point2 { get; private set; }
		public IVector<TNumber> Point3 { get; private set; }

		public Triangle(IVector<TNumber> point1, IVector<TNumber> point2, IVector<TNumber> point3)
		{
			Point1 = point1;
			Point2 = point2;
			Point3 = point3;
		}

		public IEnumerator<IVector<TNumber>> GetEnumerator()
		{
			yield return Point1;
			yield return Point2;
			yield return Point3;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
