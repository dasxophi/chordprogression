using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using OfficeOpenXml;


namespace chordprogression
{
    class ChordRecommendAlgorithm
    {
        private Form1 form1;
        private string filePath;

        public void excelSet(string _filePath)
        {
            filePath = _filePath;
        }

        public void simpleRecommend(Form1 _form, string sheetName)
        {
            form1 = _form;
            var xlsxFile = File.OpenRead(filePath);
            sheetName = sheetName.Replace("M", "△");
            sheetName = sheetName.Replace('b', '-');
            using (var package = new ExcelPackage(xlsxFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                int SearchRangeRowsCount = worksheet.Dimension.Rows;
                int SearchRangeColumnsCount = worksheet.Dimension.Columns;
                form1.chordName = "";
                int num = 0;
                for (int i = 2; i <= SearchRangeRowsCount; i = i + 3)
                {

                    for (int j = 1; j <= SearchRangeColumnsCount - 2; ++j)
                    {

                        if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 10)//10
                        {

                            {
                                form1.chordName = worksheet.Cells[i, j].Text;
                                form1.chordName = form1.chordName.Replace('-', 'b');
                                form1.buttonList[num].Enabled = true;
                                form1.buttonList[num].BackColor = Color.Orange;
                                form1.buttonList[num].Text = form1.chordName;
                                num++;

                            }
                        }
                        else if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 3 && Convert.ToInt32(worksheet.Cells[i + 2, j].Value) < 10)
                        {

                            {
                                form1.chordName = worksheet.Cells[i, j].Text;
                                form1.chordName = form1.chordName.Replace('-', 'b');
                                form1.buttonList[num].Enabled = true;
                                form1.buttonList[num].BackColor = Color.Yellow;
                                form1.buttonList[num].Text = form1.chordName;
                                num++;

                            }
                        }
                        else if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 1 && Convert.ToInt32(worksheet.Cells[i + 2, j].Value) < 3)
                        {

                            {
                                form1.chordName = worksheet.Cells[i, j].Text;
                                form1.chordName = form1.chordName.Replace('-', 'b');
                                form1.buttonList[num].Enabled = true;
                                form1.buttonList[num].BackColor = Color.LightGreen;
                                form1.buttonList[num].Text = form1.chordName;
                                num++;


                            }
                        }

                    }

                }


            }
        }

        public bool multipleDetailed(string nextChordNameSheetName, string nowChordSheetName)
        {
            
            string NextChordName = nextChordNameSheetName;
            string sheetName = nowChordSheetName.Replace("M", "△");
            sheetName = sheetName.Replace("/", "on");
            sheetName = sheetName.Replace('b', '-');
            var xlsxFile = File.OpenRead(filePath);
            using (var package = new ExcelPackage(xlsxFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                int SearchRangeRowsCount = worksheet.Dimension.Rows;
                int SearchRangeColumnsCount = worksheet.Dimension.Columns;

                for (int i = 2; i <= SearchRangeRowsCount; i = i + 3)
                {
                    for (int j = 1; j <= SearchRangeColumnsCount; j++)
                    {
                        //progressBar1.PerformStep();
                        if (worksheet.Cells[i, j].Text == NextChordName)
                        {
                            if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 1)
                            {

                                return true;

                            }
                            else return false;

                        }
                    }
                }
                return false;
            }
        }

            public void multipleAlgorithm(Form1 _form, ref List<string> partCheckingList)
            {
            form1 = _form;
            int ChordsCount = form1.chordSoundList2.Count;
            //MessageBox.Show("chord sound list : " +chordSoundList2.Count.ToString());
            if (ChordsCount < 3)
            {
                for (int i = 0; i < ChordsCount; i++)
                {
                    //form1.progressBar1.PerformStep();
                    if (i != ChordsCount - 1)
                    {
                        bool result;
                        //result = form1.searchChord2(form1.chordSoundList2[i + 1], form1.chordSoundList2[i]);
                        result = form1.searchChord2(form1.chordSoundList2[i + 1], form1.chordSoundList2[i]);

                        if (result == true)
                        {
                            string chord = form1.chordSoundList2[i];
                            partCheckingList.Add(chord);
                            //MessageBox.Show("次に行くコード進行が存在します。");
                        }
                        else if (result == false)
                        {
                            //MessageBox.Show("次に行くコード進行がありません。\r\nスタックをリセットします。");
                            partCheckingList.Clear();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("多重推定によって集まったスタックの一番目は " + partCheckingList[0]);
                        string chord = form1.chordSoundList2[i];
                        partCheckingList.Add(chord);
                        //MessageBox.Show("最後のコードです。");

                    }

                }
            }

            else
            {
                if (form1.flag == false)
                    partCheckingList.RemoveAt(partCheckingList.Count - 1);
                else if (form1.flag == true)
                    form1.flag = false;

                for (int i = ChordsCount - 2; i < ChordsCount; i++)
                {

                    //progressBar1.PerformStep();
                    if (i != ChordsCount - 1)
                    {
                          bool result = form1.searchChord2(form1.chordSoundList2[i + 1], form1.chordSoundList2[i]);


                        if (result == true)
                        {

                            string chord = form1.chordSoundList2[i];
                            partCheckingList.Add(chord);
                            //MessageBox.Show("次に行くコード進行が存在します。");
                        }
                        else if (result == false)
                        {
                            //MessageBox.Show("次に行くコード進行がありません。\r\nスタックをリセットします。");
                            partCheckingList.Clear();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("多重推定によって集まったスタックの一番目は " + partCheckingList[0]);
                        string chord = form1.chordSoundList2[i];
                        partCheckingList.Add(chord);
                        //MessageBox.Show("最後のコードです。");

                    }

                }
            }

            //MessageBox.Show("多重推定を一部終了しました。今からコード推薦を行います。");

            if (partCheckingList.Count == 0)
            {
                MessageBox.Show("多重推定で推薦できるコードが存在しません。\r\n単純推薦を行います。");
                form1.chordSet();
                form1.chordName = form1.chordSoundList2.Last();
                form1.searchChord(form1.chordName);
            }
            else if (form1.part == "")　//パートの区別なしで推薦を行うときはpartChechkingRecommendに
            {
                //form1.partCheckingRecommend(partCheckingList);
                multipleRecommend(form1, ref partCheckingList);
            }
            else if (form1.part != "") //パートを区別して推薦を行うときはpartChechkingRecommend2に
            {
                //form1.partCheckingRecommend2(partCheckingList);
                multipleRecommend2(form1, ref partCheckingList);
            }

        }

        public void multipleRecommend(Form1 _form, ref List<string> partCheckingList)
        {
            form1 = _form;
            List<string> recommendList = new List<string>();
            Property property = new Property(ref form1.genreProperty, ref form1.artistDictionary);
            int PartCheckingListCount = partCheckingList.Count;
            int k = 0;
            List<string> partCheckingList2 = partCheckingList.ToList();


            for (int i = 2; i <= form1.RangeRowsCount; i++)
            {
                k = 0;

                if (form1.worksheet.Cells[i, 3].Value == null)
                {

                    form1.chordSet();
                    break;
                }

                for (int j = 5; j <= form1.RangeColumsCount; j++) //4を5に修正
                {

                    //progressBar1.PerformStep();
                    if (k == PartCheckingListCount)
                    {
                        if (form1.worksheet.Cells[i, j].Value != null)
                        {

                            string recommendChord = form1.worksheet.Cells[i, j].Value.ToString();
                            recommendChord = recommendChord.Replace('-', 'b');
                            if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                recommendList.Add(recommendChord);
                            /*
                            genresearch(worksheet, i);
                            artistsearch(worksheet, i);
                            */

                            property.genresearch(form1.worksheet, i);
                            property.artistsearch(form1.worksheet, i);
                            k = 0;
                        }

                    }

                    if (form1.worksheet.Cells[i, j].Value != null)
                    {
                        string chord = partCheckingList2[k];
                        chord = chord.Replace("M", "△");

                        if (form1.worksheet.Cells[i, j].Value.ToString() == chord)
                        {
                            k++;
                        }
                        else k = 0;
                    }
                    else if (form1.worksheet.Cells[i, j].Value == null)
                    {
                        k = 0;
                    }

                }
            }

            form1.richTextBox4.Clear();
            double allgenre = form1.genreProperty[0] + form1.genreProperty[1] + form1.genreProperty[2] + form1.genreProperty[3];
            form1.richTextBox4.AppendText("POP : " + Math.Truncate((form1.genreProperty[0] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("ROCK : " + Math.Truncate((form1.genreProperty[1] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("BALLAD : " + Math.Truncate((form1.genreProperty[2] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.richTextBox4.AppendText("VOCALOID : " + Math.Truncate((form1.genreProperty[3] / allgenre) * 100.0 * 10) / 10 + "\n");
            form1.maingenreNow = property.maingenreDecide();

            form1.maingenreText.Text = "現在のコード進行は" + form1.maingenreNow + "寄りです";

            form1.richTextBox5.Clear();
            foreach (string name in form1.artistDictionary.Keys)
            {
                form1.richTextBox5.AppendText(name + "\n");
            }
            form1.artistDictionary.Clear();

            if (recommendList.Count > 0)
            {
                int RecommendListCount = recommendList.Count;
                //int ButtonListCount = buttonList.Count;
                //MessageBox.Show("コード推薦を行います・・・");
                for (int z = 0; z < RecommendListCount; z++)
                {

                    form1.buttonList[z].Enabled = true;
                    form1.buttonList[z].BackColor = Color.Orange;
                    form1.buttonList[z].Text = recommendList[z];

                }
            }
            else
            {
                if (form1.automode == 1)
                {
                    //MessageBox.Show("推薦できるコードが存在しません。他のコード進行に乗り換えます。");
                    //Form2 f2 = new Form2(this);
                    form1.searchSimilarChords(form1.chordSoundList2);
                }
                else
                {
                    MessageBox.Show("多重推定で推薦できるコードが存在しません。\n単純推定を適用します。");
                    form1.chordSet();
                    //form1.searchChord(form1.chordList.Peek());
                    form1.searchChord(form1.chordSoundList2.Last());
                }

            }
        }

        public void multipleRecommend2(Form1 _form, ref List<string> partCheckingList)
        {
            form1 = _form;
            List<string> recommendList = new List<string>();
            Property property = new Property(ref form1.genreProperty, ref form1.artistDictionary);
            int PartCheckingListCount = partCheckingList.Count;
            int k = 0;
            List<string> partCheckingList2 = partCheckingList.ToList();

            for (int i = 2; i <= form1.RangeRowsCount; i++)
            {
                k = 0;

                if (form1.worksheet.Cells[i, 3].Value == null)
                {

                    form1.chordSet();
                    break;
                }

                switch (form1.part)
                {
                    case "Intro":
                        for (int j = 5; j <= 12; j++)
                        {

                            //progressBar1.PerformStep();
                            if (k == PartCheckingListCount)
                            {
                                if (form1.worksheet.Cells[i, j].Value != null)
                                {

                                    string recommendChord = form1.worksheet.Cells[i, j].Value.ToString();
                                    recommendChord = recommendChord.Replace('-', 'b');
                                    if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                        recommendList.Add(recommendChord);
                                    /*
                                    genresearch(worksheet, i);
                                    artistsearch(worksheet, i);
                                    */
                                    property.genresearch(form1.worksheet, i);
                                    property.artistsearch(form1.worksheet, i);

                                    k = 0;
                                }

                            }

                            if (form1.worksheet.Cells[i, j].Value != null)
                            {
                                string chord = partCheckingList2[k];
                                chord = chord.Replace("M", "△");

                                if (form1.worksheet.Cells[i, j].Value.ToString() == chord)
                                {
                                    k++;
                                }
                                else k = 0;
                            }
                            else if (form1.worksheet.Cells[i, j].Value == null)
                            {
                                k = 0;
                            }

                        }
                        break;
                    case "A":
                        for (int j = 14; j <= 21; j++)
                        {

                            //progressBar1.PerformStep();
                            if (k == PartCheckingListCount)
                            {
                                if (form1.worksheet.Cells[i, j].Value != null)
                                {

                                    string recommendChord = form1.worksheet.Cells[i, j].Value.ToString();
                                    recommendChord = recommendChord.Replace('-', 'b');
                                    if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                        recommendList.Add(recommendChord);

                                    /*
                                    genresearch(worksheet, i);
                                    artistsearch(worksheet, i);
                                    */
                                    property.genresearch(form1.worksheet, i);
                                    property.artistsearch(form1.worksheet, i);

                                    k = 0;
                                }

                            }

                            if (form1.worksheet.Cells[i, j].Value != null)
                            {
                                string chord = partCheckingList2[k];
                                chord = chord.Replace("M", "△");

                                if (form1.worksheet.Cells[i, j].Value.ToString() == chord)
                                {
                                    k++;
                                }
                                else k = 0;
                            }
                            else if (form1.worksheet.Cells[i, j].Value == null)
                            {
                                k = 0;
                            }

                        }
                        break;
                    case "B":
                        for (int j = 23; j <= 30; j++)
                        {

                            //progressBar1.PerformStep();
                            if (k == PartCheckingListCount)
                            {
                                if (form1.worksheet.Cells[i, j].Value != null)
                                {

                                    string recommendChord = form1.worksheet.Cells[i, j].Value.ToString();
                                    recommendChord = recommendChord.Replace('-', 'b');
                                    if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                        recommendList.Add(recommendChord);

                                    /*
                                    genresearch(worksheet, i);
                                    artistsearch(worksheet, i);
                                    */
                                    property.genresearch(form1.worksheet, i);
                                    property.artistsearch(form1.worksheet, i);

                                    k = 0;
                                }

                            }

                            if (form1.worksheet.Cells[i, j].Value != null)
                            {
                                string chord = partCheckingList2[k];
                                chord = chord.Replace("M", "△");

                                if (form1.worksheet.Cells[i, j].Value.ToString() == chord)
                                {
                                    k++;
                                }
                                else k = 0;
                            }
                            else if (form1.worksheet.Cells[i, j].Value == null)
                            {
                                k = 0;
                            }

                        }
                        break;
                    case "sabi":
                        for (int j = 32; j <= 39; j++)
                        {

                            //progressBar1.PerformStep();
                            if (k == PartCheckingListCount)
                            {
                                if (form1.worksheet.Cells[i, j].Value != null)
                                {

                                    string recommendChord = form1.worksheet.Cells[i, j].Value.ToString();
                                    recommendChord = recommendChord.Replace('-', 'b');
                                    if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                        recommendList.Add(recommendChord);

                                    /*
                                    genresearch(worksheet, i);
                                    artistsearch(worksheet, i);
                                    */
                                    property.genresearch(form1.worksheet, i);
                                    property.artistsearch(form1.worksheet, i);

                                    k = 0;
                                }

                            }

                            if (form1.worksheet.Cells[i, j].Value != null)
                            {
                                string chord = partCheckingList2[k];
                                chord = chord.Replace("M", "△");

                                if (form1.worksheet.Cells[i, j].Value.ToString() == chord)
                                {
                                    k++;
                                }
                                else k = 0;
                            }
                            else if (form1.worksheet.Cells[i, j].Value == null)
                            {
                                k = 0;
                            }

                        }
                        break;
                }


            }

            /*
            richTextBox4.Clear();
            double allgenre = genreProperty[0] + genreProperty[1] + genreProperty[2] + genreProperty[3];
            richTextBox4.AppendText("POP : " + Math.Truncate((genreProperty[0] / allgenre) * 100.0 * 10) / 10 + "\n");
            richTextBox4.AppendText("ROCK : " + Math.Truncate((genreProperty[1] / allgenre) * 100.0 * 10) / 10 + "\n");
            richTextBox4.AppendText("BALLAD : " + Math.Truncate((genreProperty[2] / allgenre) * 100.0 * 10) / 10 + "\n");
            richTextBox4.AppendText("VOCALOID : " + Math.Truncate((genreProperty[3] / allgenre) * 100.0 * 10) / 10 + "\n");
            maingenreNow = maingenreDecide();

            maingenreText.Text = "現在のコード進行は" + maingenreNow + "寄りです";

            richTextBox5.Clear();
            foreach (string name in artistDictionary.Keys)
            {
                richTextBox5.AppendText(name + "\n");
            }
            artistDictionary.Clear();
             */ //実験時には使わない
            if (recommendList.Count > 0)
            {
                int RecommendListCount = recommendList.Count;
                //int ButtonListCount = buttonList.Count;
                //MessageBox.Show("コード推薦を行います・・・");
                for (int z = 0; z < RecommendListCount; z++)
                {

                    form1.buttonList[z].Enabled = true;
                    form1.buttonList[z].BackColor = Color.Orange;
                    form1.buttonList[z].Text = recommendList[z];

                }
            }
            else
            {
                if (form1.automode == 1)
                {
                    //MessageBox.Show("推薦できるコードが存在しません。他のコード進行に乗り換えれます。");
                    //Form2 f2 = new Form2(this);

                    form1.searchSimilarChords(form1.chordSoundList2);
                }
                else
                {
                    MessageBox.Show("推薦できるコードが存在しません。");
                }

            }
        }
    }
}
