using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace chordprogression
{
    class HS
    {
        private Form1 form1;
        private static int HMS;
        private static double HMCR = 0.80;
        private static double PAR = 0.10;
        private static int N;
        private static int FREQUENCY = 50; //繰り返し回数
        //private static string[,] HM = new string[HMS, N];
        //private static int[] Values = new int[HMS];
        private List<String> CPc; //データベースのコード進行をn-gramで分割してここに保存
        Random random = new Random();

        private int[] InitalHM(string[,] HM, int[] Values, int N)
        {
            
            ObjectiveFunction objfunc = new ObjectiveFunction();
            N_gramCsharp ngram = new N_gramCsharp();
            List<string> FirstCP = ngram.n_gramDataBaseByN(form1,N);
            Console.WriteLine("HMS, N : " + HMS + ", " + N);
            Console.WriteLine("CPc : " + CPc[2]);
            for (int i = 0; i < HMS; i++)
            {
                string[] tmp = FirstCP[i].Split('-');
                
 
                for (int j = 0; j < N; j++)
                {
                    Console.WriteLine("----insert------");
                    tmp[j] = tmp[j].Replace('△', 'M');
                    tmp[j] = tmp[j].Replace('-', 'b');
                    HM[i, j] = tmp[j];
                }
                
            }
            Values = objfunc.objectFunction(CPc, HM, HMS, N);
            Sorting(HM, Values, 0, HMS-1);
            for (int i = 0; i < HMS; i++)
            { //出力
                for (int j = 0; j < N; j++)
                {
                    Console.Write(HM[i, j] + " ");
                }
                Console.Write("         | " + Values[i]);
                Console.WriteLine();


            }
            return Values;
        }

        private void Sorting(string[,] HM, int[] Values, int p, int r) //QuickSort
        {
            if(p < r)
            {
                int q = Partition(HM, Values, p, r);
                Sorting(HM, Values, p, q - 1);
                Sorting(HM, Values, q + 1, r);
            }
        }

        private int Partition(string[,] HM, int[] Values, int p, int r)
        {
            int q = p;
            for(int j = p; j < r; j++)
            {
                if(Values[j] <= Values[r])
                {
                    Swap(HM, Values, q, j, N);
                    q++;
                }
            }
            Swap(HM, Values, q, r, N);
            return q;
        }

        private void Swap(string[,] HM, int[] Values, int before, int fore, int N)
        {
            int tmp = Values[before];
            Values[before] = Values[fore];
            Values[fore] = tmp;
            for(int j = 0; j < N; j++)
            {
                string strtmp = HM[before, j];
                HM[before, j] = HM[fore, j];
                HM[fore, j] = strtmp;
            }
        }

        private List<string> NewHarmony(string[,] HM, double HMCR, double PAR)
        {
            List<string> CPn = new List<string>();
            if (random.NextDouble() <= HMCR) // with probability HMCR
            {

                for (int j = 0; j < N; j++)
                {
                    int i = random.Next(0, HMS);
                    CPn.Add(HM[i, j]);

                }
                return CPn;

            }
            else // with probability HMCR (all random)
            {
                for (int k = 0; k < N; k++)
                {
                    {
                        int i = random.Next(0, HMS);
                        int j = random.Next(0, N);
                        CPn.Add(HM[i, j]);
                    }

                }
                return CPn;
            }
        }

        private int UpdateHM(string[,] HM, int[] Values, List<string> CPc, List<string> CPn)
        { 
            ObjectiveFunction objfunc = new ObjectiveFunction();
            int value = objfunc.objectFunction(CPc, CPn, N);
            Console.WriteLine("value 1 : " + Values[1]);
            Console.WriteLine("value 2 : " + Values[2]);
            for (int i = 0; i < HMS; i++)
            {
                if (Values[i] > value) // CPnのvalueより大きいvalueを発見したら
                {
                    Console.WriteLine("updating...");
                    Values[i] = value; // i番目のCPのvalueをCPnのvalueとする
                    for (int j = 0; j < N; j++)
                    {
                        HM[i, j] = CPn[j]; // i番目のCPをCPnに入れ替える
                    }
                    Sorting(HM, Values, 0, HMS - 1); //updateしたHMをsort
                    return 0; //終了する
                }
                
            }
            return 0;
        }

        private void RecommendAndColor(List<string> RecommendList, int[] Values, Form1 form1)
        {
            //int num = 0;
            for(int i = 0; i < RecommendList.Count(); i++)
            {
                form1.chordName = RecommendList[i];
                form1.chordName = form1.chordName.Replace('-', 'b');
                form1.buttonList[i].Enabled = true;
                form1.buttonList[i].BackColor = Color.FromArgb(255, 0 + Values[i] * 50, 0 + Values[i]*50);
                form1.buttonList[i].Text = form1.chordName;
                //num++;
            }
            
        }

        public HS(int _HMS, int _N, Form1 _form, List<string> _CPc)
        {
            HMS = _HMS;
            N = _N;
            form1 = _form;
            CPc = _CPc;
        }

        public void Launch()
        {
            string[,] HM = new string[HMS, N];
            int[] Values = new int[HMS];

            Values = InitalHM(HM, Values, N); //HM初期化
            for(int i = 0; i < FREQUENCY; i++) //設定しておいたFREQUENCY値まで新しいハーモニーの生成とアップデートを繰り返す
            {
                UpdateHM(HM, Values ,CPc, NewHarmony(HM, HMCR, PAR));
            }
            // 生成とアップデートが終了したら
            Console.WriteLine("_______update complete_______");
            for (int i = 0; i < HMS; i++)
            { //出力
                for (int j = 0; j < N; j++)
                {
                    Console.Write(HM[i, j] + " ");
                }
                Console.Write("         | " + Values[i]);
                Console.WriteLine();
            }

            List<string> RecommendList = new List<string>();
            for (int i = 0; i < HMS; i++) 
            {
                if(RecommendList.Contains(HM[i, N - 1]) != true) RecommendList.Add(HM[i, N - 1]);
                //CPの最後の要素をRecommendListに追加
            }

            RecommendAndColor(RecommendList, Values, form1); //画面上に推薦コードを表示
        }


    }
}
