using CMatrix;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace DMC
{
    class DMCAlgorithm
    {
        /// <summary>
        /// basic setting of DMC
        /// </summary>
        public double Ts;// sampling period
        public int nu; // Dimension of input vector 输入个数
        int nd;// Dimension of disturb vector 干扰个数
        public int ny; // Dimension of output vector 输出个数
        public int P; // prediction horizon 预测步长
        public int m; // control horizon 控制步长
        public int N;// N = tfinal/delt;
        /// <summary>
        /// Initiation of DMC 
        /// </summary>
        Matrix A;
        Matrix Bb;
        Matrix Bd;
        Matrix C;
        Matrix D;
        Matrix B;
        Matrix Su;//(rows, cols) = (ny*N, nu) Step response of input U
        Matrix Sd;//(rows, cols) = (ny*N, nd) Step response of disturbance D
        Matrix Mss;//(rows, cols) = (ny*N, ny*N)
        Matrix M;//(rows, cols) = (ny*P, ny*N)
        Matrix SSd;//(rows, cols) = (ny*P, nd)
        Matrix SSu;//(rows, cols) = (ny*P, nu*m)
        Matrix SSu_T;
        Matrix Mb;//(rows, cols) = (ny*P, ny*N)
        Matrix SSdb;//(rows, cols) = (ny*P, nd)
        Matrix SSub;//(rows, cols) = (ny*P, nu*m)
        Matrix Kmpc;
        Matrix KF;//(rows, cols) = (ny*N, ny)
        public Matrix KFValue { get { return KF; } set { KF = value; } }
        Matrix SP;
        Matrix R;//(rows, cols) = (nu*P, 1)
        Matrix Cc;
        Matrix Cb;
        Matrix Cu;
        Matrix c;//(rows, cols) = (ny, ny*N)
        public Matrix Y_pre;
        Matrix Ep;
        Matrix G;
        Matrix Gamma_y ;
        public Matrix yGammaValue { get { return Gamma_y; } set { Gamma_y = value; } }
        Matrix Gamma_u;
        public Matrix uGammaValue { get { return Gamma_u; } set { Gamma_u = value; } }
        Matrix Gamma_y_T;
        Matrix Gamma_u_T;
        Matrix H;
        //Matrix H_T;
        Matrix I;
        Matrix d;
        public Matrix dValue { get { return d; } set { d = value; } }
        /// <summary>
        /// Simulation setting
        /// (state space and simulation initialization)
        /// </summary>
        Matrix d_pre;
        Matrix delta_d;
        Matrix delta_u;
        Matrix u;

        public Matrix uMatrix { get { return u; } set { u = value; } }
        public Matrix u_C;

        public Matrix u_CMatrix { get { return u_C; } set { u_C = value; } }
        Matrix y;
        /// <summary>
        /// Constraint setting
        /// </summary>
        public Matrix Umax;
        public Matrix Umin;
        Matrix dUmax;
        Matrix dUmin;
        public Matrix Ymax;
        public Matrix Ymin;
        Matrix b;
        //Untitled2.chenjie XYZ = new Untitled2.chenjie();
        AlgorithmQP CJ = new AlgorithmQP();
        bool DMC_Init_falg = true;
        public bool InitFlag { get { return DMC_Init_falg; } set { DMC_Init_falg = value; } }
        bool AutoPause;
        public bool PauseFlag { get { return AutoPause; } }

        //CV1-5.flag
        bool CV1flag;
        bool CV2flag;
        bool CV3flag;
        bool CV4flag;
        bool CV5flag;
        bool CV6flag;

        //public double hh;//用于调整QP

        public DMCAlgorithm(double ts, int Nu, int Nd, int Ny, int p, int mm, int n, Matrix a, Matrix b, Matrix cc, Matrix dd, Matrix bb, Matrix bd)
        {
            Ts = ts;
            nu = Nu;
            nd = Nd;
            ny = Ny;
            P = p;
            m = mm;
            N = n;

            A = a;
            B = b;
            C = cc;
            D = dd;
            Bb = bb;
            Bd = bd;

            Su = new Matrix(ny * N, nu);//(rows, cols) = (ny*N, nu)
            Sd = new Matrix(ny * N, nd);//(rows, cols) = (ny*N, nd)
            Mss = Matrix.Zeros(ny * N);//(rows, cols) = (ny*N, ny*N)
            M = new Matrix(ny * P, ny * N);//(rows, cols) = (ny*P, ny*N)
            SSd = new Matrix(ny * P, nd);//(rows, cols) = (ny*P, nd)
            SSu = new Matrix(ny * P, nu * m);//(rows, cols) = (ny*P, nu*m)
            Kmpc = new Matrix(nu, ny * P);
            KF = new Matrix(ny * N, ny);//(rows, cols) = (ny*N, ny)

            SP = new Matrix(ny, 1);
            R = new Matrix(ny * P, 1);//(rows, cols) = (nu*P, 1)
            Cc = new Matrix(ny, ny);
            c = Matrix.Zeros(ny, ny * N);//(rows, cols) = (ny, ny*N)

            Gamma_y = Matrix.Zeros(ny * P, ny * P);
            Gamma_u = Matrix.Zeros(nu * m, nu * m);

            U_StepResponse();
            D_StepResponse();
            InitializeDMC(Su, Sd);

            Y_pre = Matrix.Zeros(ny * N, 1);//(ny * N, 1)
            Ep = new Matrix(nu * P, 1);//(nu*P, 1)

            delta_u = Matrix.Zeros(nu, 1);
            u = Matrix.Zeros(nu, 1);
            u_C = Matrix.Zeros(nu, 1);

            d_pre = Matrix.Zeros(nd, 1);
            delta_d = Matrix.Zeros(nd, 1);
            d = Matrix.Zeros(nd, 1);
        }

        public Matrix StepResponseModel(Matrix TextVector, int Level)
        {
            Matrix test_u = TextVector;
            Matrix x = Matrix.Zeros(A.cols, 1);
            Matrix y = Matrix.Zeros(A.cols, 1);
            Matrix Su = new Matrix(ny * N, 1);
            for (int i = 0; i < N; i++)
            {
                x = A * x + B * test_u;
                y = C * x;
                for (int j = 0; j < ny; j++)
                {
                    Su[(ny * i) + j, 0] = y[j, 0];
                }
            }
            return Su;
        }

        private void U_StepResponse()
        {
            Matrix Su_temp = new Matrix(ny * N, 1);
            for (int i = 0; i < nu; i++)
            {
                Matrix test = Matrix.Zeros(B.cols, 1);
                test[i, 0] = 1;
                Su_temp = StepResponseModel(test, 3);
                for (int j = 0; j < ny * N; j++)
                {
                    Su.Data[j, i] = Su_temp.Data[j, 0];
                }
            }
        }

        private void D_StepResponse()
        {
            Matrix Sd_temp = new Matrix(ny * N, 1);
            for (int i = 0; i < nd; i++)
            {
                Matrix test = Matrix.Zeros(B.cols, 1);
                test[nu + i, 0] = 1;
                Sd_temp = StepResponseModel(test, 3);
                for (int j = 0; j < ny * N; j++)
                {
                    Sd.Data[j, i] = Sd_temp.Data[j, 0];
                }
            }
        }

        public void InitializeDMC(Matrix Su, Matrix Sd)
        {
            //Structural system output matrix
            for (int i = 0; i < ny; i++)
            {
                c[i, i] = 1;
            }
            //Structure controlled output matrix
            for (int i = 0; i < ny; i++)
            {
                Cc[i, i] = 1;
            }
            //Initiate Mss
            for (int i = 0; i < ny * (N - 1); i++)
            {
                Mss[i, i + ny] = 1;
            }
            for (int i = 0; i < ny; i++)
            {
                Mss[(ny * (N - 1) + i), (ny * (N - 1) + i)] = 1;
            }
            //Structure M
            Matrix M_temp = Cc * c * Mss;
            for (int k = 0; k < P; k++)
            {
                for (int i = 0; i < M_temp.rows; i++)
                {
                    for (int j = 0; j < M_temp.cols; j++)
                    {
                        M[M_temp.rows * k + i, j] = M_temp[i, j];
                    }
                }
                M_temp = M_temp * Mss;
            }
            //Structure SSd
            Matrix SSd_temp_RE = Cc * c;
            Matrix SSd_temp = SSd_temp_RE * Sd;
            for (int k = 0; k < P; k++)
            {
                for (int i = 0; i < SSd_temp.rows; i++)
                {
                    for (int j = 0; j < SSd_temp.cols; j++)
                    {
                        SSd[SSd_temp.rows * k + i, j] = SSd_temp[i, j];
                    }
                }
                SSd_temp_RE = SSd_temp_RE * Mss;
                SSd_temp = SSd_temp_RE * Sd;
            }
            // Structure SSu
            Matrix SSu_temp_RE = Cc * c;
            Matrix SSu_temp = SSu_temp_RE * Su;
            for (int l = 0; l < m; l++)
            {
                for (int k = 0; k < P; k++)
                {
                    for (int i = 0; i < SSu_temp.rows; i++)
                    {
                        for (int j = 0; j < SSu_temp.cols; j++)
                        {
                            if (l > k)
                                SSu[SSu_temp.rows * k + i, SSu_temp.cols * l + j] = 0;
                            else
                                SSu[SSu_temp.rows * k + i, SSu_temp.cols * l + j] = SSu_temp[i, j];
                        }
                    }
                    if (l > k) { }
                    else
                    {
                        SSu_temp_RE = SSu_temp_RE * Mss;
                        SSu_temp = SSu_temp_RE * Su;
                    }
                }
                SSu_temp_RE = Cc * c;
                SSu_temp = SSu_temp_RE * Su;
            }
            //  Structure Gamma_y and Gamma_u
            for (int i = 0; i < Gamma_y.cols; i++)
            {
                //Gamma_y[i, i] = (double)1 / 5;
                Gamma_y[i, i] = (double)1 / P;
            }
            for (int i = 0; i < Gamma_u.cols; i++)
            {
                //Gamma_u[i, i] = (double)1 / 5;
                Gamma_u[i, i] = (double)1 / (m - 2);
            }
            // Structure Kmpc
            I = Matrix.Zeros(SSu_temp.cols, m * SSu_temp.cols);
            for (int i = 0; i < I.rows; i++)
            {
                I[i, i] = 1;
            }
            double[,] SSu_T_temp = Matrix.Transpose(SSu.Data);
            SSu_T = new Matrix(SSu_T_temp);
            double[,] Gamma_y_T_temp = Matrix.Transpose(Gamma_y.Data);
            Gamma_y_T = new Matrix(Gamma_y_T_temp);
            double[,] Gamma_u_T_temp = Matrix.Transpose(Gamma_u.Data);
            Gamma_u_T = new Matrix(Gamma_u_T_temp);
            //Structure H
            Matrix H11 = SSu_T * Gamma_y_T;
            Matrix H12 = Gamma_y * SSu;
            Matrix H1 = H11 * H12;
            Matrix H2 = Gamma_u_T * Gamma_u;
            H = 1.5* (H1 + H2);//yktest 0.1; woodberry 1.5; 3C4M2D 0.4; 瘦系统 1 ；oil 5
            //Structure KF
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    KF[ny * i + j, j] = 1;
                }
            }
        }

        public void SetPoint(double[] r)
        {
            if (r.Count() == ny)
            {
                for (int j = 0; j < ny; j++)
                {
                    SP[j, 0] = r[j];
                }
            }
            else
            {
                MessageBox.Show("Dimension error of controlled variable.");
            }
            //----------------------------------------------------------------------------------------------------
            if (r.Count() == ny)
            {
                for (int i = 0; i < P; i++)
                {
                    for (int j = 0; j < ny; j++)
                    {
                        R[ny * i + j, 0] = r[j];
                    }
                }
            }
            else
            {
                MessageBox.Show("Dimension error of controlled variable.");
            }
        }

        public void SetDisturb(double[] D)
        {
            if (D.Count() == nd)
            {
                for (int j = 0; j < nd; j++)
                {
                    d[j, 0] = D[j];
                }
            }
            else
            {
                MessageBox.Show("Dimension error of disturbance variable.");
            }
        }

        public void SetLimit(double[,] dumax, double[,] dumin, double[,] umax, double[,] umin, double[,] ymax, double[,] ymin)
        {
            dUmax = new Matrix(dumax);
            dUmin = new Matrix(dumin);
            Umax = new Matrix(umax);
            Umin = new Matrix(umin);
            Ymax = new Matrix(ymax);
            Ymin = new Matrix(ymin);
        }

        public void SetY(Matrix yMatrix)
        {
            y = yMatrix;
        }

        public void control()
        {
            delta_d = d - d_pre;
            for (int i = 0; i < nd; i++)
            {
                d_pre[i, 0] = d[i, 0];
            }
            Y_pre = Mss * Y_pre + Su * delta_u + Sd * delta_d + 0.1 * KF * (y - c * Y_pre);
            Ep = R - M * Y_pre - SSd * delta_d;
            #region
            if (CV1flag)
            {
                for (int i = 0; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            if (CV2flag)
            {
                for (int i = 1; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            if (CV3flag)
            {
                for (int i = 2; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            if (CV4flag)
            {
                for (int i = 3; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            if (CV5flag)
            {
                for (int i = 4; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            if (CV6flag)
            {
                for (int i = 5; i < Ep.rows; i = i + 6)
                {
                    Ep[i, 0] = 0;
                }
            }
            #endregion
            G = (-2) * SSu_T * Gamma_y_T * Gamma_y * Ep;
            //Constrained situation
            b = this.constraintB(dUmax, dUmin, Umax, Umin, Ymax, Ymin);
            double[,] Ae_t = new double[,] { };
            Matrix Ae = new Matrix(Ae_t);
            double[,] be_t = new double[,] { };
            Matrix be = new Matrix(be_t);
            Matrix x0 = Matrix.Zeros(nu * m, 1);
            Matrix UUUUU = CJ.MainMethod(H, G, Ae, be, Cu, b, x0);
            delta_u = I * UUUUU;


            if (!CJ.Exit_Flag)
            {
                u_C = u + delta_u;
                AutoPause = CJ.Exit_Flag;
            }
            else
            {
                u_C = u;
                AutoPause = CJ.Exit_Flag;
            }

        }

        public void constraint()
        {
            Cb = Cc;
            Mb = M;//(rows, cols) = (ny*P, ny*N)
            SSdb = SSd;//(rows, cols) = (ny*P, nd)
            SSub = SSu;//(rows, cols) = (ny*P, nu*m)

            Matrix T1 = Matrix.Zeros(nu * m, nu * m);
            for (int i = 0; i < T1.cols; i++)
            {
                T1[i, i] = -1;
            }
            Matrix T2 = Matrix.Zeros(nu * m, nu * m);
            for (int i = 0; i < T1.cols; i++)
            {
                T2[i, i] = 1;
            }
            Matrix L1 = Matrix.Zeros(nu * m, nu * m);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i >= j)
                    {
                        for (int k = 0; k < nu; k++)
                        {
                            L1[i * nu + k, j * nu + k] = -1;
                        }
                    }
                }
            }
            Matrix L2 = Matrix.Zeros(nu * m, nu * m);
            for (int i = 0; i < nu * m; i++)
            {
                for (int j = 0; j < nu * m; j++)
                {
                    L2[i, j] = (-1) * L1[i, j];
                }
            }
            Matrix SSub2 = SSub;
            Matrix SSub1 = new Matrix(SSub2.rows, SSub2.cols);
            for (int i = 0; i < SSub1.rows; i++)
            {
                for (int j = 0; j < SSub1.cols; j++)
                {
                    SSub1[i, j] = (-1) * SSub2[i, j];
                }
            }
            Cu = new Matrix((T1.rows + T2.rows + L1.rows + L2.rows + SSub1.rows + SSub2.rows), T1.cols);
            for (int i = 0; i < Cu.rows; i++)
            {
                for (int j = 0; j < Cu.cols; j++)
                {
                    if (i < T1.rows)
                    { Cu[i, j] = T1[i, j]; }
                    else if ((i >= T1.rows) && (i < (T1.rows + T2.rows)))
                    { Cu[i, j] = T2[i - T1.rows, j]; }
                    else if ((i >= (T1.rows + T2.rows)) && (i < (T1.rows + T2.rows + L1.rows)))
                    { Cu[i, j] = L1[i - (T1.rows + T2.rows), j]; }
                    else if ((i >= (T1.rows + T2.rows + L1.rows)) && (i < (T1.rows + T2.rows + L1.rows + L2.rows)))
                    { Cu[i, j] = L2[i - (T1.rows + T2.rows + L1.rows), j]; }
                    else if ((i >= (T1.rows + T2.rows + L1.rows + L2.rows)) && (i < (T1.rows + T2.rows + L1.rows + L2.rows + SSub1.rows)))
                    { Cu[i, j] = SSub1[i - (T1.rows + T2.rows + L1.rows + L2.rows), j]; }
                    else
                    { Cu[i, j] = SSub2[i - (T1.rows + T2.rows + L1.rows + L2.rows + SSub1.rows), j]; }
                }
            }
        }

        public Matrix constraintB(Matrix dUmax, Matrix dUmin, Matrix Umax, Matrix Umin, Matrix Ymax, Matrix Ymin)
        {
            //Matrix Umin, Matrix Umax, Matrix dUmin, Matrix dUmax, Matrix Ymin, Matrix Ymax
            Matrix b1 = Matrix.Zeros(nu * m, 1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < nu; j++)
                {
                    b1[nu * i + j, 0] = (-1) * dUmax[j, 0];
                }
            }
            Matrix b2 = Matrix.Zeros(nu * m, 1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < nu; j++)
                {
                    b2[nu * i + j, 0] = dUmin[j, 0];
                }
            }
            Matrix b3 = Matrix.Zeros(nu * m, 1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < nu; j++)
                {
                    b3[nu * i + j, 0] = u[j, 0] - Umax[j, 0];
                }
            }
            Matrix b4 = Matrix.Zeros(nu * m, 1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < nu; j++)
                {
                    b4[nu * i + j, 0] = Umin[j, 0] - u[j, 0];
                }
            }
            Matrix b5 = Matrix.Zeros(ny * P, 1);
            for (int i = 0; i < P; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    b5[ny * i + j, 0] = Ymax[j, 0];
                }
            }
            b5 = (Mb * Y_pre + SSdb * delta_d) - b5;
            Matrix b6 = Matrix.Zeros(ny * P, 1);
            for (int i = 0; i < P; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    b6[ny * i + j, 0] = Ymin[j, 0];
                }
            }
            b6 = (-1) * (Mb * Y_pre + SSdb * delta_d) + b6;
            Matrix b = new Matrix((b1.rows + b2.rows + b3.rows + b4.rows + b5.rows + b6.rows), 1);
            for (int i = 0; i < b.rows; i++)
            {
                if (i < b1.rows)
                { b[i, 0] = b1[i, 0]; }
                else if ((i >= b1.rows) && (i < (b1.rows + b2.rows)))
                { b[i, 0] = b2[i - b1.rows, 0]; }
                else if ((i >= (b1.rows + b2.rows)) && (i < (b1.rows + b2.rows + b3.rows)))
                { b[i, 0] = b3[i - (b1.rows + b2.rows), 0]; }
                else if ((i >= (b1.rows + b2.rows + b3.rows)) && (i < (b1.rows + b2.rows + b3.rows + b4.rows)))
                { b[i, 0] = b4[i - (b1.rows + b2.rows + b3.rows), 0]; }
                else if ((i >= (b1.rows + b2.rows + b3.rows + b4.rows)) && (i < (b1.rows + b2.rows + b3.rows + b4.rows + b5.rows)))
                { b[i, 0] = b5[i - (b1.rows + b2.rows + b3.rows + b4.rows), 0]; }
                else
                { b[i, 0] = b6[i - (b1.rows + b2.rows + b3.rows + b4.rows + b5.rows), 0]; }
            }
            return b;
        }

        public void setU(Matrix U)
        {
            u = U;
        }

        public void control_Init(Matrix y)
        {
            //The first step of on-line calculation

            for (int i = 0; i < nd; i++)
            {
                d_pre[i, 0] = d[i, 0];
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    Y_pre[i * ny + j, 0] = y[j, 0];
                }
            }
        }

        public void ResetGamma()//暂时还未用到
        {          
            //  Structure Gamma_y and Gamma_u
            double[,] Gamma_y_T_temp = Matrix.Transpose(Gamma_y.Data);
            Gamma_y_T = new Matrix(Gamma_y_T_temp);
            double[,] Gamma_u_T_temp = Matrix.Transpose(Gamma_u.Data);
            Gamma_u_T = new Matrix(Gamma_u_T_temp);
            //Structure H
            Matrix H11 = SSu_T * Gamma_y_T;
            Matrix H12 = Gamma_y * SSu;
            Matrix H1 = H11 * H12;
            Matrix H2 = Gamma_u_T * Gamma_u;
            H = H1 + H2;
        }

        public void Control_All(Matrix uMatrix, Matrix yMatrix, double[] dMatrix,bool CV1,bool CV2,bool CV3,bool CV4,bool CV5,bool CV6)
        {
            this.setU(uMatrix);
            this.SetDisturb(dMatrix);
            if (this.DMC_Init_falg)
            {
                this.control_Init(yMatrix);
                this.DMC_Init_falg = false;
            }
            this.SetY(yMatrix);
            CV1flag = CV1;
            CV2flag = CV2;
            CV3flag = CV3;
            CV4flag = CV4;
            CV5flag = CV5;
            CV6flag = CV6;
            this.control();
        }

        public void Control_All_2(Matrix uMatrix, Matrix yMatrix, double[] dMatrix)
        {
            this.setU(uMatrix);
            this.SetDisturb(dMatrix);
            this.SetY(yMatrix);
            if (this.DMC_Init_falg)
            {
                u_C = u;
                this.control_Init(yMatrix);
                this.DMC_Init_falg = false;
            }
            //this.control();
        }

        public bool CheckSetPointAndLimit(double[] SetPoint)
        {
            bool Flag_HIGH = false;
            bool Flag_LOW = false;
            Matrix TEMP_Ymax = Ymax;
            Matrix TEMP_Ymin = Ymin;
            Matrix NEW_SetPoint = Matrix.Zeros(SetPoint.Count(), 1);
            for (int i = 0; i < SetPoint.Count(); i++)
            {
                NEW_SetPoint[i, 0] = SetPoint[i];
            }
            if ((NEW_SetPoint.rows == TEMP_Ymax.rows) && (NEW_SetPoint.cols == TEMP_Ymax.cols))//检查维数是否匹配
            {
                Matrix temp = TEMP_Ymax - NEW_SetPoint;
                ArrayList list = new ArrayList();
                foreach (double t in temp.Data)
                {
                    list.Add(t);
                }
                list.Sort();
                if (Convert.ToDouble(list[0]) > 0)
                {
                    Flag_HIGH = true;
                }
            }
            if ((NEW_SetPoint.rows == TEMP_Ymin.rows) && (NEW_SetPoint.cols == TEMP_Ymin.cols))//检查维数是否匹配
            {
                Matrix temp = NEW_SetPoint - TEMP_Ymin;
                ArrayList list = new ArrayList();
                foreach (double t in temp.Data)
                {
                    list.Add(t);
                }
                list.Sort();
                if (Convert.ToDouble(list[0]) > 0)
                {
                    Flag_LOW = true;
                }
            }
            return (Flag_HIGH & Flag_LOW);
            //return (true & true);
        }

        public bool CheckLimit(Matrix dumax, Matrix dumin, Matrix umax, Matrix umin, Matrix ymax, Matrix ymin, Matrix u_t, Matrix y_t)
        {
            bool flag_1 = false;
            bool flag_2 = true;
            bool flag_3 = true;
            flag_1 = isXoverY(dumax, dumin) & isXoverY(umax, umin) & isXoverY(ymax, ymin);
            flag_2 = isXoverY(umax, u_t) & isXoverY(u_t, umin) & isXoverY(ymax, y_t) & isXoverY(y_t, ymin);
            flag_3 = isXoverY(ymax, this.SP) & isXoverY(this.SP, ymin);
            return (flag_1 & flag_2 & flag_3);
        }

        private bool isXoverY(Matrix X, Matrix Y)
        {
            bool FLAG = false;

            if ((X.cols == Y.cols) && (X.rows == Y.rows))
            {
                Matrix ans = X - Y;
                ArrayList list = new ArrayList();
                foreach (double a in ans.Data)
                {
                    list.Add(a);
                }
                list.Sort();
                if (Convert.ToDouble(list[0]) > 0)
                {
                    FLAG = true;
                }
            }

            return FLAG;
        }
    }
}
