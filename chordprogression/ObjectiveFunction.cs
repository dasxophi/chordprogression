using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace chordprogression
{
    class ObjectiveFunction
    {

        private static int INF = 50000;
        private static int[,] fifthCircle = new int[,]
        {
                {0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                {1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                {INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                {INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF},
                {INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF},
                {INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF},
                {1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF},
                {INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF},
                {INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF},
                {INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF},
                {INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF},
                {1, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1},

                {1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1},
                {INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF},
                {INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF, INF},
                {INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF, INF},
                {INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1, INF},
                {INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF, 1},
                {INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF, INF},
                {INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF, INF},
                {INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF, INF},
                {INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1, INF},
                {INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0, 1},
                {INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 1, 1, INF, INF, INF, INF, 1, INF, INF, INF, INF, 1, 0}
        };
        private static int[] dist = new int[] { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF };
        private static int[] prev = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        
        public int ChordToNumber(string chord)
        {
            Regex regexC = new Regex(@"[C|CM7|C7]/[a-z|A-Z|0-9]$");
            Regex regexF = new Regex(@"[F|FM7|F7]/[a-z|A-Z|0-9]$");
            Regex regexBb = new Regex(@"[Bb|BbM7|Bb7]/[a-z|A-Z|0-9]$");
            Regex regexEb = new Regex(@"[Eb|EbM7|Eb7]/[a-z|A-Z|0-9]$");
            Regex regexAb = new Regex(@"[Ab|AbM7|Ab7]/[a-z|A-Z|0-9]$");
            Regex regexDb = new Regex(@"[Db|DbM7|Db7]/[a-z|A-Z|0-9]$");
            Regex regexGb = new Regex(@"[Gb|GbM7|Gb7]/[a-z|A-Z|0-9]$");
            Regex regexB = new Regex(@"[B|BM7|B7]/[a-z|A-Z|0-9]$");
            if (chord == "C" || chord == "CM7" || chord == "C7" ) return 0;
            else if (chord == "F" || chord == "FM7" || chord == "F7" ) return 1;
            else if (chord == "Bb" || chord == "BbM7" || chord == "Bb7") return 2;
            else if (chord == "Eb" || chord == "EbM7" || chord == "Eb7") return 3;
            else if (chord == "Ab" || chord == "AbM7" || chord == "Ab7") return 4;
            else if (chord == "Db" || chord == "DbM7" || chord == "Db7") return 5;
            else if (chord == "Gb" || chord == "GbM7" || chord == "Gb7") return 6;
            else if (chord == "B" || chord == "BM7" || chord == "B7") return 7;
            else if (chord == "E" || chord == "EM7" || chord == "E7") return 8;
            else if (chord == "A" || chord == "AM7" || chord == "A7") return 9;
            else if (chord == "D" || chord == "DM7" || chord == "D7") return 10;
            else if (chord == "G" || chord == "GM7" || chord == "G7") return 11;
            else if (chord == "Am" || chord == "Am7" || chord == "AmM7") return 12;
            else if (chord == "Dm" || chord == "Dm7" || chord == "DmM7") return 13;
            else if (chord == "Gm" || chord == "Gm7" || chord == "GmM7") return 14;
            else if (chord == "Cm" || chord == "Cm7" || chord == "CmM7") return 15;
            else if (chord == "Fm" || chord == "Fm7" || chord == "FmM7") return 16;
            else if (chord == "Bbm" || chord == "Bbm7" || chord == "BbmM7") return 17;
            else if (chord == "Ebm" || chord == "Ebm7" || chord == "EbmM7") return 18;
            else if (chord == "Abm" || chord == "Abm7" || chord == "AbmM7") return 19;
            else if (chord == "Dbm" || chord == "Dbm7" || chord == "DbmM7") return 20;
            else if (chord == "Gbm" || chord == "Gbm7" || chord == "GbmM7") return 21;
            else if (chord == "Bm" || chord == "Bm7" || chord == "BmM7") return 22;
            else if (chord == "Em" || chord == "Em7" || chord == "EmM7") return 23;
            else return -1;
        }

        private int TrackingMinimum(int start, int tmp)
        {
            if(tmp == start)
            {
                MessageBox.Show(start.ToString());
            } 
            else
            {
                MessageBox.Show(tmp + "->");
                return TrackingMinimum(start, prev[tmp]);
            }
            return 0;
        }

        private int DijkstraAlgorithm(int start, int end)
        {
            //Dijkstra Algorithm
            if (start > 23 || end > 23) MessageBox.Show("計算できません");
            Console.WriteLine("start : " + start);
            dist[start] = 0; // initialization
            for(int i = 0; i < 23; i++)
            {
                for(int j = 0; j < 23; j++)
                {
                    if(fifthCircle[i,j] != INF) //iからjまでの経路が存在するかチェック
                    {
                        if(dist[j] == INF) //また, Jまでの距離が初期値(INF)だったら
                        {
                            dist[j] = dist[i] + fifthCircle[i, j]; // jまでの距離が最小距離
                            prev[j] = i;
                        }
                        else if(dist[j] > dist[i] + fifthCircle[i, j])
                        {
                            dist[j] = dist[i] + fifthCircle[i, j];
                            prev[j] = i;
                        }
                    }
                }
            }
            //MessageBox.Show("length : " + dist[end]);
            //TrackingMinimum(start, end);
            return dist[end];
        }

        public int objectFunction(List<string> CPc, List<string> CPn, int N) //List ver
        {
            //int N = CPn.Count();
            int value = 0;
            for(int i = 0; i < N-1; i++)
            {
                value += DijkstraAlgorithm(ChordToNumber(CPc[i]), ChordToNumber(CPn[i]));
                Console.WriteLine("value : " + value);
            }
            
            return value;
        }

        public int[] objectFunction(List<string> CPc, string[,] CPn, int HMS, int N) //All-Array ver
        {
            //int N = CPc.Count(); //dummy考慮
            int value = 0;
            int[] Values = new int[HMS];
            for (int i = 0; i < HMS; i++)
            {
                Console.WriteLine("i = " + i);
                for(int j = 0; j< N - 1; j++)
                {
                    Console.WriteLine("i, j" + i + ", " + j);
                    value += DijkstraAlgorithm(ChordToNumber(CPc[j]), ChordToNumber(CPn[i, j]));
                    //Console.WriteLine(value);
                }
                Values[i] = value;
                value = 0;
                
            }

            return Values;
        }

    }
}
