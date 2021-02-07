using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
namespace chordprogression
{
    class Verific
    {
        string _filePath;
        string _scale;
        int _partCheck;
        public int verific()
        {
            var xlsxFile = File.OpenRead(_filePath);
            using (var package = new ExcelPackage(xlsxFile))
            {
                if (_scale == "C Major")
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
                        
                        StreamWriter configText = new StreamWriter("./config.txt", true);
                        configText.WriteLine("partCheck = 1");
                        configText.Dispose();
                        return _partCheck = 1;
                    }
                    else
                    {
                        MessageBox.Show("単純推薦アルゴリズムを適用します。");
                        
                        StreamWriter configText = new StreamWriter("./config.txt", true);
                        configText.WriteLine("partCheck = 0");
                        configText.Dispose();
                        return _partCheck = 0;
                    }
                }
            }
            return -1;
        }

        public Verific(string _filePathInput, string _scaleInput)
        {
            _filePath = _filePathInput;
            _scale = _scaleInput;        
        }
    }
}
