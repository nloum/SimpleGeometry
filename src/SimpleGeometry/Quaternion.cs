using GenericNumbers;
using GenericNumbers.Arithmetic.Times;
using SimpleVectors;

namespace SimpleGeometry
{
    public class Quaternion<TNumber> : Vector4<IQuaternion<TNumber>, TNumber>,
        IQuaternion<TNumber>
    {
        public static IQuaternion<TNumber> Identity { get; } = new Quaternion<TNumber>(
            NumbersUtility<TNumber>.One, NumbersUtility<TNumber>.Zero, NumbersUtility<TNumber>.Zero, NumbersUtility<TNumber>.Zero);

        public Quaternion(params TNumber[] elements)
            : base(elements)
        {
        }

        public static IQuaternion<TNumber> CreateRotation(IVector3<TNumber> axis, TNumber angle)
        {
            // Algorithm from here: http://www.cprogramming.com/tutorial/3d/quaternions.html

            var halfAngle = angle.DividedBy(2.Convert().To<TNumber>());
            var sinHalfAngle = NumbersUtility<TNumber>.Sine(halfAngle);
            var cosHalfAngle = NumbersUtility<TNumber>.Cosine(halfAngle);

            var x = axis.X.Times(sinHalfAngle);
            var y = axis.Y.Times(sinHalfAngle);
            var z = axis.Z.Times(sinHalfAngle);
            var w = cosHalfAngle;

            return new Quaternion<TNumber>(x, y, z, w);
        }

        public IMatrix4X4<TNumber> ToRotationMatrix()
        {
            // Algorithm from here: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/

            var mat1 = new Matrix4X4<TNumber>(
                W, Z, Y.Negative(), X,
                Z.Negative(), W, X, Y,
                Y, X.Negative(), W, Z,
                X.Negative(), Y.Negative(), Z.Negative(), W);

            var mat2 = new Matrix4X4<TNumber>(
                W, Z, Y.Negative(), X.Negative(),
                Z.Negative(), W, X, Y.Negative(),
                Y, X.Negative(), W, Z.Negative(),
                X, Y, Z, W);

            return mat1.Times(mat2);
        }

        public override void Times(IQuaternion<TNumber> input, out IQuaternion<TNumber> output)
        {
            // Algorithm from here: http://www.cprogramming.com/tutorial/3d/quaternions.html

            var x = W.Times(input.X).Plus(X.Times(input.W)).Plus(Y.Times(input.Z)).Minus(Z.Times(input.Y));
            var y = W.Times(input.Y).Minus(X.Times(input.Z)).Plus(Y.Times(input.W)).Plus(Z.Times(input.X));
            var z = W.Times(input.Z).Plus(X.Times(input.Y)).Minus(Y.Times(input.X)).Plus(Z.Times(input.W));
            var w = W.Times(input.W).Minus(X.Times(input.X)).Minus(Y.Times(input.Y)).Minus(Z.Times(input.Z));

            output = new Quaternion<TNumber>(x, y, z, w);
        }
    }
}
