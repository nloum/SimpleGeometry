namespace SimpleGeometry
{
	public interface IShape<out TNumber>
	{
		TNumber Area { get; }
	}
}