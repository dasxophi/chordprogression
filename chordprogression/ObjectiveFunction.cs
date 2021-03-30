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
        private static int[,] fifthCircle = new int[24, 24]
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
        private static int[] prev = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        Regex regexC = new Regex(@"^(C)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexF = new Regex(@"^(F)(M7|7|dim|)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexBb = new Regex(@"^(Bb|A#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexEb = new Regex(@"^(Eb|D#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexAb = new Regex(@"^(Ab|G#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexDb = new Regex(@"^(Db|C#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexGb = new Regex(@"^(Gb|F#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexB = new Regex(@"^(B)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexE = new Regex(@"^(E)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexA = new Regex(@"^(A)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexD = new Regex(@"^(D)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexG = new Regex(@"^(G)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexAm = new Regex(@"^(Am)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexDm = new Regex(@"^(Dm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexGm = new Regex(@"^(Gm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexCm = new Regex(@"^(Cm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexFm = new Regex(@"^(Fm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexBbm = new Regex(@"^(A#m|Bbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexEbm = new Regex(@"^(D#m|Ebm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexAbm = new Regex(@"^(G#m|Abm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexDbm = new Regex(@"^(C#m|Dbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexGbm = new Regex(@"^(F#m|Gbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexBm = new Regex(@"^(Bm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexEm = new Regex(@"^(Em)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");

        public int ChordToNumber(string chord)
        {

            if (regexC.IsMatch(chord) == true && chord.Contains("Cm") == false) return 0;
            else if (regexF.IsMatch(chord) == true && chord.Contains("Fm") == false) return 1;
            else if (regexBb.IsMatch(chord) == true && chord.Contains("Bbm") == false && chord.Contains("A#m") == false) return 2;
            else if (regexEb.IsMatch(chord) == true && chord.Contains("Ebm") == false && chord.Contains("D#m") == false) return 3;
            else if (regexAb.IsMatch(chord) == true && chord.Contains("Abm") == false && chord.Contains("G#m") == false) return 4;
            else if (regexDb.IsMatch(chord) == true && chord.Contains("Dbm") == false && chord.Contains("C#m") == false) return 5;
            else if (regexGb.IsMatch(chord) == true && chord.Contains("Gbm") == false && chord.Contains("F#m") == false) return 6;
            else if (regexB.IsMatch(chord) == true && chord.Contains("Bm") == false) return 7;
            else if (regexE.IsMatch(chord) == true && chord.Contains("Em") == false) return 8;
            else if (regexA.IsMatch(chord) == true && chord.Contains("Am") == false) return 9;
            else if (regexD.IsMatch(chord) == true && chord.Contains("Dm") == false) return 10;
            else if (regexG.IsMatch(chord) == true && chord.Contains("Gm") == false) return 11;
            else if (regexAm.IsMatch(chord) == true) return 12;
            else if (regexDm.IsMatch(chord) == true) return 13;
            else if (regexGm.IsMatch(chord) == true) return 14;
            else if (regexCm.IsMatch(chord) == true) return 15;
            else if (regexFm.IsMatch(chord) == true) return 16;
            else if (regexBbm.IsMatch(chord) == true) return 17;
            else if (regexEbm.IsMatch(chord) == true) return 18;
            else if (regexAbm.IsMatch(chord) == true) return 19;
            else if (regexDbm.IsMatch(chord) == true) return 20;
            else if (regexGbm.IsMatch(chord) == true) return 21;
            else if (regexBm.IsMatch(chord) == true) return 22;
            else if (regexEm.IsMatch(chord) == true) return 23;
            else return -1;
        }

        private int TrackingMinimum(int start, int tmp)
        {
            if(tmp == start)
            {
                //MessageBox.Show(start.ToString());
                Console.Write(start);
            } 
            else
            {
                Console.Write(tmp + "->");
                return TrackingMinimum(start, prev[tmp]);
            }
            return 0;
        }

        private int DijkstraAlgorithm(int start, int end)
        {

            if (start > 23 || end > 23) MessageBox.Show("エラー");
            else
            {
                
                for (int i = 0; i < 24; i++) // initialization1
                {
                    dist[i] = INF;
                    prev[i] = -1;
                }
                
                //Dijkstra Algorithm
                dist[start] = 0; // initialization2
                for (int i = 0; i < 24; i++) //左回りに最短距離計算
                {
                    for (int j = 0; j < 24; j++)
                    {
                        if (fifthCircle[i, j] != INF) //iからjまでの経路が存在するかチェック. fifthCirlce[i,j]は最初に設定しておいたiとj間の最短距離
                        {
                            if (dist[j] == INF) //また, Jまでの距離が初期値(INF)だったら
                            {
                               
                                dist[j] = dist[i] + fifthCircle[i, j]; // jまでの距離が最小距離
                                prev[j] = i;
                            }
                            else if (dist[j] > dist[i] + fifthCircle[i, j])
                            {
                                dist[j] = dist[i] + fifthCircle[i, j];
                                prev[j] = i;
                            }
                        }

                    }
                }
                
                for (int i = 23; i >= 0; i--)//右回りに最短距離計算
                {
                    for (int j = 23; j >= 0; j--)
                    {
                        if (fifthCircle[i, j] != INF) //iからjまでの経路が存在するかチェック. fifthCirlce[i,j]は最初に設定しておいたiとj間の最短距離
                        {
                            if (dist[j] == INF) //また, Jまでの距離が初期値(INF)だったら
                            {

                                dist[j] = dist[i] + fifthCircle[i, j]; // jまでの距離が最小距離
                                prev[j] = i;
                            }
                            else if (dist[j] > dist[i] + fifthCircle[i, j])
                            {
                                dist[j] = dist[i] + fifthCircle[i, j];
                                prev[j] = i;
                            }
                        }

                    }
                }

            }
            /*
            for(int i =0; i<24;i++)
            {
                Console.Write(dist[i] + ",");
            }
            Console.WriteLine();
            */
            //TrackingMinimum(start, end);
            return dist[end];
        }

        public int objectFunction(List<string> CPc, List<string> CPn, int N) //List ver
        {
            int value = 0;
            for(int i = 0; i < N-1; i++)
            {
                value += DijkstraAlgorithm(ChordToNumber(CPc[i]), ChordToNumber(CPn[i]));

            }
            
            return value;
        }

        public int[] objectFunction(List<string> CPc, string[,] CPn, int HMS, int N) //All-Array ver
        {
            int value = 0;
            int[] Values = new int[HMS];
            for (int i = 0; i < HMS; i++)
            {
                for(int j = 0; j< N - 1; j++)
                {
                    Console.WriteLine("current chord : " + CPc[j] + " " + "new chord : " + CPn[i, j]);
                    Console.WriteLine(ChordToNumber(CPc[j]) + " " +  ChordToNumber(CPn[i, j]));
                    value += DijkstraAlgorithm(ChordToNumber(CPc[j]), ChordToNumber(CPn[i, j]));
                }
                //Console.WriteLine("value : " + value);
                Values[i] = value;
                value = 0;
                
            }

            return Values;
        }

    }
}
