using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMatrix;

namespace DMC
{
    class CSTRAlgorithm
    {
        Matrix AA;
        Matrix BBb;
        Matrix BBd;
        Matrix CC;
        Matrix DD;
        Matrix u;
        Matrix y;
        public Matrix yMatrix { get { return y; } set { y = value; } }
        Matrix x;
        
        Matrix d_temp;

        public CSTRAlgorithm(Matrix A, Matrix Bb, Matrix Bd, Matrix C, Matrix D, Matrix U, Matrix X, Matrix Y)
        {
            AA = A;
            BBb = Bb;
            BBd = Bd;
            CC = C;
            DD = D;
            u = U;
            x = X;
            y = Y;
            d_temp = Matrix.Zeros(Bd.cols, 1);
        }

        public void StateEquation(Matrix U, Matrix D)
        {
            u = U;
            d_temp = D;
            //x = AA * x + Bb * u + Bd * d(k) + w(k);
            x = AA * x + BBb * u + BBd * d_temp;
            //y = CC * x + v(k);
            y = CC * x;
        }
    }
}
