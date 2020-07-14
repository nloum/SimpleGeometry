using GenericNumbers;
using SimpleVectors;

namespace SimpleGeometry
{
	public class Rectangle
	{
		public static IRectangle<TNumber> Create<TNumber>(IVector2<TNumber> topLeft, IVector2<TNumber> bottomRight)
		{
			var width = topLeft.X.Minus(bottomRight.X).Abs();
			var height = topLeft.Y.Minus(bottomRight.Y).Abs();
			var area = width.Times(height);

			var left = topLeft.X.MinimumWith(bottomRight.X);
			var right = topLeft.X.MaximumWith(bottomRight.X);
			var top = topLeft.Y.MinimumWith(bottomRight.Y);
			var bottom = topLeft.Y.MaximumWith(bottomRight.Y);

			var center = Vector2.Create(left.MeanWith(right), top.MeanWith(bottom));

			var topRight = Vector2.Create(top, right);
			var bottomLeft = Vector2.Create(bottom, left);

			return new Rectangle<TNumber>(area, center, topLeft, topRight, bottomLeft, bottomRight, width, height, left, right, top, bottom);
		}

		public static IRectangle<TNumber> Create<TNumber>(IVector2<TNumber> topLeft, TNumber width, TNumber height)
		{
			var area = width.Times(height);
			var corner2 = Vector2.Create(topLeft[0].Plus(width), topLeft[0].Plus(height));

			var left = topLeft.X.MinimumWith(corner2.X);
			var right = topLeft.X.MaximumWith(corner2.X);
			var top = topLeft.Y.MinimumWith(corner2.Y);
			var bottom = topLeft.Y.MaximumWith(corner2.Y);

			var center = Vector2.Create(left.MeanWith(right), top.MeanWith(bottom));

			var bottomRight = Vector2.Create(bottom, right);
			var topRight = Vector2.Create(top, right);
			var bottomLeft = Vector2.Create(bottom, left);

			return new Rectangle<TNumber>(area, center, topLeft, topRight, bottomLeft, bottomRight, width, height, left, right, top, bottom);
		}
	}

	internal class Rectangle<TNumber> : IRectangle<TNumber>
	{
		public TNumber Area { get; private set; }
		public IVector2<TNumber> TopLeft { get; private set; }
		public IVector2<TNumber> BottomRight { get; private set; }
		public IVector2<TNumber> BottomLeft { get; private set; }
		public IVector2<TNumber> TopRight { get; private set; }
		public TNumber Width { get; private set; }
		public TNumber Height { get; private set; }
		public TNumber Left { get; private set; }
		public TNumber Right { get; private set; }
		public TNumber Top { get; private set; }
		public TNumber Bottom { get; private set; }
		public IVector2<TNumber> Center { get; private set; }

		public Rectangle(TNumber area, IVector2<TNumber> center, IVector2<TNumber> topLeft, IVector2<TNumber> topRight, IVector2<TNumber> bottomLeft, IVector2<TNumber> bottomRight, TNumber width, TNumber height, TNumber left, TNumber right, TNumber top, TNumber bottom)
		{
			Area = area;
			Center = center;
			TopLeft = topLeft;
			TopRight = topRight;
			BottomLeft = bottomLeft;
			BottomRight = bottomRight;
			Width = width;
			Height = height;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}
	}
}
