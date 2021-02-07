using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace chordprogression
{
    class Property
    {
        int[] genreProperty;
        Dictionary<string, int> artistDictionary;

        public void genresearch(ExcelWorksheet worksheet, int i)
        {
            object genreName = worksheet.Cells[i, 1].Value.ToString();
            if (genreName != null)
            {
                switch (genreName)
                {
                    case "ポップ":
                        genreProperty[0] += 1;
                        break;
                    case "ロック":
                        genreProperty[1] += 1;
                        break;
                    case "バラード":
                        genreProperty[2] += 1;
                        break;
                    case "ボカロ":
                        genreProperty[3] += 1;
                        break;
                    default:
                        break;
                }


            }
        }


        public string maingenreDecide()
        {
            int max = 0;
            int maxidx = 0;
            for (int i = 0; i < genreProperty.Length; i++)
            {
                int thisNum = genreProperty[i];

                if (thisNum > max)
                {
                    max = thisNum;
                    maxidx = i;
                }

            }
            string maingenre = "";
            switch (maxidx)
            {
                case 0:
                    maingenre = "POP";
                    break;
                case 1:
                    maingenre = "ROCK";
                    break;
                case 2:
                    maingenre = "BALLAD";
                    break;
                case 3:
                    maingenre = "VOCALOID";
                    break;

            }
            return maingenre;
        }

        public void artistsearch(ExcelWorksheet worksheet, int i)
        {
            object artistName = worksheet.Cells[i, 4].Value;
            if (artistName != null)
            {
                if (artistDictionary.ContainsKey(artistName.ToString()) == false)
                {
                    artistDictionary.Add(artistName.ToString(), 1);
                }
                else
                {
                    int value = artistDictionary[artistName.ToString()];
                    value = value + 1;
                    artistDictionary.Remove(artistName.ToString());
                    artistDictionary.Add(artistName.ToString(), value);
                }

            }
        }


        public Property(ref int[] getgenreProperty, ref Dictionary<string, int> getartistDictionary)
        {
            genreProperty = getgenreProperty;
            artistDictionary = getartistDictionary;

        }
    }
}
