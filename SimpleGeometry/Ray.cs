using SimpleVectors;

namespace SimpleGeometry
{
	public class Ray<TNumber>
	{
		public IVector<TNumber> Start { get; private set; }
		public IVector<TNumber> NormalizedDirection { get; private set; }

		public Ray(IVector<TNumber> start, IVector<TNumber> direction)
		{
			Start = start;
			NormalizedDirection = direction;
		}
	}
}
