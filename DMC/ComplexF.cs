using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMatrix;

namespace DMC
{
    class ComplexF
    {
        public float r;// real part
        public float i;// imaginary part

        public ComplexF()// initialize to zero
        {
            r = 0.0F;// constructor logic
            i = 0.0F;
        }

        public ComplexF(float real, float imag)
        {
            r = real;// constructor logic
            i = imag;
        }

        public static ComplexF operator &(ComplexF a, ComplexF b)// use &= for = assignment
        {
            a.r = b.r;
            a.i = b.i;
            return a;
        }

        public static ComplexF operator +(ComplexF a, ComplexF b)// can be used for a += b
        {
            ComplexF c = new ComplexF(a.r, a.i);
            c.r += b.r;
            c.i += b.i;
            return c;
        }

        public static ComplexF operator -(ComplexF a, ComplexF b)// can be used for a -= b
        {
            ComplexF c = new ComplexF(a.r, a.i);
            c.r -= b.r;
            c.i -= b.i;
            return c;
        }

        public static ComplexF operator -(ComplexF a)
        {
            ComplexF c = new ComplexF(-a.r, -a.i);
            return c;
        }

        public static ComplexF operator *(ComplexF a, ComplexF b)// can be used for a *= b
        {
            ComplexF c = new ComplexF();
            c.r = a.r * b.r - a.i * b.i;
            c.i = a.i * b.r + a.r * b.i;
            return c;
        }

        public static ComplexF operator /(ComplexF a, ComplexF b)// can be used for a /= b
        {
            ComplexF c = new ComplexF();
            float r, den;
            if (Math.Abs(b.r) >= Math.Abs(b.i))
            {

                r = b.i / b.r;
                den = b.r + r * b.i;
                c.r = (a.r + r * a.i) / den;
                c.i = (a.i - r * a.r) / den;

            }
            else
            {

                r = b.r / b.i;
                den = b.i + r * b.r;
                c.r = (a.r * r + a.i) / den;
                c.i = (a.i * r - a.r) / den;

            }
            return c;
        }

        public static ComplexF operator /(ComplexF a, float b)// can be used for a /= b
        {
            ComplexF c = new ComplexF();
            c.r = a.r / b;
            c.i = a.i / b;
            return c;
        }

        public static ComplexF operator *(ComplexF a, float b)// can be used for a *= b
        {
            ComplexF c = new ComplexF();
            c.r = a.r * b;
            c.i = a.i * b;
            return c;
        }

        public static ComplexF operator *(float a, ComplexF b)// can be used for a *= b
        {
            ComplexF c = new ComplexF();
            c.r = a * b.r;
            c.i = a * b.i;
            return c;
        }

        public float Real(ComplexF a)
        {
            return a.r;
        }

        public float Imag(ComplexF a)
        {
            return a.i;
        }

        public static ComplexF Set(float real, float imag)
        {
            ComplexF a = new ComplexF(real, imag);
            return a;
        }

        public static ComplexF Conjg(ComplexF a)
        {
            ComplexF c = new ComplexF();
            c.r = a.r;
            c.i = -a.i;
            return c;
        }

        public static float Abs(ComplexF a)
        {
            float x, y, ans, temp;
            x = Math.Abs(a.r);
            y = Math.Abs(a.i);
            if (x == 0.0F)
                ans = y;
            else if (y == 0.0F)
                ans = x;
            else if (x > y)
            {
                temp = y / x;
                ans = (float)(x * Math.Sqrt(1.0F + temp * temp));
            }
            else
            {
                temp = x / y;
                ans = (float)(y * Math.Sqrt(1.0F + temp * temp));
            }
            return ans;
        } // Abs

        public static void CSVD(ComplexF[,] a, int m, int n, int p, int nu, int nv, float[] s, ComplexF[,] u, ComplexF[,] v)
        {
            float[] b = new float[n];
            float[] c = new float[n];
            float cs, eps, eta, f, g, h;
            int i, j, k, k1, L, L1, nM1, np;
            ComplexF q = new ComplexF();
            ComplexF r = new ComplexF();
            float sn;
            float[] t = new float[n];
            float tol, w, x, y, z;
            eta = 1.5E-07F;// eta = the relative machine precision
            tol = 1.5E-31F;// tol = the smallest normalized positive number, divided by eta
            np = n + p;
            nM1 = n - 1;
            L = 0;
            //
            // HOUSEHOLDER REDUCTION
            c[0] = 0.0F;
            k = 0;
            while (true)
            {
                k1 = k + 1;
                //
                // ELIMINATION OF a(i, k), i = k, ..., m-1
                z = 0.0F;
                for (i = k; i < m; i++)
                    z += a[i, k].r * a[i, k].r + a[i, k].i * a[i, k].i;
                b[k] = 0.0F;
                if (z > tol)
                {
                    z = (float)Math.Sqrt(z);
                    b[k] = z;
                    w = ComplexF.Abs(a[k, k]);
                    q &= ComplexF.Set(1.0F, 0.0F);
                    if (w != 0.0F) q &= a[k, k] / w;
                    a[k, k] &= q * (z + w);
                    if (k != np - 1)
                    {
                        for (j = k1; j < np; j++)
                        {
                            q &= ComplexF.Set(0.0F, 0.0F);
                            for (i = k; i < m; i++)
                                q += ComplexF.Conjg(a[i, k]) * a[i, j];
                            q /= z * (z + w);
                            for (i = k; i < m; i++)
                                a[i, j] -= q * a[i, k];
                        }
                    }
                    //
                    // PHASE TRANSFORMATION
                    q &= -ComplexF.Conjg(a[k, k]) / ComplexF.Abs(a[k, k]);
                    for (j = k1; j < np; j++)
                        a[k, j] *= q;
                }
                //
                // ELIMINATION OF a(k, j], j = k+2, ..., n-1
                if (k == nM1) break;
                z = 0.0F;
                for (j = k1; j < n; j++)
                    z += a[k, j].r * (float)a[k, j].r + a[k, j].i * a[k, j].i;
                c[k1] = 0.0F;
                if (z > tol)
                {
                    z = (float)Math.Sqrt(z);
                    c[k1] = z;
                    w = ComplexF.Abs(a[k, k1]);
                    q &= ComplexF.Set(1.0F, 0.0F);
                    if (w != 0.0F) q &= a[k, k1] / w;
                    a[k, k1] &= q * (z + w);
                    for (i = k1; i < m; i++)
                    {
                        q &= ComplexF.Set(0.0F, 0.0F);
                        for (j = k1; j < n; j++)
                            q += ComplexF.Conjg(a[k, j]) * a[i, j];
                        q /= z * (z + w);
                        for (j = k1; j < n; j++)
                            a[i, j] -= q * a[k, j];
                    }
                    //
                    // PHASE TRANSFORMATION
                    q &= -ComplexF.Conjg(a[k, k1]) / ComplexF.Abs(a[k, k1]);
                    for (i = k1; i < m; i++)
                        a[i, k1] *= q;
                }
                k = k1;
            }
            //
            // TOLERANCE FOR NEGLIGIBLE ELEMENTS
            eps = 0.0F;
            for (k = 0; k < n; k++)
            {
                s[k] = b[k];
                t[k] = c[k];
                if ((float)s[k] + t[k] > eps)
                    eps = (float)s[k] + t[k];
            }
            eps *= eta;
            //
            //  INITIALIZATION OF u AND v
            if (nu > 0)
            {
                for (j = 0; j < nu; j++)
                {
                    for (i = 0; i < m; i++)
                        u[i, j] = ComplexF.Set(0.0F, 0.0F);
                    u[j, j] = ComplexF.Set(1.0F, 0.0F);
                }
            }
            if (nv > 0)
            {
                for (j = 0; j < nv; j++)
                {
                    for (i = 0; i < n; i++)
                        v[i, j] = ComplexF.Set(0.0F, 0.0F);
                    v[j, j] = ComplexF.Set(1.0F, 0.0F);
                }
            }
            //
            //  QR DIAGONALIZATION
            for (k = nM1; k >= 0; k--)
            {
                //
                // TEST FOR SPLIT
                while (true)
                {
                    for (L = k; L >= 0; L--)
                    {

                        if (Math.Abs(t[L]) <= eps) goto Test;
                        if (Math.Abs(s[L - 1]) <= eps) break;

                    }
                    //
                    // CANCELLATION OF E(L)
                    cs = 0.0F;
                    sn = 1.0F;
                    L1 = L - 1;
                    for (i = L; i <= k; i++)
                    {
                        f = sn * t[i];
                        t[i] *= cs;
                        if (Math.Abs(f) <= eps) goto Test;
                        h = s[i];
                        w = (float)Math.Sqrt(f * f + h * h);
                        s[i] = w;
                        cs = h / w;
                        sn = -f / w;
                        if (nu > 0)
                        {
                            for (j = 0; j < n; j++)
                            {
                                x = u[j, L1].r;
                                y = u[j, i].r;
                                u[j, L1] = ComplexF.Set(x * cs + y * sn, 0.0F);
                                u[j, i] = ComplexF.Set(y * cs - x * sn, 0.0F);
                            }
                        }
                        if (np == n) continue;
                        for (j = n; j < np; j++)
                        {
                            q &= a[L1, j];
                            r &= a[i, j];
                            a[L1, j] &= q * cs + r * sn;
                            a[i, j] &= r * cs - q * sn;
                        }
                    }
                //
                // TEST FOR CONVERGENCE
                Test: w = s[k];
                    if (L == k) break;
                    //
                    // ORIGIN SHIFT
                    x = s[L];
                    y = s[k - 1];
                    g = t[k - 1];
                    h = t[k];
                    f = ((y - w) * (y + w) + (g - h) * (g + h)) / (2.0F * h * y);
                    g = (float)Math.Sqrt(f * f + 1.0F);
                    if (f < 0.0F) g = -g;
                    f = ((x - w) * (x + w) + (y / (f + g) - h) * h) / x;
                    //
                    // QR STEP
                    cs = 1.0F;
                    sn = 1.0F;
                    L1 = L + 1;
                    for (i = L1; i <= k; i++)
                    {

                        g = t[i];
                        y = s[i];
                        h = sn * g;
                        g = cs * g;
                        w = (float)Math.Sqrt(h * h + f * f);
                        t[i - 1] = w;
                        cs = f / w;
                        sn = h / w;
                        f = x * cs + g * sn;
                        g = g * cs - x * sn;
                        h = y * sn;
                        y = y * cs;
                        if (nv > 0)
                        {
                            for (j = 0; j < n; j++)
                            {
                                x = v[j, i - 1].r;
                                w = v[j, i].r;
                                v[j, i - 1] = ComplexF.Set(x * cs + w * sn, 0.0F);
                                v[j, i] = ComplexF.Set(w * cs - x * sn, 0.0F);
                            }
                        }
                        w = (float)Math.Sqrt(h * h + f * f);
                        s[i - 1] = w;
                        cs = f / w;
                        sn = h / w;
                        f = cs * g + sn * y;
                        x = cs * y - sn * g;
                        if (nu > 0)
                        {
                            for (j = 0; j < n; j++)
                            {
                                y = u[j, i - 1].r;
                                w = u[j, i].r;
                                u[j, i - 1] = ComplexF.Set(y * cs + w * sn, 0.0F);
                                u[j, i] = ComplexF.Set(w * cs - y * sn, 0.0F);
                            }
                        }
                        if (n == np) continue;
                        for (j = n; j < np; j++)
                        {
                            q &= a[i - 1, j];
                            r &= a[i, j];
                            a[i - 1, j] &= q * cs + r * sn;
                            a[i, j] &= r * cs - q * sn;
                        }
                    }
                    t[L] = 0.0F;
                    t[k] = f;
                    s[k] = x;
                }
                //
                //  CONVERGENCE
                if (w >= 0.0F) continue;
                s[k] = -w;
                if (nv == 0) continue;
                for (j = 0; j < n; j++)
                    v[j, k] = -v[j, k];
            }
            //
            //  SORT SINGULAR VALUES
            for (k = 0; k < n; k++)// sortdescending
            {
                g = -1.0F;
                j = k;
                for (i = k; i < n; i++)// sort descending
                {
                    if (s[i] <= g) continue;
                    g = s[i];
                    j = i;
                }
                if (j == k) continue;
                s[j] = s[k];
                s[k] = g;
                if (nv > 0)
                {
                    for (i = 0; i < n; i++)
                    {
                        q &= v[i, j];
                        v[i, j] &= v[i, k];
                        v[i, k] &= q;
                    }
                }
                if (nu > 0)
                {
                    for (i = 0; i < n; i++)
                    {
                        q &= u[i, j];
                        u[i, j] &= u[i, k];
                        u[i, k] &= q;
                    }
                }
                if (n == np) continue;
                for (i = n; i < np; i++)
                {
                    q &= a[j, i];
                    a[j, i] &= a[k, i];
                    a[k, i] &= q;
                }
            }
            //
            //  BACK TRANSFORMATION
            if (nu > 0)
            {
                for (k = nM1; k >= 0; k--)
                {
                    if (b[k] == 0.0F) continue;
                    q &= -a[k, k] / ComplexF.Abs(a[k, k]);
                    for (j = 0; j < nu; j++)
                        u[k, j] *= q;
                    for (j = 0; j < nu; j++)
                    {
                        q &= ComplexF.Set(0.0F, 0.0F);
                        for (i = k; i < m; i++)
                            q += ComplexF.Conjg(a[i, k]) * u[i, j];
                        q /= ComplexF.Abs(a[k, k]) * b[k];
                        for (i = k; i < m; i++)
                            u[i, j] -= q * a[i, k];
                    }
                }
            }
            if (nv > 0 && n > 1)
            {
                for (k = n - 2; k >= 0; k--)
                {
                    k1 = k + 1;
                    if (c[k1] == 0.0F) continue;
                    q &= -ComplexF.Conjg(a[k, k1]) / ComplexF.Abs(a[k, k1]);
                    for (j = 0; j < nv; j++)
                        v[k1, j] *= q;
                    for (j = 0; j < nv; j++)
                    {

                        q &= ComplexF.Set(0.0F, 0.0F);
                        for (i = k1; i < n; i++)
                            q += a[k, i] * v[i, j];
                        q &= q / (ComplexF.Abs(a[k, k1]) * c[k1]);
                        for (i = k1; i < n; i++)
                            v[i, j] -= q * ComplexF.Conjg(a[k, i]);
                    }
                }
            }
        } // CSVD

        public static void SVD(Matrix rxx, out Matrix S_Value, out Matrix U_Vector, out Matrix V_Vector)
        {
            int m = rxx.rows;
            int n = rxx.cols;

            ComplexF[,] rxx_f = new ComplexF[m, n];
            ComplexF[,] u_f = new ComplexF[m, m];
            U_Vector = Matrix.Zeros(m, m);
            ComplexF[,] v_f = new ComplexF[n, n];
            V_Vector = Matrix.Zeros(n, n);
            float[] sValue = new float[m];
            S_Value = Matrix.Zeros(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    rxx_f[i, j] = ComplexF.Set((float)rxx[i, j], 0);
                }
            }

            CSVD(rxx_f, m, n, 0, m, n, sValue, u_f, v_f);

            for (int i = 0; i < Math.Min(m, n); i++)
            {
                S_Value[i, i] = (double)sValue[i];
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    U_Vector[i, j] = (double)u_f[i, j].r;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    V_Vector[i, j] = (double)v_f[i, j].r;
                }
            }
        }

        public static Matrix pinv(Matrix A)
        {
            int m = A.rows;
            int n = A.cols;

            Matrix S, U, V;
            SVD(A, out S, out U, out V);

            Matrix s = Matrix.Zeros(Math.Min(m, n), 1);
            for (int i = 0; i < s.rows; i++)
            {
                s[i, 0] = S[i, i];
            }

            double eps = 0.000001;
            double tol = Math.Max(m, n) * eps;

            int r = 0;
            for (int i = 0; i < s.rows; i++)
            {
                if (s[i, 0] > tol) r++;
            }

            Matrix ss = Matrix.Zeros(r, r);
            for (int i = 0; i < ss.rows; i++)
            {
                ss[i, i] = 1 / s[i, 0];
            }

            Matrix VV = Matrix.Zeros(n, r);
            for (int i = 0; i < VV.rows; i++)
            {
                for (int j = 0; j < VV.cols; j++)
                {
                    VV[i, j] = V[i, j];
                }
            }

            Matrix UU = Matrix.Zeros(m, r);
            for (int i = 0; i < UU.rows; i++)
            {
                for (int j = 0; j < UU.cols; j++)
                {
                    UU[i, j] = U[i, j];
                }
            }

            Matrix UU_t = Matrix.Zeros(r, m);
            for (int i = 0; i < UU_t.rows; i++)
            {
                for (int j = 0; j < UU_t.cols; j++)
                {
                    UU_t[i, j] = UU[j, i];
                }
            }

            Matrix A_t = VV * ss * UU_t;
            return A_t;
        }
    }    
}
