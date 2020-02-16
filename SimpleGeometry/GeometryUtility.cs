using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
	public static class GeometryUtility<TNumber>
	{
		public static IVector<TNumber> Origin { get; private set; }
		public static Plane<TNumber> XyPlane { get; private set; }
		public static Plane<TNumber> XzPlane { get; private set; }
		public static Plane<TNumber> YzPlane { get; private set; }

		static GeometryUtility()
		{
			Origin = Vector3<TNumber>.Zero;
			XyPlane = new Plane<TNumber>(Vector3<TNumber>.UnitZ, Origin);
			XzPlane = new Plane<TNumber>(Vector3<TNumber>.UnitY, Origin);
			YzPlane = new Plane<TNumber>(Vector3<TNumber>.UnitX, Origin);
		}
	}
}
