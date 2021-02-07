using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;



namespace chordprogression
{
    public partial class Form2 : Form
    {

        Form1 f1;
        public Form2(Form1 form)
        {
            InitializeComponent();
            f1 = form;
        }

       
        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new Form1(this);
            this.f1.chordSet();
            string changeChords = "";
            List<string> changeChordsList = new List<string>();
            changeChords = checkedListBox1.SelectedItem.ToString();
            changeChords = changeChords.Replace("[", "");
            changeChords = changeChords.Replace("]", "");
            changeChords = changeChords.Substring(0, changeChords.IndexOf(","));
            string[] splitChords = changeChords.Split('-');
            for (int i = 0; i < splitChords.Length; i++) 
            {
                
                changeChordsList.Add( splitChords[i]);
            }
           
           
            this.f1.chordSoundList2.Clear();
            this.f1.chordSoundList2 = changeChordsList;
            this.f1.chordList.Clear();
            

            this.f1.richTextBox1.Clear();
            foreach (string chord in this.f1.chordSoundList2)
            {
                this.f1.richTextBox1.Text = this.f1.richTextBox1.Text.TrimEnd();
                this.f1.richTextBox1.AppendText(chord + "- ");
                this.f1.chordList.Push(chord);
                
            }

            
            this.f1.partCheckingList.Clear();
            this.f1.partCheckingList = this.f1.chordSoundList2.ToList();
            this.f1.partCheckingList.RemoveRange(this.f1.partCheckingList.Count - 2, (this.f1.partCheckingList.Count) - (this.f1.partCheckingList.Count - 2));
            this.f1.flag = true;
            this.f1.partCheckAlgorithm(this.f1.partCheckingList);
            this.Close();
        }
    }
}
