using SimpleVectors;

namespace SimpleGeometry
{
	public interface IRectangle<out TNumber> : IShape<TNumber>
	{
        IVector2<TNumber> Center { get; }
        IVector2<TNumber> TopLeft { get; }
        IVector2<TNumber> BottomRight { get; }
        IVector2<TNumber> BottomLeft { get; }
        IVector2<TNumber> TopRight { get; }
		TNumber Width { get; }
		TNumber Height { get; }
		TNumber Left { get; }
		TNumber Right { get; }
		TNumber Top { get; }
		TNumber Bottom { get; }
	}
}
