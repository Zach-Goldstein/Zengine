using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class MatrixTransforms
    {
        public static Matrix ScaleMatrix(float scale)
        {
            Matrix retMatrix = Matrix.Identity;
            retMatrix.M11 = scale;
            retMatrix.M22 = scale;
            retMatrix.M33 = scale;
            return retMatrix;
        }

        public static Matrix TransformMatrix(float x, float y, float z = 0)
        {
            Matrix retMatrix = Matrix.Identity;
            retMatrix.M41 = x;
            retMatrix.M42 = y;
            retMatrix.M43 = z;
            return retMatrix;
        }

        public static Matrix RotationMatrix(double rAngle)
        {
            Matrix retMatrix = Matrix.Identity;
            retMatrix.M11 = (float)Math.Cos(rAngle);
            retMatrix.M12 = (float)-Math.Sin(rAngle);
            retMatrix.M21 = (float)Math.Sin(rAngle);
            retMatrix.M22 = (float)Math.Cos(rAngle);
            return retMatrix;
        }
    }
}
