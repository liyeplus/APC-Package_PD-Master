using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC
{
    public class WriteTxt
    {
        public static void WriteToTxt(string T, string filepath, string filename)
        {
            string Temtxt = filename; //文件名称
            string path = filepath;
            string txtpath = path + "/" + Temtxt;//文件存放路径
            DirectoryInfo directoryInfo = new DirectoryInfo(filepath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            if (!File.Exists(txtpath))
            {
                File.Create(txtpath).Close();
            }
            StreamWriter sw = new StreamWriter(txtpath, true, System.Text.Encoding.Default);
            sw.WriteLine(DateTime.Now + "   " + T);
            sw.Flush();
            sw.Close();
        }
    }
}
