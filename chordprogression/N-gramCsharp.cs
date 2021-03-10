using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chordprogression
{
    class N_gramCsharp
    { 
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

            /*
            for (int i = 0; i < result.Count; i++)
            {
                MessageBox.Show(result[i]);
            }
            */
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
