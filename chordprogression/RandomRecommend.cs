using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace chordprogression 
{
    class RandomRecommend
    {
        private Stack<string> chordList;
        private List<string> chordSoundList2;
        private RichTextBox richTextBox1;
        private Form1 form;

        public void setter(ref Stack<string> _chordList, ref List<string> _chordSoundList2, ref RichTextBox _richTextBox1, Form1 _form)
        {
            chordList = _chordList;
            chordSoundList2 = _chordSoundList2;
            richTextBox1 = _richTextBox1;
            form = _form;
        }

        public void recommend1(List<Control> buttonList)
        {
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
        }

        public void recommend2(List<Control> buttonList)
        {
            Random random = new Random();
            List<Control> randombuttonList = new List<Control>();


            for (int j = 0; j < 8; j++)
            {

                for (int i = 0; i < buttonList.Count; i++)
                {
                    //MessageBox.Show(i.ToString());

                    //progressBar1.PerformStep();
                    //Thread.Sleep(10);
                    if (buttonList[i].Enabled == true)
                    {
                        randombuttonList.Add(buttonList[i]);


                    }


                }

                string name = "";
                int number = random.Next(0, randombuttonList.Count);
                name = randombuttonList[number].Text;
                /*
                name = name.Replace("_", "");
                name = name.Replace("s", "#");
                name = name.Replace("#u#4", "sus4");
                name = name.Replace("Ma", "M"); */
                form.chordButton(name);
                randombuttonList.Clear();
            }
        }
    }
}
