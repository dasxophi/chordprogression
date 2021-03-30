using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace chordprogression
{
    class N_gramCsharp
    { 
        public void n_gramDataBase(Form1 form1) //n=2からn=ColumMaxまで全てngram分割
        {
            int ColumMax = form1.RangeColumsCount;
            int RowMax = form1.RangeRowsCount;
            string[] strtmp = new string[ColumMax];
            List<string> FirstHM = new List<string>();
            int k = 0;

            for (int i = 2; i <= RowMax; i++)
            {
                for (int j = 5; j <= ColumMax; j++)
                {
                    if (form1.worksheet.Cells[i, j].Value != null) //nullじゃないとき
                    {
                        //MessageBox.Show(j.ToString());
                        strtmp[k] = form1.worksheet.Cells[i, j].Value.ToString();
                        k++;
                    }
                    else if (form1.worksheet.Cells[i, j].Value == null) //nullのとき
                    {}
                }

                for (int m =ColumMax; m > 0; m--)
                {
                    n_gram(strtmp, i, ref FirstHM);
                    k = 0;
                }
            }


            MessageBox.Show("前処理終了 : " + FirstHM.Count.ToString() + " 個の初期データを検出しました");
            for (int i = 0; i < FirstHM.Count; i++)
            {
                MessageBox.Show(FirstHM[i]);
            }

            //Excelファイルから1行を読み込む
            //その1行に対してn=32から(コード進行データのmaxから)n-gramで分割していく
            //重複は除いてListに保存しておく
            //これをデータベースの全ての行に対して行う
            //全部終わったら前処理終了

        }

        public List<string> n_gramDataBaseByN(Form1 form1, int N) //n=Nで分割
        {
            int ColumMax = form1.RangeColumsCount;
            int RowMax = form1.RangeRowsCount;
            string[] strtmp = new string[ColumMax];
            List<string> FirstHM = new List<string>();
            int k = 0;

            for (int i = 2; i <= RowMax; i++)
            {
                for (int j = 5; j <= ColumMax; j++)
                {
                    if (form1.worksheet.Cells[i, j].Value != null) //nullじゃないとき
                    {
                        //MessageBox.Show(j.ToString());
                        strtmp[k] = form1.worksheet.Cells[i, j].Value.ToString();
                        strtmp[k] = strtmp[k].Replace('△', 'M');
                        strtmp[k] = strtmp[k].Replace('-', 'b');
                        k++;
                    }
                    else if (form1.worksheet.Cells[i, j].Value == null) //nullのとき
                    { }
                }

                for (int m = ColumMax; m > 0; m--)
                {
                    n_gram(strtmp, N, ref FirstHM);
                    k = 0;
                }
            }

            return FirstHM;
            /*
            MessageBox.Show("前処理終了 : " + FirstHM.Count.ToString() + " 個の初期データを検出しました");
            for (int i = 0; i < FirstHM.Count; i++)
            {
                MessageBox.Show(FirstHM[i]);
            }
            */
        }



        public void n_gram(string[] src, int num, ref List<string> result) // numの分だけ分割(配列に対して)
        {

            string tmp;
            int n = num;
            Regex regex = new Regex(@"[a-z|A-Z|0-9]-$");
            for (int i = 0; i < src.Length - (n - 1); i++)
            {
                tmp = src[i];
                for (int j = 0; j < n - 1; j++)
                {
                    tmp += "-" + src[i + j + 1];

                }
                if(result.Contains(tmp) != true && tmp.Contains("--") != true && tmp != "-" && regex.IsMatch(tmp) != true) result.Add(tmp);
            }


        }

        public void n_gram(string src, int num, List<string> result) // numの分だけ分割
        {
            string[] strsplit = src.Split('-');
            string tmp;
            int n = num;
            for(int i = 0; i < strsplit.Length - (n-1); i++ )
            {
                tmp = strsplit[i];
                for (int j = 0; j < n - 1; j++)
                {
                    tmp += "-" + strsplit[i + j + 1];
                }
                result.Add(tmp);
            }
            /*testcode
            for (int i = 0; i < 2; i++)
            {
                tmp = strsplit[i];
                for (int j = 0; j < 2; j++)
                {
                    tmp += "-" + strsplit[i+j+1];
                }
                result.Add(tmp);
            }

            

            for (int i = 0; i < result.Count; i++)
            {
                MessageBox.Show(result[i]);
            }
            */

        }

        public List<string> n_gram(string src) // n=1からn = maxまで分割
        {
            List<string> result = new List<string>();
            string[] strsplit = src.Split('-');
            int n = strsplit.Length;

            for(int i = n; i > 0; i--)
            {
                n_gram(src, i, result);
            }

            return result;

        }

        private double addition(List<string> chordsByNgram1, List<string> chordsByNgram2, int i, int j) 
        {
            double similarity = 0.00;
            if (chordsByNgram1[i] == chordsByNgram2[j] && i == j) similarity = similarity + 0.3;
            if ((chordsByNgram1[i] == "C" && chordsByNgram2[j] == "Em" && i == j) ||
                (chordsByNgram1[i] == "Em" && chordsByNgram2[j] == "C" && i == j) ||
                (chordsByNgram1[i] == "Dm" && chordsByNgram2[j] == "F" && i == j) ||
                (chordsByNgram1[i] == "F" && chordsByNgram2[j] == "Dm" && i == j) ||
                (chordsByNgram1[i] == "C" && chordsByNgram2[j] == "CM7" && i == j) ||
                (chordsByNgram1[i] == "CM7" && chordsByNgram2[j] == "C" && i == j) ||
                (chordsByNgram1[i] == "Dm" && chordsByNgram2[j] == "Dm7" && i == j) ||
                (chordsByNgram1[i] == "Dm7" && chordsByNgram2[j] == "Dm" && i == j) ||
                (chordsByNgram1[i] == "Em" && chordsByNgram2[j] == "Em7" && i == j) ||
                (chordsByNgram1[i] == "Em7" && chordsByNgram2[j] == "Em" && i == j) ||
                (chordsByNgram1[i] == "F" && chordsByNgram2[j] == "FM7" && i == j) ||
                (chordsByNgram1[i] == "FM7" && chordsByNgram2[j] == "F" && i == j) ||
                (chordsByNgram1[i] == "G" && chordsByNgram2[j] == "G7" && i == j) ||
                (chordsByNgram1[i] == "G7" && chordsByNgram2[j] == "G" && i == j) ||
                (chordsByNgram1[i] == "Am" && chordsByNgram2[j] == "Am7" && i == j) ||
                (chordsByNgram1[i] == "Am7" && chordsByNgram2[j] == "Am" && i == j) ||
                (chordsByNgram1[i] == "Bdim" && chordsByNgram2[j] == "Bm7-5" && i == j) ||
                (chordsByNgram1[i] == "Bm7-5" && chordsByNgram2[j] == "Bdim" && i == j) ) similarity = similarity + 0.3;
            return similarity;
        }

        public double similardegree(string src1, string src2)
        {
            double totalcount = 0.00;
            double equalcount = 0.00;
            double additionPoint = 0.00;

            List<string> chordsByNgram1 = n_gram(src1);
            List<string> chordsByNgram2 = n_gram(src2);

            for (int i = 0; i < chordsByNgram1.Count; i++)
            {
                totalcount++;
                for(int j = 0; j < chordsByNgram2.Count; j++)
                {
                    additionPoint = addition(chordsByNgram1, chordsByNgram2, i, j);
                    if (chordsByNgram1[i] == chordsByNgram2[j]) { equalcount++; break; }
                }
            }

            double similarity = (equalcount / totalcount) + additionPoint;

            return similarity;

        }


    }
}
