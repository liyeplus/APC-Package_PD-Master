using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMatrix;
using System.Windows.Forms;

namespace DMC
{
    class AlgorithmQP  //QP优化算法
    {
        bool exitflag;      //定义停止标志的bool字段,若为T则停止运行，若为F则继续运行
        public bool Exit_Flag { get { return exitflag; } }      //定义调用Exit_Flag方法，得到exitflag的bool值

        public AlgorithmQP()
        {
        }

        public void qsubp(Matrix Ht, Matrix ct, Matrix Aet, Matrix bet, out Matrix x, out Matrix lambda)
        {
            //Matrix invHt = Ht.Inverse();
            Matrix invHt = ComplexF.pinv(Ht);
            int m = Aet.rows;
            int n = Aet.cols;
            if (m > 0)
            {
                Matrix rb = Aet * invHt * ct + bet;
                lambda = ComplexF.pinv(Aet * invHt * Aet.Transpose()) * rb;
                x = invHt * (Aet.Transpose() * lambda - ct);
            }
            else
            {
                x = (-1) * invHt * ct;
                lambda = Matrix.Zeros(m, 1);
            }
        }

        private double norm(Matrix M)
        {
            double ans = 0;
            if (M.cols != 1)
            {
                MessageBox.Show("dimension error.");
            }
            Matrix temp = M.Transpose() * M;
            ans = Math.Sqrt(temp[0, 0]);

            return ans;
        }

        private double Min(Matrix M, out int Num)
        {
            double min = double.MaxValue;//定义double类型的min变量，赋初值为double可能的最大值
            Num = -1;
            if (M.cols != 1)//如果矩阵M的列数不等于1
            {
                MessageBox.Show("dimension error.");//矩阵尺寸错误
            }
            for (int i = 0; i < M.rows; i++)
            {
                if (min > M[i, 0])
                {
                    min = M[i, 0];
                    Num = i;
                }
            }
            return min;
        }

        public Matrix MainMethod(Matrix H, Matrix c, Matrix Ae, Matrix be, Matrix Ai, Matrix bi, Matrix x0)     //定义主要数学算法的方法
        {
            exitflag = true;        //对停止标志赋初值T，T表示继续运行
            double epsilon = 0.000000001;
            double err = 0.000001;
            double k = 0;
            Matrix x = x0;
            int n = x.rows;
            double kmax = 1000;
            int ne = be.rows;//定义ne是矩阵be的行数
            int ni = bi.rows;//定义bi是矩阵bi的行数
            Matrix lamk = Matrix.Zeros(ne + ni, 1);
            Matrix index = Matrix.Zeros(ni, 1);
            for (int i = 0; i < index.rows; i++)
            {
                index[i, 0] = 1;
            }
            for (int i = 0; i < index.rows; i++)
            {
                Matrix Ai_temp = new Matrix(1, Ai.cols);
                for (int j = 0; j < Ai.cols; j++)
                {
                    Ai_temp[0, j] = Ai[i, j];
                }
                if ((Ai_temp * x).Data[0, 0] > bi[i, 0] + epsilon)
                {
                    index[i, 0] = 0;
                }
            }

            while (k <= kmax)
            {
                Matrix Aee;
                if (ne > 0)
                {
                    Aee = Ae;
                }
                else
                {
                    double[,] Aee_temp = new double[,] { };
                    Aee = new Matrix(Aee_temp);
                }
                for (int i = 0; i < ni; i++)
                {
                    if (index[i, 0] > 0)
                    {
                        Matrix Ai_temp = new Matrix(1, Ai.cols);
                        for (int j = 0; j < Ai.cols; j++)
                        {
                            Ai_temp[0, j] = Ai[i, j];
                        }
                        if ((Aee.rows == 0) && (Aee.cols == 0))
                        {
                            Aee = Ai_temp;
                        }
                        else
                        {
                            Aee = Matrix.Combine(Aee, Ai_temp, Matrix.Direction.vertical);
                        }
                    }
                }
                Matrix gk = H * x + c;
                int m1 = Aee.rows;
                int n1 = Aee.cols;
                Matrix dk;
                this.qsubp(H, gk, Aee, Matrix.Zeros(m1, 1), out dk, out lamk);

                if (norm(dk) <= err)
                {
                    double y = 0;
                    int jk = -1;
                    if (lamk.rows > ne)//lamk.rows是矩阵lamk的行数
                    {
                        int l = lamk.rows - ne;
                        Matrix lamk_t = new Matrix(l, 1);
                        for (int i = 0; i < l; i++)
                        {
                            lamk_t[i, 0] = lamk[ne + i, 0];
                        }
                        y = this.Min(lamk_t, out jk);
                    }
                    if (y >= 0)
                    {
                        exitflag = false;       //唯一一次将停止运行赋值为F，F表示停止运行
                    }
                    else
                    {
                        exitflag = true;
                        for (int i = 0; i < ni; i++)
                        {
                            double sum = 0;
                            for (int j = 0; j < i; j++)
                            {
                                sum = sum + index[j, 0];
                            }
                            if (index[i, 0] != 0 && (sum + ne) == jk)
                            {
                                index[i, 0] = 0;
                                break;
                            }
                        }
                    }
                    k = k + 1;
                }
                else
                {
                    int ti = -1;//?????????????
                    exitflag = true;
                    double alpha = 1.0;
                    double tm = 1.0;
                    for (int i = 0; i < ni; i++)
                    {
                        Matrix Ai_temp = new Matrix(1, Ai.cols);
                        for (int j = 0; j < Ai.cols; j++)
                        {
                            Ai_temp[0, j] = Ai[i, j];
                        }
                        if (index[i, 0] == 0 && (Ai_temp * dk).Data[0, 0] < 0)
                        {
                            double tm1 = (bi[i, 0] - (Ai_temp * x).Data[0, 0]) / (Ai_temp * dk).Data[0, 0];
                            if (tm1 < tm)
                            {
                                tm = tm1;
                                ti = i;
                            }
                        }
                    }
                    alpha = Math.Min(alpha, tm);
                    x = x + alpha * dk;
                    if (tm < 1) { index[ti, 0] = 1; }
                }
                if (exitflag == false) break;
                k = k + 1;
            }
            return x;
        }
    }
}
