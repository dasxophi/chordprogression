﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Linq;
using System.Threading;
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
        public List<string> partCheckingList = new List<string>();
        public List<Control> buttonList = new List<Control>();
        List<string> prevPartCheckingList = new List<string>();
        public Dictionary<string, int> artistDictionary = new Dictionary<string, int>();

        public ExcelWorksheet worksheet;


        public dynamic xlsxFile;
        public dynamic package;

        public bool flag = false;
        public string scale;
        public string filePath = "";
        public int partCheck;
        public List<string> changeList = new List<string>();
        public int[] genreProperty = new int[4]; // [0] = pop, [1] = rock, [2] = ballad, [3] = vocaloid
        public string chordName = "";
        public string maingenreNow = "";
        public int automode = 0;
        public string part = "";
        public List<string> prevChangedChords = new List<string>();

        public int RangeRowsCount;
        public int RangeColumsCount;
        public int PartCheckingListCount;

        public Form1()
        {
            InitializeComponent();

            //Image im = Image.FromFile();

            //this.BackgroundImage = im;

            //this.BackgroundImageLayout = ImageLayout.Stretch;

            
            foreach (Control c in this.Controls.Cast<Control>()
                                         .OrderBy(c => c.TabIndex))
                if (c.Name.StartsWith("_"))
                {
                    buttonList.Add(c);

                }

            /*
            OpenFileDialog dataSheet = new OpenFileDialog();


            if (dataSheet.ShowDialog() == DialogResult.OK)
            {
                richTextBox3.Clear();
                richTextBox3.Text = dataSheet.FileName;
                filePath = dataSheet.FileName;


            }
            
            if (filePath != "")
            {
                //var xlsxFile = File.OpenRead(filePath);
            }
            */
            filePath = "./chorddata.xlsx"; //配布時に追加
            var xlsxFile = File.OpenRead(filePath);
            if(filePath != null)
            MessageBox.Show("コード進行データベースのロードに成功しました。\n起動まで少々お待ちください。");


            xlsxFile = File.OpenRead(filePath);
            package = new ExcelPackage(xlsxFile) ;

            worksheet = package.Workbook.Worksheets["データ"];
            RangeRowsCount = worksheet.Dimension.Rows;
            RangeColumsCount = worksheet.Dimension.Columns;
            

            string lasttime = File.GetLastWriteTime(filePath).ToString("dd-MM-yyyy"); //現在の日時取得

            //MessageBox.Show(lasttime);
            FileInfo configExist = new FileInfo("./config.txt");

            if (configExist.Exists)
            {
                MessageBox.Show("既存設定が存在します。");
                string[] configLine = File.ReadAllLines("./config.txt");
                if (configLine[0] != "")
                {
                    if (configLine[0] == lasttime)
                    {
                        MessageBox.Show("データの更新がなかったので既存設定を適用します。");
                        StreamWriter configText = new StreamWriter("./config.txt", false);
                        configText.WriteLine(lasttime);
                        configText.WriteLine("algorithm0");
                        configText.WriteLine(configLine[2]);
                        configText.Dispose();
                        //更新がなかったので既存のアルゴリズムを使用
                    }
                    else
                    {
                        MessageBox.Show("検証作業を行う必要があります。");
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
                MessageBox.Show("既存設定が存在しません。");
                StreamWriter configText = File.CreateText("./config.txt");
                configText.WriteLine(lasttime);
                configText.WriteLine("algorithm1");//検証作業を行う
                configText.Dispose();
            }

            

        }


       

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (comboBox1.SelectedIndex > -1)
            {
                scale = comboBox1.SelectedItem.ToString();
                Verific verificAlgorithm = new Verific(filePath, scale);
                //検証作業部on
                string[] configLineNew = File.ReadAllLines("./config.txt");

                if (configLineNew[1] == "algorithm1")
                {
                    partCheck = verificAlgorithm.verific();
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
                        partCheck = verificAlgorithm.verific();
                    }

                }
                //検証作業部off
                scaleSet();

            }


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

        private void button22_Click(object sender, EventArgs e)　//終了
        {
            ((IDisposable)package).Dispose();
            Application.Exit();
        }

        private void button25_Click(object sender, EventArgs e)　//全てのコード再生
        {
            string sound;
            if (chordSoundList2.Count > 0)
            {
                for (int i = 0; i < chordSoundList2.Count; i++)
                {
                    sound = chordSoundList2[i];
                    sound = sound.Replace("△", "Ma");
                    sound = sound.Replace("-", "b");
                    sound = sound.Replace("/", "_");
                    SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + sound + ".wav");
                    chordSound.Play();
                    Delay(1500);


                }

            }

        }

        private void button24_Click(object sender, EventArgs e) // RESET
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
                progressBar1.Value = 0;
                richTextBox5.Clear();
                richTextBox4.Clear();
                MessageBox.Show("全てのコードを削除しました");
                chordSet();
                scaleSet();

            }
        }

        private void button23_Click(object sender, EventArgs e)  //コードの削除
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

        private void scaleSet() //スケールの選択。現在Cスケールのみ使用可能
        {
            if (scale == "C Major")
            {
                MessageBox.Show("C Majorスケールの一つ目のコード選択に戻りました。");
                buttonList[0].Enabled = true;
                buttonList[0].BackColor = Color.Orange;
                _button1.Text = "C";
                buttonList[1].Enabled = true;
                buttonList[1].BackColor = Color.Orange;
                _button2.Text = "F";
                buttonList[2].Enabled = true;
                buttonList[2].BackColor = Color.Orange;
                _button3.Text = "Am";
                buttonList[3].Enabled = true;
                buttonList[3].BackColor = Color.Orange;
                _button4.Text = "Dm";

                richTextBox1.Focus();
            }
            if (scale == "A Minor")
            {
                MessageBox.Show("A Minorスケールを選択しました。");
            }
        }
        public void chordSet() //ボタンやテキストなどを初期状態にセット
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].Text = "";
                buttonList[i].Enabled = false;
                buttonList[i].BackColor = SystemColors.Control;
            }
        }
        public void set(string chordName) //  アルゴリズム選択
        {
            Array.Clear(genreProperty, 0, genreProperty.Length);
            if (partCheck == 1 && chordList.Count > 1) //多重推定を適用　chordList.Count 要らない？
            {
                //MessageBox.Show("set !!");
                partCheckAlgorithm(partCheckingList);
                //chordSet();
                //searchChord(chordName);
            }
            else//直前のコードだけを対象としてコードを推薦(単純推定)
            {
                chordSet();
                searchChord(chordName);

                //chordSumAverage();
            }

        }

        public void chordButton(string buttonName)　//コードのボタンを押したときの動作
        {
            //chordSet();
            chordList.Push(buttonName);
            chordSoundList2.Add(buttonName);
            richTextBox1.Text = richTextBox1.Text.TrimEnd();
            richTextBox1.AppendText(chordList.Peek() + "- ");
            set(chordList.Peek());
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

        public void searchChord(string sheetName) //単純推定そのもの
        {
            ChordRecommendAlgorithm chordRecommendAlgorithm = new ChordRecommendAlgorithm();
            chordRecommendAlgorithm.excelSet(filePath);
            chordRecommendAlgorithm.simpleRecommend(this,sheetName);
            
        }

        public bool searchChord2(string nextChordName, string nowChord) //多重推定のアルゴリズム
        {
            ChordRecommendAlgorithm chordRecommendAlgorithm = new ChordRecommendAlgorithm();
            chordRecommendAlgorithm.excelSet(filePath);
            return chordRecommendAlgorithm.multipleDetailed(nextChordName , nowChord);

        }

        public void partCheckingRecommend(ref List<string> partCheckingList) //多重推定を適用
        {
            ChordRecommendAlgorithm chordRecommendAlgorithm = new ChordRecommendAlgorithm();
            chordRecommendAlgorithm.multipleRecommend(this, ref partCheckingList);

        }

        public void partCheckingRecommend2(ref List<string> partCheckingList)  //パート別自動生成時に使う
        {
            ChordRecommendAlgorithm chordRecommendAlgorithm = new ChordRecommendAlgorithm();
            chordRecommendAlgorithm.multipleRecommend2(this, ref partCheckingList);


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


        public void partCheckAlgorithm(List<string> partCheckingList)//多重推定アルゴリズム List<string> partCheckingList
        {

            ChordRecommendAlgorithm chordRecommendAlgorithm = new ChordRecommendAlgorithm();
            chordRecommendAlgorithm.multipleAlgorithm(this, ref partCheckingList);

        }

        private float SimilarDegree(string chordprogression1, string chordprogression2)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();

            //var source = engine.CreateScriptSourceFromFile("C:/Users/admin/source/repos/n-gram/n-gram/n_gram.py");
            var source = engine.CreateScriptSourceFromFile("./n_gram.py");
            source.Execute(scope);

            var similardegree = scope.GetVariable<Func<string, string, float>>("similardegree");
            float similarity = similardegree(chordprogression1, chordprogression2);
            return similarity;
        }

        public void searchSimilarChords(List<string> partCheckingList)
        {

            List<string> SimilarChordsProgression = new List<string>();
            Dictionary<string, float> ChordAndSimilarity = new Dictionary<string, float>();
            
                //ExcelWorksheet worksheet = package.Workbook.Worksheets["データ"];
                //int RangeRowsCount = worksheet.Dimension.Rows;
                //int RangeColumsCount = worksheet.Dimension.Columns;
                int PartCheckingListCount = partCheckingList.Count;
                List<string> SimilarChordsProgressionView = new List<string>();
                List<float> DegreeOfSimilarList = new List<float>();

                int k = 0;

                for (int i = 2; i <= RangeRowsCount; i++)
                {
                    k = 0;
                    //MessageBox.Show(i+"行");
                    if (worksheet.Cells[i, 3].Value == null)
                    {
                        //chordSet();
                        break;
                    }

                    for (int j = 5; j <= RangeColumsCount; j++) //4を5に変更
                    {

                        progressBar1.PerformStep();

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


                                Property property = new Property(ref genreProperty, ref artistDictionary);
                                property.genresearch(worksheet,i);
                                string maingenreNew = property.maingenreDecide();
                                /*genresearch(worksheet, i);
                                string maingenreNew = maingenreDecide(); */


                                if (maingenreNow == maingenreNew)
                                {
                                    similarity += (float)3.0; //元のコード進行とメインジャンルが一致したら類似度+3.0

                                }

                                ChordAndSimilarity.Add(listToString, similarity);
                                SimilarChordsProgression.Clear();

                            }
                            else
                            {
                                SimilarChordsProgression.Clear();
                            }


                        }
                        if (worksheet.Cells[i, j].Value != null)
                        {
                            if (worksheet.Cells[i, j + 1].Value != null)
                            {
                                string chord = worksheet.Cells[i, j].Value.ToString();
                                chord = chord.Replace("△", "M");
                                chord = chord.Replace('-', 'b');
                                SimilarChordsProgression.Add(chord);
                                k++;
                            }
                            else
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

                if (ChordAndSimilarity.Count != 0)
                {
                    //MessageBox.Show("prev chord : " + prevChangedChords);
                    ChordAndSimilarity.Remove(ChordAndSimilarity.Aggregate((x, y) => x.Value > y.Value ? x : y).Key);
                    var ChordAndSimilarityDescend = ChordAndSimilarity.OrderByDescending(x => x.Value);//類似度を降順でソート

                    foreach (var item in ChordAndSimilarityDescend)
                    {
                        //MessageBox.Show("item : " + item.ToString());
                        if (automode == 0 && item.Value >= 0) //filter
                        {
                            form2.checkedListBox1.Items.Add(item);
                        }

                        else if (automode == 1 && item.Value >= 0 && prevChangedChords.Contains(item.ToString()) != true)
                        {
                            chordSet();
                            string changeChords = "";
                            List<string> changeChordsList = new List<string>();

                            //MessageBox.Show("item : " + item.ToString());
                            changeChords = item.ToString();
                            prevChangedChords.Add(changeChords);
                            changeChords = changeChords.Replace("[", "");
                            changeChords = changeChords.Replace("]", "");
                            changeChords = changeChords.Substring(0, changeChords.IndexOf(","));
                            string[] splitChords = changeChords.Split('-');
                            for (int i = 0; i < splitChords.Length; i++)
                            {

                                changeChordsList.Add(splitChords[i]);
                            }

                            chordSoundList2.Clear();
                            chordSoundList2 = changeChordsList.ToList();
                            chordList.Clear();

                            richTextBox1.Clear();
                            foreach (string chord in chordSoundList2)
                            {
                                //MessageBox.Show("chord : " + chord);
                                richTextBox1.Text = richTextBox1.Text.TrimEnd();
                                richTextBox1.AppendText(chord + "- ");
                                chordList.Push(chord);
                            }


                            partCheckingList.Clear();
                            partCheckingList = chordSoundList2.ToList();
                            List<string> copy = new List<string>(partCheckingList);

                            //for (int i = 0; i < partCheckingList.Count; i++)
                            //MessageBox.Show("partcheking list : " + partCheckingList[i]);
                            copy.RemoveRange(copy.Count - 2, (copy.Count) - (copy.Count - 2));
                            flag = true;
                            partCheckAlgorithm(copy);
                            break;
                        }

                    }
                    if (automode == 0)
                        form2.Show();
                    //else if (automode == 1)
                    // partCheckAlgorithm(copy);

                }
                else
                {
                    MessageBox.Show("類似コード進行がありません");
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string soundFileName = _button1.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button1.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string soundFileName = _button2.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button2.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string soundFileName = _button3.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string soundFileName = _button4.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button4.Text);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string soundFileName = _button5.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button5.Text);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string soundFileName = _button6.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button6.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button7.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button7.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string soundFileName = _button13.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button13.Text);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            string soundFileName = _button8.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button8.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string soundFileName = _button14.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button14.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string soundFileName = _button15.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button15.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string soundFileName = _button16.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button16.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string soundFileName = _button17.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button17.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string soundFileName = _button18.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button18.Text);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string soundFileName = _button31.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button31.Text);
        }

        private void _Bdim7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button19.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button19.Text);
        }

        private void _Cm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button25.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button25.Text);
        }

        private void _D_Click(object sender, EventArgs e)
        {
            string soundFileName = _button26.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button26.Text);
        }

        private void _E_Click(object sender, EventArgs e)
        {
            string soundFileName = _button27.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button27.Text);
        }

        private void _Fm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button28.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button28.Text);
        }

        private void _Gm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button29.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button29.Text);
        }

        private void _A_Click(object sender, EventArgs e)
        {
            string soundFileName = _button30.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button30.Text);
        }


        private void _Ds_Click(object sender, EventArgs e)
        {
            string soundFileName = _button9.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button9.Text);
        }

        private void _Fs_Click(object sender, EventArgs e)
        {
            string soundFileName = _button10.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button10.Text);
        }

        private void _Gs_Click(object sender, EventArgs e)
        {
            string soundFileName = _button11.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button11.Text);
        }

        private void _As_Click(object sender, EventArgs e)
        {
            string soundFileName = _button12.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button12.Text);
        }

        private void _Cs7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button20.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button20.Text);
        }

        private void _Ds7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button21.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button21.Text);
        }

        private void _Fs7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button22.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button22.Text);
        }

        private void _Gs7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button23.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button23.Text);
        }

        private void _As7_Click(object sender, EventArgs e)
        {
            string soundFileName = _button24.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button24.Text);
        }

        private void _Csm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button32.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button32.Text);
        }

        private void _Dsm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button33.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button33.Text);
        }

        private void _Fsm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button34.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button34.Text);
        }

        private void _Gsm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button35.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button35.Text);
        }

        private void _Asm_Click(object sender, EventArgs e)
        {
            string soundFileName = _button36.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button36.Text);
        }

        private void _button37_Click(object sender, EventArgs e)
        {
            string soundFileName = _button37.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button37.Text);
        }

        private void _button38_Click(object sender, EventArgs e)
        {
            string soundFileName = _button38.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button38.Text);
        }

        private void _button39_Click(object sender, EventArgs e)
        {
            string soundFileName = _button39.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button39.Text);
        }

        private void _button40_Click(object sender, EventArgs e)
        {
            string soundFileName = _button40.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button40.Text);
        }

        private void _button41_Click(object sender, EventArgs e)
        {
            string soundFileName = _button41.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button41.Text);
        }

        private void _button42_Click(object sender, EventArgs e)
        {
            string soundFileName = _button42.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button42.Text);
        }

        private void _button43_Click(object sender, EventArgs e)
        {
            string soundFileName = _button43.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button43.Text);
        }

        private void _button44_Click(object sender, EventArgs e)
        {
            string soundFileName = _button44.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button44.Text);
        }

        private void _button45_Click(object sender, EventArgs e)
        {
            string soundFileName = _button45.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button45.Text);
        }

        private void _button46_Click(object sender, EventArgs e)
        {
            string soundFileName = _button46.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button46.Text);
        }

        private void _button47_Click(object sender, EventArgs e)
        {
            string soundFileName = _button47.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button47.Text);
        }

        private void _button48_Click(object sender, EventArgs e)
        {
            string soundFileName = _button48.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button48.Text);
        }

        private void _button49_Click(object sender, EventArgs e)
        {
            string soundFileName = _button49.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button49.Text);
        }

        private void _button50_Click(object sender, EventArgs e)
        {
            string soundFileName = _button50.Text;
            soundFileName = soundFileName.Replace("△", "Ma");
            soundFileName = soundFileName.Replace("-", "b");
            soundFileName = soundFileName.Replace("/", "_");
            SoundPlayer chordSound = new SoundPlayer(Application.StartupPath + @"\sound\" + soundFileName + ".wav");
            chordSound.Play();
            chordButton(_button50.Text);
        }

        private void progressThread()
        {
            for (int i = 0; i < 100000; i++)
            {
                progressBar1.Invoke(new ProgvarCall(ProgValueSetting), new object[] { i });
                //Thread.Sleep(10);
                MessageBox.Show("thread " + i.ToString());


            }
        }

        private void threadstart()
        {


            Thread thread1 = new Thread(new ThreadStart(progressThread));
            thread1.Start();
            thread1.Join();
            //MessageBox.Show("thread start ");
        }


        delegate void ProgvarCall(int var);


        private void ProgValueSetting(int var)
        {
            progressBar1.Value = var;
        }

        private void random_Click_1(object sender, EventArgs e) //ランダムでコード進行を作る(規則なし)
        {
            RandomRecommend random1 = new RandomRecommend();

            random1.setter(ref chordList, ref chordSoundList2, ref richTextBox1, this);
            random1.recommend1(buttonList);

        }

        private void random2_Click(object sender, EventArgs e)
        {
            RandomRecommend random2 = new RandomRecommend();

            random2.setter(ref chordList, ref chordSoundList2, ref richTextBox1, this);
            random2.recommend2(buttonList);

            /*
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 130000;
            progressBar1.Step = 3;
            */
            //threadstart();

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            List<string> copyList = new List<string>(chordSoundList2);
            searchSimilarChords(copyList);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)　//類似コード進行推薦を自動で行うかそうでないかをチェック
        {
            if (checkBox1.Checked == false)
                automode = 0;　//手動で行う
            else
                automode = 1;　//自動で行う
        }


        private void ToIntro_Click(object sender, EventArgs e) //Introパートのコード進行データから推薦
        {
            part = "Intro";
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();


            for (int j = 0; j < 8; j++)
            {

                for (int i = 0; i < buttonList.Count; i++)
                {
                    //MessageBox.Show(i.ToString());

                    progressBar1.PerformStep();
                    Thread.Sleep(10);
                    if (buttonList[i].Enabled == true)
                    {

                        randombuttonList.Add(buttonList[i]);


                    }


                }
                string name = "";
                //MessageBox.Show(randombuttonList.Count.ToString());
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Text;
                chordButton(name);
                randombuttonList.Clear();
            }
            intro.Text = richTextBox1.Text;
            richTextBox1.Clear();
        }


        private void ToAmelody_Click(object sender, EventArgs e)　//Aメロパートのコード進行データから推薦
        {

            part = "A";
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();


            for (int j = 0; j < 8; j++)
            {

                for (int i = 0; i < buttonList.Count; i++)
                {
                    //MessageBox.Show(i.ToString());

                    progressBar1.PerformStep();
                    Thread.Sleep(10);
                    if (buttonList[i].Enabled == true)
                    {
                        randombuttonList.Add(buttonList[i]);

                    }

                }

                string name = "";
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Text;
                chordButton(name);
                randombuttonList.Clear();
            }


            Amelody.Text = richTextBox1.Text;
            richTextBox1.Clear();
        }

        private void ToBmelody_Click(object sender, EventArgs e)　//Bメロパートのコード進行データから推薦
        {

            part = "B";
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();


            for (int j = 0; j < 8; j++)
            {

                for (int i = 0; i < buttonList.Count; i++)
                {
                    //MessageBox.Show(i.ToString());

                    progressBar1.PerformStep();
                    Thread.Sleep(10);
                    if (buttonList[i].Enabled == true)
                    {
                        randombuttonList.Add(buttonList[i]);

                    }

                }
                string name = "";
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Text;
                chordButton(name);
                randombuttonList.Clear();
            }


            Bmelody.Text = richTextBox1.Text;
            richTextBox1.Clear();
        }

        private void ToSabi_Click(object sender, EventArgs e) //サビパートのコード進行データから推薦
        {
            part = "sabi";
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();


            for (int j = 0; j < 8; j++)
            {

                for (int i = 0; i < buttonList.Count; i++)
                {
                    //MessageBox.Show(i.ToString());

                    progressBar1.PerformStep();
                    Thread.Sleep(10);
                    if (buttonList[i].Enabled == true)
                    {
                        randombuttonList.Add(buttonList[i]);

                    }

                }
                string name = "";
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Text;
                chordButton(name);
                randombuttonList.Clear();
            }
            sabi.Text = richTextBox1.Text;
            richTextBox1.Clear();
        }

        private void melodyInputButton_Click(object sender, EventArgs e) //メロディー認識によるコード推薦(実装予定)
        {
            /*
            Dictionary<string, int> recommendChordByMelody;
            MelodyToChordForCsharp.Logic logic = new MelodyToChordForCsharp.Logic();
            logic.Setting();
            recommendChordByMelody = logic.main();
            */
            

        }
    }
}

