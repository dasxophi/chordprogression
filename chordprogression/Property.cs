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
        Form1 form1;
        private void genresearch(ExcelWorksheet worksheet, int i)
        {
            object genreName = worksheet.Cells[i, 1].Value.ToString();
            if (genreName != null)
            {
                switch (genreName)
                {
                    case "ポップ":
                        form1.genreProperty[0] += 1;
                        break;
                    case "ロック":
                        form1.genreProperty[1] += 1;
                        break;
                    case "バラード":
                        form1.genreProperty[2] += 1;
                        break;
                    case "ボカロ":
                        form1.genreProperty[3] += 1;
                        break;
                    default:
                        break;
                }


            }
        }

       
        private void artistsearch(ExcelWorksheet worksheet, int i)
        {
            object artistName = worksheet.Cells[i, 4].Value;
            if (artistName != null)
            {
                if (form1.artistDictionary.ContainsKey(artistName.ToString()) == false)
                {
                    form1.artistDictionary.Add(artistName.ToString(), 1);
                }
                else
                {
                    int value = form1.artistDictionary[artistName.ToString()];
                    value = value + 1;
                    form1.artistDictionary.Remove(artistName.ToString());
                    form1.artistDictionary.Add(artistName.ToString(), value);
                }

            }
        }
        public string maingenreDecide()
        {
            int max = 0;
            int maxidx = 0;
            for (int i = 0; i < form1.genreProperty.Length; i++)
            {
                int thisNum = form1.genreProperty[i];

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

        public void print()
        {
            double allgenre = form1.genreProperty[0] + form1.genreProperty[1] + form1.genreProperty[2] + form1.genreProperty[3];

            form1.richTextBox4.Clear();
            form1.richTextBox4.AppendText("POP : " + Math.Truncate((form1.genreProperty[0] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("ROCK : " + Math.Truncate((form1.genreProperty[1] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("BALLAD : " + Math.Truncate((form1.genreProperty[2] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("VOCALOID : " + Math.Truncate((form1.genreProperty[3] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.maingenreNow = maingenreDecide();

            form1.maingenreText.Text = "現在のコード進行は" + form1.maingenreNow + "寄りです";

            form1.richTextBox5.Clear();
            foreach (string name in form1.artistDictionary.Keys)
            {
                form1.richTextBox5.AppendText(name + "\n");
            }
            form1.artistDictionary.Clear();
        }

        public void search(ExcelWorksheet worksheet, int i)
        {
            genresearch(worksheet, i);
            artistsearch(worksheet, i);

        }

        
        public Property(Form1 _form1)
        {
            form1 = _form1;
        }

    }
}
