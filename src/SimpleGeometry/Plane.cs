using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
	public class Plane<TNumber>
	{
		private static Plane<TNumber> _xy;
		public static Plane<TNumber> Xy
		{
			get
			{
				if (_xy == null)
				{
					_xy = new Plane<TNumber>(Vector3<TNumber>.UnitZ, Vector3<TNumber>.Zero);
				}
				return _xy;
			}
		}

		private static Plane<TNumber> _xz;
		public static Plane<TNumber> Xz
		{
			get
			{
				if (_xz == null)
				{
					_xz = new Plane<TNumber>(Vector3<TNumber>.UnitY, Vector3<TNumber>.Zero);
				}
				return _xz;
			}
		}

		private static Plane<TNumber> _yz;
		public static Plane<TNumber> Yz
		{
			get
			{
				if (_yz == null)
				{
					_yz = new Plane<TNumber>(Vector3<TNumber>.UnitX, Vector3<TNumber>.Zero);
				}
				return _yz;
			}
		}

		public IVector<TNumber> Normal { get; private set; }
		public IVector<TNumber> Coordinate { get; private set; }

		public Plane(IVector<TNumber> normal, IVector<TNumber> point)
		{
			Normal = normal;
			Coordinate = point;
		}
	}
}
