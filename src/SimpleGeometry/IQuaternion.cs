using SimpleVectors;

namespace SimpleGeometry
{
    public interface IQuaternion<out TNumber> : IVector4<IQuaternion<TNumber>, TNumber>
    {
        IMatrix4X4<TNumber> ToRotationMatrix();
    }
}
