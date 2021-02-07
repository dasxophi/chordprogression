using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Linq;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Forms.Button;
using System.Runtime.CompilerServices;

using OfficeOpenXml;
using IronPython.Runtime;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;


namespace chordprogression
{
    /* 色なし→データなし
       緑色→☆
       黄色→☆☆
       オレンジ色→☆☆☆ */

    public partial class Form1 : Form
    {
        Form2 f2;
        public Stack<string> chordList = new Stack<string>();
        public List<string> chordSoundList2 = new List<string>();
        List<Control> buttonList = new List<Control>();
        public List<string> partCheckingList = new List<string>();
        List<string> prevPartCheckingList = new List<string>();
        //List<string> artistList = new List<string>();
        Dictionary<string, int> artistDictionary = new Dictionary<string, int>();

        ExcelWorksheet worksheet;

        dynamic xlsxFile;
        dynamic package;

        public bool flag = false;
        public string scale;
        public string filePath = "";
        public int partCheck;
        public List<string> changeList = new List<string>();
        public int[] genreProperty = new int[2]; // [0] = pop, [1] = rock
        //public Dictionary<string, int> genreProperty2 = new Dictionary<string, int>();
        string chordName = "";
        public string maingenreNow = "";



        public Form1()
        {
            InitializeComponent();

            Image im = Image.FromFile("./EoDSi8LUUAAkLFu.jpg");

            this.BackgroundImage = im;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            foreach (Control c in Controls)
                if (c.Name.StartsWith("_"))
                    buttonList.Add(c);



            OpenFileDialog dataSheet = new OpenFileDialog();
           
            
            if (dataSheet.ShowDialog() == DialogResult.OK)
            {
                richTextBox3.Clear();
                richTextBox3.Text = dataSheet.FileName;
                filePath = dataSheet.FileName;
               

            }

            if (filePath != "")
            {
                var xlsxFile = File.OpenRead(filePath);

            }
            MessageBox.Show(filePath + " をロードしました");



            string lasttime = File.GetLastWriteTime(filePath).ToString("dd-MM-yyyy");

            MessageBox.Show(lasttime);
            FileInfo configExist = new FileInfo("./config.txt");

            if (configExist.Exists)
            {
                MessageBox.Show("exist");
                string[] configLine = File.ReadAllLines("./config.txt");
                //MessageBox.Show(configLine[0]);
                if (configLine[0] != "")
                {
                    if (configLine[0] == lasttime)
                    {
                        MessageBox.Show("更新がなかったので既存のアルゴリズムを使用");
                        StreamWriter configText = new StreamWriter("./config.txt", false);
                        configText.WriteLine(lasttime);
                        configText.WriteLine("algorithm0");
                        configText.WriteLine(configLine[2]);
                        configText.Dispose();
                        //更新がなかったので既存のアルゴリズムを使用
                    }
                    else
                    {
                        MessageBox.Show("検証作業を行う必要があります");
                        File.Delete("./config.txt");
                        StreamWriter configText = File.CreateText("./config.txt");
                        configText.WriteLine(lasttime);
                        configText.WriteLine("algorithm1");//検証作業を行う
                        configText.WriteLine(configLine[2]);
                        configText.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("検証作業を行う必要があります2");
                    StreamWriter configText = new StreamWriter("./config.txt");
                    configText.WriteLine(lasttime);
                    configText.WriteLine("algorithm1");//検証作業を行う
                    configText.WriteLine(configLine[2]);
                    configText.Dispose();
                }
                
            }
            else
            {
                MessageBox.Show("no exist");
                StreamWriter configText = File.CreateText("./config.txt");
                configText.WriteLine(lasttime);
                configText.WriteLine("algorithm1");//検証作業を行う
                configText.Dispose();
            }



        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C.wav");
            chordSound.Play();
            chordList.Push("C");
            chordSoundList2.Add("C");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dm.wav");
            chordSound.Play();
            chordList.Push("Dm");
            chordSoundList2.Add("Dm");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());

        }
        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Em.wav");
            chordSound.Play();
            chordList.Push("Em");
            chordSoundList2.Add("Em");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F.wav");
            chordSound.Play();
            chordList.Push("F");
            chordSoundList2.Add("F");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }


        private void button5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G.wav");
            chordSound.Play();
            chordList.Push("G");
            chordSoundList2.Add("G");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Am.wav");
            chordSound.Play();
            chordList.Push("Am");
            chordSoundList2.Add("Am");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bdim.wav");
            chordSound.Play();
            chordList.Push("Bdim");
            chordSoundList2.Add("Bdim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\CMa7.wav");
            chordSound.Play();
            chordList.Push("CM7");
            chordSoundList2.Add("CM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            chordList.Push("B");
            chordSoundList2.Add("B");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                scale = comboBox1.SelectedItem.ToString();

                //検証作業部on
                string[] configLineNew = File.ReadAllLines("./config.txt");

                if (configLineNew[1] == "algorithm1")
                {
                    verific();
                }
                else
                {
                    //以前の推薦アルゴリズムそのまま使う
                    string[] configLine = File.ReadAllLines("./config.txt");
                    if (configLine.Length == 3 && configLine[2] == "partCheck = 1")
                    {
                        partCheck = 1;
                    }
                    else if (configLine.Length == 3 && configLine[2] == "partCheck = 0")
                    {
                        partCheck = 0;
                    }
                    else
                    {
                        MessageBox.Show("以前のアルゴリズムの記録が存在しません。\r\n検証作業を実施します。");
                        verific();
                    }

                }
                //検証作業部off
                scaleSet();

            }


        }

        public void verific()
        {
          var xlsxFile = File.OpenRead(filePath);
          using (var package = new ExcelPackage(xlsxFile))
          {
                if (scale == "C Major")
                {
                    int count = 0;
                    string[] mainChords = {"C","Dm","Em","F","G","Am","Bdim","C△7","Dm7","Em7","F△7",
                            "G7","Am7","Bm7-5","D","E","A","C7","D7","E7","A7","Fm","Fm7","D#","G#","A#"
                        };
                    for (int i = 0; i < mainChords.Length; i++)
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[mainChords[i]];

                        if (Convert.ToInt32(worksheet.Cells[9, 16].Value) >= 1)
                        {
                            //MessageBox.Show(mainChords[i] + "は主要コードとして十分なデータが存在します。");
                            count++;
                        }

                    }
                    if (count == mainChords.Length)
                    {
                        MessageBox.Show("部分的検査アルゴリズムを適用します。");
                        partCheck = 1;
                        StreamWriter configText = new StreamWriter("./config.txt", true);
                        configText.WriteLine("partCheck = 1");
                        configText.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("単純推薦アルゴリズムを適用します。");
                        partCheck = 0;
                        StreamWriter configText = new StreamWriter("./config.txt", true);
                        configText.WriteLine("partCheck = 0");
                        configText.Dispose();
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            richTextBox2.Clear();

            //音楽理論表示部分
            string chord = richTextBox1.Text;
            if (chord.Contains("Dm-G- "))
            {
                richTextBox2.Text = "ツーファイブ";
            }

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {




        }

        private void button22_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (chordSoundList2.Count > 0)
            {
                for (int i = 0; i < chordSoundList2.Count; i++)
                {
                    
                    SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + chordSoundList2[i] + ".wav");
                    chordSound.Play();
                    Delay(1500);


                }

            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            //reset機能
            if (chordList.Count > 0)
            {
                richTextBox1.Clear();
                chordList.Clear();
                chordSoundList2.Clear();
                partCheckingList.Clear();
                richTextBox4.Clear();
                maingenreText.Text = "";
                richTextBox5.Clear();
                artistDictionary.Clear();
                MessageBox.Show("全てのコードを削除しました");
                chordSet();
                scaleSet();

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //undo機能
            if (chordList.Count > 0)
            {
                richTextBox1.Text = richTextBox1.Text.Replace(chordList.Peek() + "- ", " ");
                chordSoundList2.RemoveAt(chordSoundList2.Count - 1); //chordSoundList2の最後の要素を削除
                MessageBox.Show(chordList.Pop() + "コードを削除しました");

                if (chordList.Count > 0)
                {
                    chordSet();
                    string prevChord = chordList.Peek();
                    worksheet = package.Workbook.Worksheets[prevChord];

                    //MessageBox.Show(worksheet.Name + "から読み込んでいます");
                   // searchChord(worksheet);
                    //DeleteObject(worksheet);
                }
                else
                {
                    chordSet();
                    scaleSet();
                }


            }
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);
            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        private void scaleSet()
        {
            if (scale == "C Major")
            {
                MessageBox.Show("C Majorスケールの一つ目のコード選択に戻りました。");
                r(2, _C, _F, _Am);

                richTextBox1.Focus();
            }
            if (scale == "A Minor")
            {
                MessageBox.Show("A Minorスケールを選択しました。");
            }
        }
        public void chordSet()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].Enabled = false;
                buttonList[i].BackColor = SystemColors.Control;
            }

            //MessageBox.Show("Setting Complete");
        }

        private void r(int rating, params Control[] chord)
        {
            for (int i = 0; i < chord.Length; i++)
            {
                chord[i].Enabled = true;
                switch (rating)
                {
                    case 0:
                        chord[i].BackColor = Color.LightGreen;
                        break;
                    case 1:
                        chord[i].BackColor = Color.Yellow;
                        break;
                    case 2:
                        chord[i].BackColor = Color.Orange;
                        break;
                }
            }

        }


        /*private int ChordSumAverage()
        {

            chordName = "";
            int sum = 0;
            int count = 0;
            int sumdivcount = 0;
            for (int i = 2; i <= range.Rows.Count; i = i + 3)
            {
                // MessageBox.Show("iは" + i);

                for (int j = 1; j <= range.Columns.Count - 2; ++j)
                {
                    if ((int)worksheet.Cells[i + 2, j].Value > 0)
                    {
                        sum = sum + (int)worksheet.Cells[i + 2, j].Value;
                        count++;
                    }
                }

            }
            //DeleteObject(application.ActiveSheet);
            sumdivcount = sum / count;

            if (sumdivcount > 0)
                return (sumdivcount);
            else
                return 0;

        } */

        private void searchChord(string sheetName)
        {
            var xlsxFile = File.OpenRead(filePath);
            sheetName = sheetName.Replace("M", "△");
            using (var package = new ExcelPackage(xlsxFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                int SearchRangeRowsCount = worksheet.Dimension.Rows;
                int SearchRangeColumnsCount = worksheet.Dimension.Columns;
                chordName = "";
                for (int i = 2; i <= SearchRangeRowsCount; i = i + 3)
                {
                    // MessageBox.Show("iは" + i);

                    for (int j = 1; j <= SearchRangeColumnsCount - 2; ++j)
                    {



                        if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 10)//10
                        {

                            chordName = "_" + worksheet.Cells[i, j].Text;
                            chordName = chordName.Replace("△", "Ma");
                            chordName = chordName.Replace("mM7", "mMa7");
                            chordName = chordName.Replace("#", "s");
                            //MessageBox.Show(chordName);
                            for (int k = 0; k < buttonList.Count; k++)
                            {

                                if (buttonList[k].Name == chordName)
                                {
                                    r(2, buttonList[k]);
                                    //MessageBox.Show(buttonList[k] + "が推薦されました");

                                }


                            }



                        }
                        else if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 3 && Convert.ToInt32(worksheet.Cells[i + 2, j].Value) < 10)
                        {
                            chordName = "_" + worksheet.Cells[i, j].Text;
                            chordName = chordName.Replace("△", "Ma");
                            chordName = chordName.Replace("mM7", "mMa7");
                            chordName = chordName.Replace("#", "s");
                            //MessageBox.Show(chordName);

                            for (int k = 0; k < buttonList.Count; k++)
                            {

                                if (buttonList[k].Name == chordName)
                                {
                                    r(1, buttonList[k]);
                                    //MessageBox.Show(buttonList[k] + "が推薦されました");

                                }


                            }
                        }
                        else if (Convert.ToInt32(worksheet.Cells[i + 2, j].Value) >= 1 && Convert.ToInt32(worksheet.Cells[i + 2, j].Value) < 3)
                        {
                            chordName = "_" + worksheet.Cells[i, j].Text;
                            chordName = chordName.Replace("△", "Ma");
                            chordName = chordName.Replace("mM7", "mMa7");
                            chordName = chordName.Replace("#", "s");
                            //MessageBox.Show(chordName);
                            for (int k = 0; k < buttonList.Count; k++)
                            {

                                if (buttonList[k].Name == chordName)
                                {
                                    r(0, buttonList[k]);
                                    //MessageBox.Show(buttonList[k] + "が推薦されました");

                                }


                            }
                        }

                    }

                }
                /*
                richTextBox4.Clear();
                double allgenre = genreProperty[0] + genreProperty[1];
                richTextBox4.AppendText("POP : " + Math.Truncate((genreProperty[0] / allgenre) * 100.0 * 10) / 10 + "\n");
                richTextBox4.AppendText("ROCK : " + Math.Truncate((genreProperty[1] / allgenre) * 100.0 * 10) / 10 + "\n");
                maingenreNow = maingenreDecide();

                maingenreText.Text = "現在のコード進行は" + maingenreNow + "寄りです"; */
            }
        }

        private bool searchChord2(string nextChordName, string nowChord)
        {
            string NextChordName = nextChordName;
            string sheetName = nowChord.Replace("M", "△");
            var xlsxFile = File.OpenRead(filePath);
            using (var package = new ExcelPackage(xlsxFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                int SearchRangeRowsCount = worksheet.Dimension.Rows;
                int SearchRangeColumnsCount = worksheet.Dimension.Columns;

                //Range range = worksheet.get_Range("A2", "N37");

                for (int i = 2; i <= SearchRangeRowsCount; i = i + 3)
                {

                    // MessageBox.Show("iは" + i);

                    for (int j = 1; j <= SearchRangeColumnsCount; j++)
                    {


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
        public void genresearch(ExcelWorksheet worksheet, int i, int j)
        {
            int l = j;
            Boolean flag = true;
            while (flag == true)
            {
                if (worksheet.Cells[i, l].Value != null)
                    switch (worksheet.Cells[i, l].Value.ToString())
                    {
                        case "ポップ":
                            genreProperty[0] += 1;
                            flag = false;
                            break;
                        case "ロック":
                            genreProperty[1] += 1;
                            flag = false;
                            break;
                        default:
                            if (l == 1)
                            {
                                flag = false;
                                break;
                            }
                            else
                                break;
                    }
                l--;


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

            }
        return maingenre;
        }

        public void artistsearch(ExcelWorksheet worksheet, int i)
        {
            object artistName = worksheet.Cells[i, 3].Value;
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
        public void partCheckingRecommend(List<string> partCheckingList)
        {

            List<string> recommendList = new List<string>();
            var xlsxFile = File.OpenRead(filePath);
            using (var package = new ExcelPackage(xlsxFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["データ"];
                int RangeRowsCount = worksheet.Dimension.Rows;
                int RangeColumsCount = worksheet.Dimension.Columns;
                int PartCheckingList = partCheckingList.Count;
                int k = 0;


                for (int i = 2; i <= RangeRowsCount; i++)
                {
                    k = 0;

                    if (worksheet.Cells[i, 2].Value == null)
                    {

                        chordSet();
                        break;
                    }

                    for (int j = 4; j <= RangeColumsCount; j++)
                    {

                        if (k == PartCheckingList)
                        {

                            if (worksheet.Cells[i, j].Value != null)
                            {

                                string recommendChord = worksheet.Cells[i, j].Value.ToString();
                                if (recommendList.Contains(recommendChord) == false) //同じコードがすでに推薦リストにあるときは除く
                                    recommendList.Add(recommendChord);

                                genresearch(worksheet, i, j);
                                artistsearch(worksheet, i);

                                k = 0;
                            }

                        }

                        
                            if (worksheet.Cells[i, j].Value != null)
                            {
                                string chord = partCheckingList[k];
                                chord = chord.Replace("M", "△");

                                if (worksheet.Cells[i, j].Value.ToString() == chord)
                                {
                                    k++;
                                }
                                else k = 0;
                            } else if(worksheet.Cells[i,j].Value == null)
                            {

                            }
                        
                    }
                }
            }
        
                           
                           


                    richTextBox4.Clear();
                    double allgenre = genreProperty[0] + genreProperty[1];
                    richTextBox4.AppendText("POP : " + Math.Truncate((genreProperty[0] / allgenre) * 100.0 * 10) / 10 + "\n");
                    richTextBox4.AppendText("ROCK : " + Math.Truncate((genreProperty[1] / allgenre) * 100.0 * 10) / 10 + "\n");
                    maingenreNow = maingenreDecide();

                    maingenreText.Text = "現在のコード進行は" + maingenreNow + "寄りです";

                    richTextBox5.Clear();
                    foreach (string name in artistDictionary.Keys)
                    {
                        richTextBox5.AppendText(name + "\n");
                    }
                    artistDictionary.Clear();

                    if (recommendList.Count > 0)
                    {
                        int RecommendListCount = recommendList.Count;
                        int ButtonListCount = buttonList.Count;
                        //MessageBox.Show("コード推薦を行います・・・");
                        for (int z = 0; z < RecommendListCount; z++)
                        {
                            recommendList[z] = "_" + recommendList[z];
                            recommendList[z] = recommendList[z].Replace("△", "Ma");
                            recommendList[z] = recommendList[z].Replace("mM7", "mMa7");
                            recommendList[z] = recommendList[z].Replace("#", "s");

                            for (int x = 0; x < ButtonListCount; x++)
                            {

                                if (buttonList[x].Name == recommendList[z])
                                {
                                    r(2, buttonList[x]);
                                    //MessageBox.Show(buttonList[x].Name + "が推薦されました");

                                }


                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("推薦できるコードが存在しません。");
                    }

        }
            

                    
        
        private void DeleteObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("問題が発生しました" + ex.ToString(), "Error!");
            }
            finally
            {
                GC.Collect();
            }
        }
        private void set(string chordName)
        {
            if (chordList.Count > 1 && partCheck == 1) //部分的検査を適用
            {
                partCheckAlgorithm();
            }
            else//直前のコードだけを対象としてコードを推薦
            {
                chordSet();
                searchChord(chordName);

                //chordSumAverage();
            } 

        }

        public void partCheckAlgorithm()//部分的検査アルゴリズム
        {

            int ChordsCount = chordSoundList2.Count;
            //MessageBox.Show("chord sound list : " +chordSoundList2.Count.ToString());
            if (ChordsCount < 3)
            {
                for (int i = 0; i < ChordsCount; i++)
                {

                    if (i != ChordsCount - 1)
                    {
                        bool result = searchChord2(chordSoundList2[i + 1], chordSoundList2[i]);
                        if (result == true)
                        {
                            string chord = chordSoundList2[i];
                            partCheckingList.Add(chord);
                            //MessageBox.Show("次に行くコード進行が存在します。");
                        }
                        else if (result == false)
                        {
                            MessageBox.Show("次に行くコード進行がありません。\r\nスタックをリセットします。");
                            partCheckingList.Clear();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("部分的検査によって集まったスタックの一番目は " + partCheckingList[0]);
                        string chord = chordSoundList2[i];
                        partCheckingList.Add(chord);
                        //MessageBox.Show("最後のコードです。");

                    }

                }
            }

            else 
            {
                if (flag == false)
                    partCheckingList.RemoveAt(partCheckingList.Count - 1);
                else if (flag == true)
                    flag = false;
                for (int i = ChordsCount - 2; i < ChordsCount; i++)
                {
                    
                   
                    if (i != ChordsCount - 1)
                    {

                        bool result = searchChord2(chordSoundList2[i + 1], chordSoundList2[i]);
                        if (result == true)
                        {
                            
                            string chord = chordSoundList2[i];
                            partCheckingList.Add(chord);
                            //MessageBox.Show("次に行くコード進行が存在します。");
                        }
                        else if (result == false)
                        {
                            MessageBox.Show("次に行くコード進行がありません。\r\nスタックをリセットします。");
                            partCheckingList.Clear();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("部分的検査によって集まったスタックの一番目は " + partCheckingList[0]);
                        string chord = chordSoundList2[i];
                        partCheckingList.Add(chord);
                        //MessageBox.Show("最後のコードです。");

                    }

                }
            }

            //MessageBox.Show("部分的検査を一部終了しました。今からコード推薦を行います。");

            if (partCheckingList.Count == 0)
            {
                MessageBox.Show("部分的検査で推薦できるコードが存在しません。\r\n単純推薦を行います。");
                chordSet();
                //chordSoundList2.Last().Replace("M7", "△7");
                //worksheet = package.Workbook.Worksheets[chordSoundList2.Last()];
                chordName = chordSoundList2.Last();
                searchChord(chordName);
                //DeleteObject(worksheet);
            }
            else
            {
                partCheckingRecommend(partCheckingList);
            }



        }

        private float SimilarDegree(string chordprogression1, string chordprogression2)
        {

            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();


            var source = engine.CreateScriptSourceFromFile("C:/Users/cs17102/source/repos/n-gram/n-gram/n_gram.py");
            source.Execute(scope);


            var similardegree = scope.GetVariable<Func<string, string, float>>("similardegree");
            float similarity = similardegree(chordprogression1, chordprogression2);
            //MessageBox.Show(similarity.ToString());
            return similarity;


        }

        private void searchSimilarChords(List<string> partCheckingList)
        {
            //MessageBox.Show(worksheet.Name);
            List<string> SimilarChordsProgression = new List<string>();
            Dictionary<string, float> ChordAndSimilarity = new Dictionary<string, float>();
            var xlsxFile = File.OpenRead(filePath);
            using (var package = new ExcelPackage(xlsxFile))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets["データ"];
                int RangeRowsCount = worksheet.Dimension.Rows;
                int RangeColumsCount = worksheet.Dimension.Columns;
                int PartCheckingListCount = partCheckingList.Count;
                List<string> SimilarChordsProgressionView = new List<string>();
                List<float> DegreeOfSimilarList = new List<float>();

                int k = 0;

                for (int l = 0; l < PartCheckingListCount; l++)
                {
                    //MessageBox.Show(partCheckingList[l]);
                }

                for (int i = 2; i <= RangeRowsCount; i++)
                {
                    k = 0;
                    //MessageBox.Show(i+"行");
                    if (worksheet.Cells[i, 2].Value == null)
                    {
                        //chordSet();
                        break;
                    }

                    for (int j = 4; j <= RangeColumsCount; j++)
                    {
                        
                        if (k == PartCheckingListCount)
                        {
                            //ここで類似度検査
                            k = 0;
                            string listToString = string.Join("-", SimilarChordsProgression.ToArray());
                            string listToString2 = string.Join("-", partCheckingList.ToArray());

                            if (ChordAndSimilarity.ContainsKey(listToString) == false)
                            {
                                //MessageBox.Show(listToString);
                                float similarity = SimilarDegree(listToString, listToString2);

                                genresearch(worksheet, i, j);
                                string maingenreNew = maingenreDecide();

                                if (maingenreNow == maingenreNew)
                                {
                                    similarity += (float)3.0; //元のコード進行とメインジャンルが一致したら類似度+3.0

                                }
                                    
                                ChordAndSimilarity.Add(listToString, similarity);
                                SimilarChordsProgression.Clear();
                                
                                

                            } else
                            {
                                SimilarChordsProgression.Clear();
                            }
                            


                        }
                        if (worksheet.Cells[i, j].Value != null)
                        {
                            if (worksheet.Cells[i ,j+1].Value != null)
                            {
                                string chord = worksheet.Cells[i, j].Value.ToString();
                                chord = chord.Replace("△", "M");
                                SimilarChordsProgression.Add(chord);
                                k++;
                            } else
                            {
                                SimilarChordsProgression.Clear();
                                k = 0;
                            }

                                
                            
                        }
                        else if (worksheet.Cells[i, j].Value == null)
                        {
                            SimilarChordsProgression.Clear();
                            k = 0;
                            
                        }




                    }
                }


                
                Form2 form2 = new Form2(this);

                if(ChordAndSimilarity.Count != 0)
                {
                    var ChordAndSimilarityDescend = ChordAndSimilarity.OrderByDescending(x => x.Value);//類似度を降順でソート
                    foreach(var item in ChordAndSimilarityDescend) 
                    {
                        if(item.Value >= 1) //filter
                        form2.checkedListBox1.Items.Add(item);
                            
                    }
                    
                    form2.Show();
                    
                } else
                {
                    MessageBox.Show("類似コード進行がありません");
                }
                

            }
        }



        private void button26_Click(object sender, EventArgs e)
        {
            chordList.Push("C#");
            chordSoundList2.Add("C#");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dm7.wav");
            chordSound.Play();
            chordList.Push("Dm7");
            chordSoundList2.Add("Dm7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Em7.wav");
            chordSound.Play();
            chordList.Push("Em7");
            chordSoundList2.Add("Em7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\FMa7.wav");
            chordSound.Play();
            chordList.Push("FM7");
            chordSoundList2.Add("FM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G7.wav");
            chordSound.Play();
            chordList.Push("G7");
            chordSoundList2.Add("G7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Am7.wav");
            chordSound.Play();
            chordList.Push("Am7");
            chordSoundList2.Add("Am7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void button138_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bm.wav");
            chordSound.Play();
            chordList.Push("Bm");
            chordSoundList2.Add("Bm");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Bdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bdim7.wav");
            chordSound.Play();
            chordList.Push("Bdim7");
            chordSoundList2.Add("Bdim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cm.wav");
            chordSound.Play();
            chordList.Push("Cm");
            chordSoundList2.Add("Cm");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _D_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D.wav");
            chordSound.Play();
            chordList.Push("D");
            chordSoundList2.Add("D");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _E_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\E.wav");
            chordSound.Play();
            chordList.Push("E");
            chordSoundList2.Add("E");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fm.wav");
            chordSound.Play();
            chordList.Push("Fm");
            chordSoundList2.Add("Fm");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gm.wav");
            chordSound.Play();
            chordList.Push("Gm");
            chordSoundList2.Add("Gm");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _A_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A.wav");
            chordSound.Play();
            chordList.Push("A");
            chordSoundList2.Add("A");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _C7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C7.wav");
            chordSound.Play();
            chordList.Push("C7");
            chordSoundList2.Add("C7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _D7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D7.wav");
            chordSound.Play();
            chordList.Push("D7");
            chordSoundList2.Add("D7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _E7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\E7.wav");
            chordSound.Play();
            chordList.Push("E7");
            chordSoundList2.Add("E7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fm7.wav");
            chordSound.Play();
            chordList.Push("Fm7");
            chordSoundList2.Add("Fm7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gm7.wav");
            chordSound.Play();
            chordList.Push("Gm7");
            chordSoundList2.Add("Gm7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _A7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A7.wav");
            chordSound.Play();
            chordList.Push("A7");
            chordSoundList2.Add("A7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _B7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\B7.wav");
            chordSound.Play();
            chordList.Push("B7");
            chordSoundList2.Add("B7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cm7.wav");
            chordSound.Play();
            chordList.Push("Cm7");
            chordSoundList2.Add("Cm7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _DMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\DMa7.wav");
            chordSound.Play();
            chordList.Push("DM7");
            chordSoundList2.Add("DM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _EMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\EMa7.wav");
            chordSound.Play();
            chordList.Push("EM7");
            chordSoundList2.Add("EM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _F7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F7.wav");
            chordSound.Play();
            chordList.Push("F7");
            chordSoundList2.Add("F7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _GMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\GMa7.wav");
            chordSound.Play();
            chordList.Push("GM7");
            chordSoundList2.Add("GM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _AMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\AMa7.wav");
            chordSound.Play();
            chordList.Push("AM7");
            chordSoundList2.Add("AM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _BMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\BMa7.wav");
            chordSound.Play();
            chordList.Push("BM7");
            chordSoundList2.Add("BM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _CmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\CmM7.wav");
            chordSound.Play();
            chordList.Push("CmM7");
            chordSoundList2.Add("CmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _DmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\DmM7.wav");
            chordSound.Play();
            chordList.Push("DmM7");
            chordSoundList2.Add("DmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _EmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\EmM7.wav");
            chordSound.Play();
            chordList.Push("EmM7");
            chordSoundList2.Add("EmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _FmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\FmM7.wav");
            chordSound.Play();
            chordList.Push("FmM7");
            chordSoundList2.Add("FmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _GmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\GmM7.wav");
            chordSound.Play();
            chordList.Push("GmM7");
            chordSoundList2.Add("GmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _AmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\AmM7.wav");
            chordSound.Play();
            chordList.Push("AmM7");
            chordSoundList2.Add("AmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _BmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\BmM7.wav");
            chordSound.Play();
            chordList.Push("BmM7");
            chordSoundList2.Add("BmM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Csus4.wav");
            chordSound.Play();
            chordList.Push("Csus4");
            chordSoundList2.Add("Csus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dsus4.wav");
            chordSound.Play();
            chordList.Push("Dsus4");
            chordSoundList2.Add("Dsus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Esus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Esus4.wav");
            chordSound.Play();
            chordList.Push("Esus4");
            chordSoundList2.Add("Esus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fsus4.wav");
            chordSound.Play();
            chordList.Push("Fsus4");
            chordSoundList2.Add("Fsus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gsus4.wav");
            chordSound.Play();
            chordList.Push("Gsus4");
            chordSoundList2.Add("Gsus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Asus4.wav");
            chordSound.Play();
            chordList.Push("Asus4");
            chordSoundList2.Add("Asus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Bsus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bsus4.wav");
            chordSound.Play();
            chordList.Push("Bsus4");
            chordSoundList2.Add("Bsus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _C7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C7sus4.wav");
            chordSound.Play();
            chordList.Push("C7sus4");
            chordSoundList2.Add("C7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _D7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D7sus4.wav");
            chordSound.Play();
            chordList.Push("D7sus4");
            chordSoundList2.Add("D7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _E7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\E7sus4.wav");
            chordSound.Play();
            chordList.Push("E7sus4");
            chordSoundList2.Add("E7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _F7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F7sus4.wav");
            chordSound.Play();
            chordList.Push("F7sus4");
            chordSoundList2.Add("F7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _G7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G7sus4.wav");
            chordSound.Play();
            chordList.Push("G7sus4");
            chordSoundList2.Add("G7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _A7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A7sus4.wav");
            chordSound.Play();
            chordList.Push("A7sus4");
            chordSoundList2.Add("A7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _B7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\B7sus4.wav");
            chordSound.Play();
            chordList.Push("B7sus4");
            chordSoundList2.Add("B7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cdim.wav");
            chordSound.Play();
            chordList.Push("Cdim");
            chordSoundList2.Add("Cdim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ddim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Ddim.wav");
            chordSound.Play();
            chordList.Push("Ddim");
            chordSoundList2.Add("Ddim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Edim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Edim.wav");
            chordSound.Play();
            chordList.Push("Edim");
            chordSoundList2.Add("Edim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fdim.wav");
            chordSound.Play();
            chordList.Push("Fdim");
            chordSoundList2.Add("Fdim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gdim.wav");
            chordSound.Play();
            chordList.Push("Gdim");
            chordSoundList2.Add("Gdim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Adim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Adim.wav");
            chordSound.Play();
            chordList.Push("Adim");
            chordSoundList2.Add("Adim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cdim7.wav");
            chordSound.Play();
            chordList.Push("Cdim7");
            chordSoundList2.Add("Cdim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ddim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Ddim7.wav");
            chordSound.Play();
            chordList.Push("Ddim7");
            chordSoundList2.Add("Ddim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Edim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Edim7.wav");
            chordSound.Play();
            chordList.Push("Edim7");
            chordSoundList2.Add("Edim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fdim7.wav");
            chordSound.Play();
            chordList.Push("Fdim7");
            chordSoundList2.Add("Fdim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gdim7.wav");
            chordSound.Play();
            chordList.Push("Gdim7");
            chordSoundList2.Add("Gdim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Adim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Adim7.wav");
            chordSound.Play();
            chordList.Push("Adim7");
            chordSoundList2.Add("Adim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Bm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bm7.wav");
            chordSound.Play();
            chordList.Push("Bm7");
            chordSoundList2.Add("Bm7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cm7b5.wav");
            chordSound.Play();
            chordList.Push("Cm7-5");
            chordSoundList2.Add("Cm7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dm7b5.wav");
            chordSound.Play();
            chordList.Push("Dm7-5");
            chordSoundList2.Add("Dm7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Em7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Em7b5.wav");
            chordSound.Play();
            chordList.Push("Em7-5");
            chordSoundList2.Add("Em7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fm7b5.wav");
            chordSound.Play();
            chordList.Push("Fm7-5");
            chordSoundList2.Add("Fm7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gm7b5.wav");
            chordSound.Play();
            chordList.Push("Gm7-5");
            chordSoundList2.Add("Gm7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Am7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Am7b5.wav");
            chordSound.Play();
            chordList.Push("Am7-5");
            chordSoundList2.Add("Am7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Bm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bm7b5.wav");
            chordSound.Play();
            chordList.Push("Bm7-5");
            chordSoundList2.Add("Bm7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Caug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Caug.wav");
            chordSound.Play();
            chordList.Push("Caug");
            chordSoundList2.Add("Caug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Daug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Daug.wav");
            chordSound.Play();
            chordList.Push("Daug");
            chordSoundList2.Add("Daug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Eaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Eaug.wav");
            chordSound.Play();
            chordList.Push("Eaug");
            chordSoundList2.Add("Eaug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Faug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Faug.wav");
            chordSound.Play();
            chordList.Push("Faug");
            chordSoundList2.Add("Faug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gaug.wav");
            chordSound.Play();
            chordList.Push("Gaug");
            chordSoundList2.Add("Gaug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Aaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Aaug.wav");
            chordSound.Play();
            chordList.Push("Aaug");
            chordSoundList2.Add("Aaug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Baug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Baug.wav");
            chordSound.Play();
            chordList.Push("Baug");
            chordSoundList2.Add("Baug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cadd9.wav");
            chordSound.Play();
            chordList.Push("Cadd9");
            chordSoundList2.Add("Cadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dadd9.wav");
            chordSound.Play();
            chordList.Push("Dadd9");
            chordSoundList2.Add("Dadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Eadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Eadd9.wav");
            chordSound.Play();
            chordList.Push("Eadd9");
            chordSoundList2.Add("Eadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fadd9.wav");
            chordSound.Play();
            chordList.Push("Fadd9");
            chordSoundList2.Add("Fadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gadd9.wav");
            chordSound.Play();
            chordList.Push("Gadd9");
            chordSoundList2.Add("Gadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Aadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Aadd9.wav");
            chordSound.Play();
            chordList.Push("Aadd9");
            chordSoundList2.Add("Aadd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Badd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Badd9.wav");
            chordSound.Play();
            chordList.Push("Badd9");
            chordSoundList2.Add("Badd9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _C6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C6.wav");
            chordSound.Play();
            chordList.Push("C6");
            chordSoundList2.Add("C6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _D6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D6.wav");
            chordSound.Play();
            chordList.Push("D6");
            chordSoundList2.Add("D6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _E6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\E6.wav");
            chordSound.Play();
            chordList.Push("E6");
            chordSoundList2.Add("E6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _F6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F6.wav");
            chordSound.Play();
            chordList.Push("F6");
            chordSoundList2.Add("F6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _G6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G6.wav");
            chordSound.Play();
            chordList.Push("G6");
            chordSoundList2.Add("G6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _A6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A6.wav");
            chordSound.Play();
            chordList.Push("A6");
            chordSoundList2.Add("A6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _B6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\B6.wav");
            chordSound.Play();
            chordList.Push("B6");
            chordSoundList2.Add("B6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Cm6.wav");
            chordSound.Play();
            chordList.Push("Cm6");
            chordSoundList2.Add("Cm6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Dm6.wav");
            chordSound.Play();
            chordList.Push("Dm6");
            chordSoundList2.Add("Dm6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Em6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Em6.wav");
            chordSound.Play();
            chordList.Push("Em6");
            chordSoundList2.Add("Em6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Fm6.wav");
            chordSound.Play();
            chordList.Push("Fm6");
            chordSoundList2.Add("Fm6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Gm6.wav");
            chordSound.Play();
            chordList.Push("Gm6");
            chordSoundList2.Add("Gm6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Am6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Am6.wav");
            chordSound.Play();
            chordList.Push("Am6");
            chordSoundList2.Add("Am6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Bm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\Bm6.wav");
            chordSound.Play();
            chordList.Push("Bm6");
            chordSoundList2.Add("Bm6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ds_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#.wav");
            chordSound.Play();
            chordList.Push("D#");
            chordSoundList2.Add("D#");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fs_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#.wav");
            chordSound.Play();
            chordList.Push("F#");
            chordSoundList2.Add("F#");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gs_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#.wav");
            chordSound.Play();
            chordList.Push("G#");
            chordSoundList2.Add("G#");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _As_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#.wav");
            chordSound.Play();
            chordList.Push("A#");
            chordSoundList2.Add("A#");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cs7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#7.wav");
            chordSound.Play();
            chordList.Push("C#7");
            chordSoundList2.Add("C#7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ds7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#7.wav");
            chordSound.Play();
            chordList.Push("D#7");
            chordSoundList2.Add("D#7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fs7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#7.wav");
            chordSound.Play();
            chordList.Push("F#7");
            chordSoundList2.Add("F#7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gs7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#7.wav");
            chordSound.Play();
            chordList.Push("G#7");
            chordSoundList2.Add("G#7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _As7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#7.wav");
            chordSound.Play();
            chordList.Push("A#7");
            chordSoundList2.Add("A#7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#m.wav");
            chordSound.Play();
            chordList.Push("C#m");
            chordSoundList2.Add("C#m");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#m.wav");
            chordSound.Play();
            chordList.Push("D#m");
            chordSoundList2.Add("D#m");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#m.wav");
            chordSound.Play();
            chordList.Push("F#m");
            chordSoundList2.Add("F#m");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#m.wav");
            chordSound.Play();
            chordList.Push("G#m");
            chordSoundList2.Add("G#m");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asm_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#m.wav");
            chordSound.Play();
            chordList.Push("A#m");
            chordSoundList2.Add("A#m");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#m7.wav");
            chordSound.Play();
            chordList.Push("C#m7");
            chordSoundList2.Add("C#m7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#m7.wav");
            chordSound.Play();
            chordList.Push("D#m7");
            chordSoundList2.Add("D#m7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#m7.wav");
            chordSound.Play();
            chordList.Push("F#m7");
            chordSoundList2.Add("F#m7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#m7.wav");
            chordSound.Play();
            chordList.Push("G#m7");
            chordSoundList2.Add("G#m7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asm7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#m7.wav");
            chordSound.Play();
            chordList.Push("A#m7");
            chordSoundList2.Add("A#m7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _CsMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#Ma7.wav");
            chordSound.Play();
            chordList.Push("C#M7");
            chordSoundList2.Add("C#M7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _DsMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#Ma7.wav");
            chordSound.Play();
            chordList.Push("D#M7");
            chordSoundList2.Add("D#M7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _FsMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#Ma7.wav");
            chordSound.Play();
            chordList.Push("F#M7");
            chordSoundList2.Add("F#M7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _GsMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#Ma7.wav");
            chordSound.Play();
            chordList.Push("G#M7");
            chordSoundList2.Add("G#M7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _AsMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#Ma7.wav");
            chordSound.Play();
            chordList.Push("A#M7");
            chordSoundList2.Add("A#M7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _CsmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#mM7.wav");
            chordSound.Play();
            chordList.Push("C#mM7");
            chordSoundList2.Add("C#mM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _DsmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#mMa7.wav");
            chordSound.Play();
            chordList.Push("D#mM7");
            chordSoundList2.Add("D#mM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _FsmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#mMa7.wav");
            chordSound.Play();
            chordList.Push("F#mM7");
            chordSoundList2.Add("F#mM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _GsmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#mMa7.wav");
            chordSound.Play();
            chordList.Push("G#mM7");
            chordSoundList2.Add("G#mM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _AsmMa7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#mMa7.wav");
            chordSound.Play();
            chordList.Push("A#mM7");
            chordSoundList2.Add("A#mM7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cssus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#sus4.wav");
            chordSound.Play();
            chordList.Push("C#sus4");
            chordSoundList2.Add("C#sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dssus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#sus4.wav");
            chordSound.Play();
            chordList.Push("D#sus4");
            chordSoundList2.Add("D#sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fssus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#sus4.wav");
            chordSound.Play();
            chordList.Push("F#sus4");
            chordSoundList2.Add("F#sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gssus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#sus4.wav");
            chordSound.Play();
            chordList.Push("G#sus4");
            chordSoundList2.Add("G#sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Assus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#sus4.wav");
            chordSound.Play();
            chordList.Push("A#sus4");
            chordSoundList2.Add("A#sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cs7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#7sus4.wav");
            chordSound.Play();
            chordList.Push("C#7sus4");
            chordSoundList2.Add("C#7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ds7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#7sus4.wav");
            chordSound.Play();
            chordList.Push("D#7sus4");
            chordSoundList2.Add("D#7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fs7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#7sus4.wav");
            chordSound.Play();
            chordList.Push("F#7sus4");
            chordSoundList2.Add("F#7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gs7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#7sus4.wav");
            chordSound.Play();
            chordList.Push("G#7sus4");
            chordSoundList2.Add("G#7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _As7sus4_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#7sus4.wav");
            chordSound.Play();
            chordList.Push("A#7sus4");
            chordSoundList2.Add("A#7sus4");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#dim.wav");
            chordSound.Play();
            chordList.Push("C#dim");
            chordSoundList2.Add("C#dim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#dim.wav");
            chordSound.Play();
            chordList.Push("D#dim");
            chordSoundList2.Add("D#dim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#dim.wav");
            chordSound.Play();
            chordList.Push("F#dim");
            chordSoundList2.Add("F#dim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#dim.wav");
            chordSound.Play();
            chordList.Push("G#dim");
            chordSoundList2.Add("G#dim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asdim_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#dim.wav");
            chordSound.Play();
            chordList.Push("A#dim");
            chordSoundList2.Add("A#dim");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#dim7.wav");
            chordSound.Play();
            chordList.Push("C#dim7");
            chordSoundList2.Add("C#dim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#dim7.wav");
            chordSound.Play();
            chordList.Push("D#dim7");
            chordSoundList2.Add("D#dim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#dim7.wav");
            chordSound.Play();
            chordList.Push("F#dim7");
            chordSoundList2.Add("F#dim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#dim7.wav");
            chordSound.Play();
            chordList.Push("G#dim7");
            chordSoundList2.Add("G#dim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asdim7_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#dim7.wav");
            chordSound.Play();
            chordList.Push("A#dim7");
            chordSoundList2.Add("A#dim7");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#m7b5.wav");
            chordSound.Play();
            chordList.Push("C#m7-5");
            chordSoundList2.Add("C#m7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#m7b5.wav");
            chordSound.Play();
            chordList.Push("D#m7-5");
            chordSoundList2.Add("D#m7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#m7b5.wav");
            chordSound.Play();
            chordList.Push("F#m7-5");
            chordSoundList2.Add("F#m7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#m7b5.wav");
            chordSound.Play();
            chordList.Push("G#m7-5");
            chordSoundList2.Add("G#m7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asm7b5_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#m7b5.wav");
            chordSound.Play();
            chordList.Push("A#m7-5");
            chordSoundList2.Add("A#m7-5");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#aug.wav");
            chordSound.Play();
            chordList.Push("C#aug");
            chordSoundList2.Add("C#aug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#aug.wav");
            chordSound.Play();
            chordList.Push("D#aug");
            chordSoundList2.Add("D#aug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#aug.wav");
            chordSound.Play();
            chordList.Push("F#aug");
            chordSoundList2.Add("F#aug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#aug.wav");
            chordSound.Play();
            chordList.Push("G#aug");
            chordSoundList2.Add("G#aug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asaug_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#aug.wav");
            chordSound.Play();
            chordList.Push("A#aug");
            chordSoundList2.Add("A#aug");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#add9.wav");
            chordSound.Play();
            chordList.Push("C#add9");
            chordSoundList2.Add("C#add9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#add9.wav");
            chordSound.Play();
            chordList.Push("D#add9");
            chordSoundList2.Add("D#add9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#add9.wav");
            chordSound.Play();
            chordList.Push("F#add9");
            chordSoundList2.Add("F#add9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#add9.wav");
            chordSound.Play();
            chordList.Push("G#add9");
            chordSoundList2.Add("G#add9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asadd9_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#add9.wav");
            chordSound.Play();
            chordList.Push("A#add9");
            chordSoundList2.Add("A#add9");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Cs6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#6.wav");
            chordSound.Play();
            chordList.Push("C#6");
            chordSoundList2.Add("C#6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Ds6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#6.wav");
            chordSound.Play();
            chordList.Push("D#6");
            chordSoundList2.Add("D#6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fs6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#6.wav");
            chordSound.Play();
            chordList.Push("F#6");
            chordSoundList2.Add("F#6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gs6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#6.wav");
            chordSound.Play();
            chordList.Push("G#6");
            chordSoundList2.Add("G#6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _As6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#6.wav");
            chordSound.Play();
            chordList.Push("A#6");
            chordSoundList2.Add("A#6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Csm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\C#m6.wav");
            chordSound.Play();
            chordList.Push("C#m6");
            chordSoundList2.Add("C#m6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Dsm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\D#m6.wav");
            chordSound.Play();
            chordList.Push("D#m6");
            chordSoundList2.Add("D#m6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Fsm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\F#m6.wav");
            chordSound.Play();
            chordList.Push("F#m6");
            chordSoundList2.Add("F#m6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Gsm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\G#m6.wav");
            chordSound.Play();
            chordList.Push("G#m6");
            chordSoundList2.Add("G#m6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void _Asm6_Click(object sender, EventArgs e)
        {
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\A#m6.wav");
            chordSound.Play();
            chordList.Push("A#m6");
            chordSoundList2.Add("A#m6");
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
        }

        private void random_Click_1(object sender, EventArgs e)
        {
            /*
            Random random = new Random();

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].Enabled = true;
                buttonList[i].BackColor = Color.Orange;
            }

            MessageBox.Show("ランダムでコード進行を作成します");

            int number = 0;
            string name;

            for (int j = 0; j < 12; j++)
            {
                number = random.Next(0, buttonList.Count);
                name = buttonList[number].Name;
                name = name.Replace("_", "");
                name = name.Replace("s", "#");
                name = name.Replace("#u#4", "sus4");
                name = name.Replace("Ma", "M");
                chordList.Push(name);
                chordSoundList2.Add(name);
                richTextBox1.Text = richTextBox1.Text.TrimEnd();
                richTextBox1.AppendText(chordList.Peek() + "- ");
            }
            */
        }

        private void random2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {

                    if (buttonList[i].Enabled == true )
                    {
                        randombuttonList.Add(buttonList[i]);


                    }


                }
                string name = "";
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Name;
                name = name.Replace("_", "");
                name = name.Replace("s", "#");
                name = name.Replace("#u#4", "sus4");
                name = name.Replace("Ma", "M");
                chordList.Push(name);
                chordSoundList2.Add(name);
                richTextBox1.Text = richTextBox1.Text.TrimEnd();
                richTextBox1.AppendText(chordList.Peek() + "- ");
                set(chordList.Peek());
                randombuttonList.Clear();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            //Range range = worksheet.UsedRange;
            searchSimilarChords(partCheckingList);
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
