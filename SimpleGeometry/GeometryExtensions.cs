using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
	public static class GeometryExtensions
	{
        #region Select Matrices

	    public static IMatrix2X2<T2> Select<T1, T2>(this IMatrix2X2<T1> source, Func<T1, T2> selector)
	    {
	        return new Matrix2X2<T2>(source.AsEnumerable().Select(selector));
	    }

        #endregion

        #region ClosestPoints

        public static IVector<TNumber> ClosestPoint<TNumber>(this Plane<TNumber> plane, IVector<TNumber> v)
		{
			return v.ClosestPoint(plane);
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this Line<TNumber> line, IVector<TNumber> v)
		{
			return v.ClosestPoint(line);
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this LineSegment<TNumber> lineSegment, IVector<TNumber> v,
														   TNumber epsilon = default(TNumber))
		{
			return v.ClosestPoint(lineSegment, epsilon);
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this Ray<TNumber> ray, IVector<TNumber> v,
														   TNumber epsilon = default(TNumber))
		{
			return v.ClosestPoint(ray, epsilon);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Line<TNumber> line1, Line<TNumber> line2)
		{
			var diff = line2.Point1.Minus(line1.Point1);
			var direction1 = line1.Direction().Normalize();
			var direction2 = line2.Direction().Normalize();
			var m = direction2.CrossProduct(direction1);
			var m2 = m.DotProduct(m);
			var r = diff.CrossProduct(m.DividedBy(m2));
			var t1 = r.DotProduct(direction2);
			var t2 = r.DotProduct(direction1);
			var result1 = line1.Point1.Plus(direction1.Times(t1));
			var result2 = line2.Point1.Plus(direction2.Times(t2));
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(result1, result2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this LineSegment<TNumber> line1, LineSegment<TNumber> line2, TNumber epsilon = default(TNumber))
		{
			var closestPointsOnLines = line1.ToLine().ClosestPoints(line2.ToLine());
			var item1 = closestPointsOnLines.Item1;
			var item2 = closestPointsOnLines.Item2;
			if (!item1.IsWithin(line1.Start, line1.Stop, epsilon))
			{
				var item2Copy = item2;
				item1 = new[] { line1.Start, line1.Stop }.Minimum(v => v.DistanceSquared(item2Copy)).Value;
			}
			if (!item2.IsWithin(line2.Start, line2.Stop, epsilon))
				item2 = new[] { line2.Start, line2.Stop }.Minimum(v => v.DistanceSquared(item1)).Value;
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(item1, item2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this LineSegment<TNumber> line1, Line<TNumber> line2, TNumber epsilon = default(TNumber))
		{
			var closestPointsOnLines = line1.ToLine().ClosestPoints(line2);
			var item1 = closestPointsOnLines.Item1;
			var item2 = closestPointsOnLines.Item2;
			if (!item1.IsWithin(line1.Start, line1.Stop, epsilon))
				item1 = new[] { line1.Start, line1.Stop }.Minimum(v => v.DistanceSquared(item2)).Value;
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(item1, item2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Line<TNumber> line1, LineSegment<TNumber> line2, TNumber epsilon = default(TNumber))
		{
			var result = line2.ClosestPoints(line1, epsilon);
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(result.Item2, result.Item1);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Ray<TNumber> line1, Line<TNumber> line2)
		{
			var closestPointsOnLines = line1.ToLine().ClosestPoints(line2);
			var item1 = closestPointsOnLines.Item1;
			var item2 = closestPointsOnLines.Item2;
			if (!item1.IsWithin(line1))
				item1 = line1.Start;
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(item1, item2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Line<TNumber> line2, Ray<TNumber> line1)
		{
			return line1.ClosestPoints(line2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this LineSegment<TNumber> line1, Ray<TNumber> line2, TNumber epsilon = default(TNumber))
		{
			var closestPointsOnLines = line1.ToLine().ClosestPoints(line2.ToLine());
			var item1 = closestPointsOnLines.Item1;
			var item2 = closestPointsOnLines.Item2;
			if (!item1.IsWithin(line1.Start, line1.Stop, epsilon))
			{
				var item2Copy = item2;
				item1 = new[] { line1.Start, line1.Stop }.Minimum(v => v.DistanceSquared(item2Copy)).Value;
			}
			if (!item2.IsWithin(line2))
				item2 = line2.Start;
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(item1, item2);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Ray<TNumber> line2, LineSegment<TNumber> line1, TNumber epsilon = default(TNumber))
		{
			return line1.ClosestPoints(line2, epsilon);
		}

		public static Tuple<IVector<TNumber>, IVector<TNumber>> ClosestPoints<TNumber>(this Ray<TNumber> line1, Ray<TNumber> line2, TNumber epsilon = default(TNumber))
		{
			var closestPointsOnLines = line1.ToLine().ClosestPoints(line2.ToLine());
			var item1 = closestPointsOnLines.Item1;
			var item2 = closestPointsOnLines.Item2;
			if (line1.IsParallelTo(line2))
				return closestPointsOnLines;
			if (!item1.IsWithin(line1))
				item1 = line1.Start;
			if (!item2.IsWithin(line2))
				item2 = line2.Start;
			return new Tuple<IVector<TNumber>, IVector<TNumber>>(item1, item2);
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this IVector<TNumber> a, IVector<TNumber> b)
		{
			return b.Times(a.DotProduct(b).DividedBy(b.DotProduct(b)));
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this IVector<TNumber> v, Line<TNumber> line)
		{
			var origin = line.Point1;
			var lineDirection = line.Point2.Minus(origin);
			var originVector = v.Minus(origin);
			var projected = originVector.ClosestPoint(lineDirection);
			return origin.Plus(projected);
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this IVector<TNumber> v, Ray<TNumber> ray,
														   TNumber epsilon = default(TNumber))
		{
			IVector<TNumber> result = v.ClosestPoint(new Line<TNumber>(ray.Start, ray.Start.Plus(ray.NormalizedDirection)));
			if (!result.IsWithin(ray))
				return ray.Start;
			return result;
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this IVector<TNumber> v, LineSegment<TNumber> lineSegment,
														   TNumber epsilon = default(TNumber))
		{
			IVector<TNumber> result = v.ClosestPoint(new Line<TNumber>(lineSegment.Start, lineSegment.Stop));
			if (!result.IsWithin(lineSegment.Start, lineSegment.Stop, epsilon))
				return lineSegment.Minimum(v1 => v1.DistanceSquared(result)).Value;
			return result;
		}

		public static IVector<TNumber> ClosestPoint<TNumber>(this IVector<TNumber> v, Plane<TNumber> plane)
		{
			throw new NotImplementedException();
		}

        #endregion ClosestPoints

        #region Line patterns

        public static IEnumerable<IVector<TNumber>> DottedLine<TNumber>(this IEnumerable<TNumber> distances,
																	   Line<TNumber> line, TNumber epsilon)
		{
			IVector<TNumber> direction = line.Direction().Normalize();
			IVector<TNumber> currentPosition = line.Point1;
			yield return currentPosition;
			foreach (TNumber distance in distances)
			{
				TNumber distance1 = distance;
				currentPosition = currentPosition.Plus(direction.Select(t => t.Times(distance1)).ToVector());
				yield return currentPosition;
			}
		}

		public static IEnumerable<IVector<TNumber>> DottedLine<TNumber>(this IEnumerable<TNumber> distances,
																	   LineSegment<TNumber> lineSegment, TNumber epsilon)
		{
			IVector<TNumber> direction = lineSegment.Direction().Normalize();
			IVector<TNumber> currentPosition = lineSegment.Start;
			yield return currentPosition;
			foreach (TNumber distance in distances)
			{
				TNumber distance1 = distance;
				currentPosition = currentPosition.Plus(direction.Select(t => t.Times(distance1)));
				if (!currentPosition.IsWithin(lineSegment.Start, lineSegment.Stop, epsilon))
				{
					yield return lineSegment.Stop;
					yield break;
				}
				yield return currentPosition;
			}
		}

		public static IEnumerable<LineSegment<TNumber>> DashedLines<TNumber>(this IEnumerable<IVector<TNumber>> points)
		{
			IEnumerator<IVector<TNumber>> enumerator = points.GetEnumerator();
			while (enumerator.MoveNext())
			{
				IVector<TNumber> pointA = enumerator.Current;
				if (!enumerator.MoveNext())
					yield break;
				IVector<TNumber> pointB = enumerator.Current;
				yield return new LineSegment<TNumber>(pointA, pointB);
			}
		}

        #endregion

        #region DistanceSquared

        public static TNumber DistanceBetweenSkewLines<TNumber>(this Line<TNumber> line1, Line<TNumber> line2)
        {
            var line1Direction = line1.Direction();
            var line2Direction = line2.Direction();
            var numerator = line1Direction.CrossProduct(line2Direction);
            IVector<TNumber> n = numerator.Normalize();
            var diff = line1.Point1.Minus(line2.Point1);
            var result = n.DotProduct(diff).Abs();
            return result;
        }

        public static TNumber DistanceBetweenSquared<TNumber>(this LineSegment<TNumber> line1, LineSegment<TNumber> line2, TNumber epsilon)
        {
            var closestPoints = line1.ToLine().ClosestPoints(line2.ToLine());
            if (closestPoints.Item1.IsWithin(line1.Start, line1.Stop, epsilon)
                && closestPoints.Item2.IsWithin(line2.Start, line2.Stop, epsilon))
                return closestPoints.Item1.DistanceSquared(closestPoints.Item2);
            var d1 = line1.Start.DistanceSquared(line2, epsilon);
            var d2 = line1.Stop.DistanceSquared(line2, epsilon);
            var d3 = line2.Start.DistanceSquared(line1, epsilon);
            var d4 = line2.Stop.DistanceSquared(line1, epsilon);
            return d1.MinimumWith(d2, d3, d4);
        }

        public static TNumber DistanceSquared<TNumber>(this Plane<TNumber> plane, IVector<TNumber> v)
		{
			return v.DistanceSquared(plane.ClosestPoint(v));
		}

		public static TNumber DistanceSquared<TNumber>(this LineSegment<TNumber> lineSegment, IVector<TNumber> v)
		{
			return v.DistanceSquared(lineSegment.ClosestPoint(v));
		}

		public static TNumber DistanceSquared<TNumber>(this Ray<TNumber> ray, IVector<TNumber> v)
		{
			return v.DistanceSquared(ray.ClosestPoint(v));
		}

		public static TNumber DistanceSquared<TNumber>(this Line<TNumber> line1, Line<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this LineSegment<TNumber> line1, LineSegment<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this LineSegment<TNumber> line1, Line<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this Line<TNumber> line1, LineSegment<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this Ray<TNumber> line1, Line<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this Line<TNumber> line2, Ray<TNumber> line1)
		{
			var points = line2.ClosestPoints(line1);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this LineSegment<TNumber> line1, Ray<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this Ray<TNumber> line2, LineSegment<TNumber> line1)
		{
			var points = line2.ClosestPoints(line1);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this Ray<TNumber> line1, Ray<TNumber> line2)
		{
			var points = line1.ClosestPoints(line2);
			return points.Item1.DistanceSquared(points.Item2);
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, Ray<TNumber> ray)
		{
			return v.DistanceSquared(v.ClosestPoint(ray));
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, LineSegment<TNumber> lineSegment)
		{
			return v.DistanceSquared(v.ClosestPoint(lineSegment));
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, Plane<TNumber> plane)
		{
			return v.DistanceSquared(v.ClosestPoint(plane));
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, Line<TNumber> line)
		{
			return v.ClosestPoint(line).DistanceSquared(v);
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, LineSegment<TNumber> lineSegment,
													   TNumber epsilon)
		{
			return v.ClosestPoint(lineSegment, epsilon).DistanceSquared(v);
		}

		public static TNumber DistanceSquared<TNumber>(this IVector<TNumber> v, IVector<TNumber> u)
		{
			return new LineSegment<TNumber>(v, u).Direction().LengthSquared();
		}

		public static TNumber DistanceSquared<TNumber>(this Line<TNumber> line, IVector<TNumber> v)
		{
			return v.ClosestPoint(line).DistanceSquared(v);
		}

		public static TNumber DistanceSquared<TNumber>(this LineSegment<TNumber> lineSegment, IVector<TNumber> v,
													   TNumber epsilon)
		{
			return v.ClosestPoint(lineSegment, epsilon).DistanceSquared(v);
		}

		#endregion DistanceSquared

		#region Methods for determining if primitives are parallel, orthogonal, skewed, etc.

		public static bool IsParallelTo<TNumber>(this IVector<TNumber> v1, IVector<TNumber> v2)
		{
			var direction1 = v1.Normalize();
			var direction2 = v2.Normalize();
			var result = direction1.CrossProduct(direction2);
			return result.LengthSquared().Equals(0.ConvertTo<int, TNumber>());
		}

		public static bool IsOrthogonalWith<TNumber>(this IVector<TNumber> v1, IVector<TNumber> v2)
		{
			return v1.DotProduct(v2).Equals(0.ConvertTo<int, TNumber>());
		}

		public static bool IsParallelTo<TNumber>(this LineSegment<TNumber> v1, LineSegment<TNumber> v2)
		{
			return v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsSkewedRelativeTo<TNumber>(this LineSegment<TNumber> v1, LineSegment<TNumber> v2)
		{
			return !v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsOrthogonalWith<TNumber>(this LineSegment<TNumber> v1, LineSegment<TNumber> v2)
		{
			return v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsParallelTo<TNumber>(this Line<TNumber> v1, Line<TNumber> v2)
		{
			return v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsSkewedRelativeTo<TNumber>(this Line<TNumber> v1, Line<TNumber> v2)
		{
			return !v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsOrthogonalWith<TNumber>(this Line<TNumber> v1, Line<TNumber> v2)
		{
			return v1.Direction().IsParallelTo(v2.Direction());
		}

		public static bool IsParallelTo<TNumber>(this Ray<TNumber> v1, Ray<TNumber> v2)
		{
			return v1.NormalizedDirection.IsParallelTo(v2.NormalizedDirection);
		}

		public static bool IsSkewedRelativeTo<TNumber>(this Ray<TNumber> v1, Ray<TNumber> v2)
		{
			return !v1.NormalizedDirection.IsParallelTo(v2.NormalizedDirection);
		}

		public static bool IsOrthogonalWith<TNumber>(this Ray<TNumber> v1, Ray<TNumber> v2)
		{
			return v1.NormalizedDirection.IsParallelTo(v2.NormalizedDirection);
		}

        #endregion Methods for determining if primitives are parallel, orthogonal, skewed, etc.

        #region Parallelism

        public static bool IsParallelTo<TNumber>(this Line<TNumber> line, Plane<TNumber> plane)
		{
			return line.Direction().DotProduct(plane.Normal).Equal(0.ConvertTo<int, TNumber>());
		}

		public static bool IsParallelTo<TNumber>(this Plane<TNumber> plane, Line<TNumber> line)
		{
			return line.IsParallelTo(plane);
		}

        #endregion

        #region Contains

        public static bool Contains<TNumber>(this Line<TNumber> line, params LineSegment<TNumber>[] lineSegs)
		{
			return lineSegs.All(lineSeg => lineSeg.Start.IsWithin(line) && lineSeg.Stop.IsWithin(line));
		}

		public static bool Contains<TNumber>(this Line<TNumber> line, params Ray<TNumber>[] rays)
		{
			return rays.All(ray => ray.Start.IsWithin(line) && (ray.Start.Plus(ray.NormalizedDirection)).IsWithin(line));
		}

		public static bool Contains<TNumber>(this Line<TNumber> line, params IVector<TNumber>[] points)
		{
			return points.All(point => point.IsWithin(line));
		}

		public static bool Contains<TNumber>(this Ray<TNumber> ray, params LineSegment<TNumber>[] lineSegs)
		{
			return lineSegs.All(lineSeg => ray.Contains(lineSeg.Start, lineSeg.Stop));
		}

		public static bool Contains<TNumber>(this Ray<TNumber> ray, params Ray<TNumber>[] rays)
		{
			return rays.All(ray2 => ray.Contains(ray2.Start, ray2.Start.Plus(ray2.NormalizedDirection)));
		}

		public static bool Contains<TNumber>(this Ray<TNumber> ray, params IVector<TNumber>[] points)
		{
			return points.All(point => !ray.Start.IsWithin(ray.Start.Plus(ray.NormalizedDirection), point, NumbersUtility<TNumber>.Epsilon));
		}

		public static bool Contains<TNumber>(this LineSegment<TNumber> lineSeg, TNumber epsilon, params IVector<TNumber>[] points)
		{
			return points.All(point => point.IsWithin(epsilon, lineSeg));
		}

		public static bool Contains<TNumber>(this Plane<TNumber> plane, params Line<TNumber>[] lines)
		{
			return lines.All(line => line.IsWithin(plane));
		}

		public static bool Contains<TNumber>(this Plane<TNumber> plane, params LineSegment<TNumber>[] lineSegs)
		{
			return lineSegs.All(lineSeg => lineSeg.ToLine().IsWithin(plane));
		}

		public static bool Contains<TNumber>(this Plane<TNumber> plane, params Ray<TNumber>[] rays)
		{
			return rays.All(ray => ray.ToLine().IsWithin(plane));
		}

		public static bool Contains<TNumber>(this Plane<TNumber> plane, params IVector<TNumber>[] points)
		{
			return points.All(point => point.IsWithin(plane));
		}

		#endregion Contains

		#region IsWithin

		public static bool IsWithin<TNumber>(this LineSegment<TNumber> lineSeg, params Line<TNumber>[] lines)
		{
			return lines.All(line => line.Contains(lineSeg));
		}

		public static bool IsWithin<TNumber>(this Ray<TNumber> ray, params Line<TNumber>[] lines)
		{
			return lines.All(line => line.Contains(ray));
		}

		public static bool IsWithin<TNumber>(this LineSegment<TNumber> lineSeg, params Ray<TNumber>[] rays)
		{
			return rays.All(ray => ray.Contains(lineSeg));
		}

		public static bool IsWithin<TNumber>(this Ray<TNumber> ray, params Ray<TNumber>[] rays)
		{
			return rays.All(ray2 => ray2.Contains(ray));
		}

		public static bool IsWithin<TNumber>(this IVector<TNumber> point, params Ray<TNumber>[] rays)
		{
			return rays.All(ray => ray.Contains(point));
		}

		public static bool IsWithin<TNumber>(this Line<TNumber> line, params Plane<TNumber>[] planes)
		{
			return planes.All(plane => line.IsParallelTo(plane) && line.Point1.IsWithin(plane));
		}

		public static bool IsWithin<TNumber>(this LineSegment<TNumber> lineSeg, params Plane<TNumber>[] planes)
		{
			return planes.All(plane => plane.Contains(lineSeg));
		}

		public static bool IsWithin<TNumber>(this Ray<TNumber> ray, params Plane<TNumber>[] planes)
		{
			return planes.All(plane => plane.Contains(ray));
		}

		public static bool IsWithin<TNumber>(this IVector<TNumber> point, params Plane<TNumber>[] planes)
		{
			return planes.All(plane => plane.Normal.DotProduct(plane.Coordinate.LineTowards(point).Direction().Normalize()).Equal(0.ConvertTo<int, TNumber>()));
		}

		public static bool IsWithin<TNumber>(this IVector<TNumber> point, params Line<TNumber>[] lines)
		{
			return lines.All(line => line.Point1.LineTowards(point).IsParallelTo(line.Point2.LineTowards(point)));
		}

		public static bool IsWithin<TNumber>(this IVector<TNumber> point, TNumber epsilon, params LineSegment<TNumber>[] lineSegs)
		{
			return lineSegs.All(lineSeg => lineSeg.Start.LineTowards(point).IsParallelTo(lineSeg.Stop.LineTowards(point)) &&
				point.IsWithin(lineSeg.Start, lineSeg.Stop, epsilon));
		}

		public static bool IsWithin<TNumber>(this IVector<TNumber> v1, IVector<TNumber> endPoint1,
											  IVector<TNumber> endPoint2, TNumber epsilon)
		{
			for (int i = 0; i < v1.Count; i++)
			{
				if (!v1[i].IsWithin(endPoint1[i], endPoint2[i], epsilon))
					return false;
			}
			return true;
		}

		public static bool IsWithin<TNumber>(this TNumber n1, TNumber similarNumber)
		{
			return n1.IsWithin(similarNumber.Minus(NumbersUtility<TNumber>.Epsilon), similarNumber.Plus(NumbersUtility<TNumber>.Epsilon));
		}

		public static bool IsWithin<TNumber>(this TNumber n1, TNumber bound1, TNumber bound2)
		{
			TNumber lowerBound, upperBound;
			if (bound1.LessThan(bound2))
			{
				lowerBound = bound1;
				upperBound = bound2;
			}
			else
			{
				lowerBound = bound2;
				upperBound = bound1;
			}

			return n1.GreaterThan(lowerBound) && n1.LessThanOrEqual(upperBound);
		}

		public static bool IsWithin<TNumber>(this TNumber n1, TNumber bound1, TNumber bound2, TNumber epsilon)
		{
			TNumber lowerBound, upperBound;
			if (bound1.LessThan(bound2))
			{
				lowerBound = bound1;
				upperBound = bound2;
			}
			else
			{
				lowerBound = bound2;
				upperBound = bound1;
			}

			TNumber smallN1 = n1.Minus(epsilon);
			TNumber bigN1 = n1.Plus(epsilon);

			return bigN1.GreaterThanOrEqual(lowerBound) && smallN1.LessThanOrEqual(upperBound);
		}

        #endregion IsWithin

        #region IntersectionWith

        public static ICircle<TNumber> IntersectionWith<TNumber>(this ISphere<TNumber> sphere1, ISphere<TNumber> sphere2)
	    {
            // Move the spheres so that they are centered at 0, 0, 0.
	        var sphere1Offset = sphere1.Center.Times((-1).Convert().To<TNumber>());
            sphere1 = new Sphere<TNumber>(Vector3<TNumber>.Zero, sphere1.Radius);
            sphere2 = new Sphere<TNumber>(sphere2.Center.Plus(sphere1Offset), sphere2.Radius);

            throw new NotImplementedException();
	    }

		public static IVector<TNumber> IntersectionWith<TNumber>(this LineSegment<TNumber> line, Plane<TNumber> plane)
		{
			var numerator = plane.Normal.Select(n => n.Times((-1).ConvertTo<int, TNumber>())).ToVector().DotProduct(line.Start.Minus(plane.Coordinate));
			var denominator = plane.Normal.DotProduct(line.Direction());
			var parametricParameter = numerator.DividedBy(denominator);
			var result = line.ToParametricEquation()(parametricParameter);
			return line.ClosestPoint(result);
		}

		public static IVector<TNumber> IntersectionWith<TNumber>(this Ray<TNumber> line, Plane<TNumber> plane)
		{
			var numerator = plane.Normal.Select(n => n.Times((-1).ConvertTo<int, TNumber>())).ToVector().DotProduct(line.Start.Minus(plane.Coordinate));
			var denominator = plane.Normal.DotProduct(line.NormalizedDirection);
			var parametricParameter = numerator.DividedBy(denominator);
			var result = line.ToParametricEquation()(parametricParameter);
			return line.ClosestPoint(result);
		}

		public static IVector<TNumber> IntersectionWith<TNumber>(this Line<TNumber> line, Plane<TNumber> plane)
		{
			var numerator = plane.Normal.Select(n => n.Times((-1).ConvertTo<int, TNumber>())).ToVector().DotProduct(line.Point1.Minus(plane.Coordinate));
			var denominator = plane.Normal.DotProduct(line.Direction());
			var parametricParameter = numerator.DividedBy(denominator);
			var result = line.ToParametricEquation()(parametricParameter);
			return result;
		}

		public static IVector<TNumber> IntersectionWith<TNumber>(this Plane<TNumber> plane, LineSegment<TNumber> line)
		{
			return line.IntersectionWith(plane);
		}

		public static IVector<TNumber> IntersectionWith<TNumber>(this Plane<TNumber> plane, Ray<TNumber> line)
		{
			return line.IntersectionWith(plane);
		}

		public static IVector<TNumber> IntersectionWith<TNumber>(this Plane<TNumber> plane, Line<TNumber> line)
		{
			return line.IntersectionWith(plane);
		}

        #endregion IntersectionWith

        #region InvertDirection

        public static Line<TNumber> InvertDirection<TNumber>(this Line<TNumber> line)
		{
			return new Line<TNumber>(line.Point2, line.Point1);
		}

		public static LineSegment<TNumber> InvertDirection<TNumber>(this LineSegment<TNumber> lineSeg)
		{
			return new LineSegment<TNumber>(lineSeg.Stop, lineSeg.Start);
		}

		public static Ray<TNumber> InvertDirection<TNumber>(this Ray<TNumber> ray)
		{
			return new Ray<TNumber>(ray.Start, ray.NormalizedDirection.Times((-1).ConvertTo<int, TNumber>()));
		}

        #endregion

        #region Parametric equations

        public static Func<TNumber, IVector<TNumber>> ToParametricEquation<TNumber>(this Line<TNumber> line)
		{
			return number => line.Point1.Plus((line.Point2.Minus(line.Point1)).Times(number));
		}

		public static Func<TNumber, IVector<TNumber>> ToParametricEquation<TNumber>(this LineSegment<TNumber> lineSeg)
		{
			return number => lineSeg.Start.Plus((lineSeg.Stop.Minus(lineSeg.Start)).Times(number));
		}

		public static Func<TNumber, IVector<TNumber>> ToParametricEquation<TNumber>(this Ray<TNumber> ray)
		{
			return number => ray.Start.Plus(ray.NormalizedDirection.Times(number));
		}

        #endregion

        #region Extension methods for fluently creating geometry (e.g., LineTo, RayTowards)

        public static Line<TNumber> LineTowards<TNumber>(this IVector<TNumber> point1, IVector<TNumber> point2)
		{
			return new Line<TNumber>(point1, point2);
		}

		public static LineSegment<TNumber> LineSegmentTo<TNumber>(this IVector<TNumber> start, IVector<TNumber> stop)
		{
			return new LineSegment<TNumber>(start, stop);
		}

		public static LineSegment<TNumber> ToLineSegment<TNumber>(this Line<TNumber> line)
		{
			return new LineSegment<TNumber>(line.Point1, line.Point2);
		}

		public static Line<TNumber> ToLine<TNumber>(this LineSegment<TNumber> lineSeg)
		{
			return new Line<TNumber>(lineSeg.Start, lineSeg.Stop);
		}

		public static Line<TNumber> ToLine<TNumber>(this Ray<TNumber> ray)
		{
			return new Line<TNumber>(ray.Start, ray.Start.Plus(ray.NormalizedDirection));
		}

		public static Ray<TNumber> RayTowards<TNumber>(this IVector<TNumber> start, IVector<TNumber> towards)
		{
			var direction = towards.Minus(start);
			return new Ray<TNumber>(start, direction);
		}

		#endregion Extension methods for naturally creating geometry (e.g., LineTo, RayTowards)
	}
}
