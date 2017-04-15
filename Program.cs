using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FlightConsoleApplication
{
    class Program
    {
        List<Section> list = new List<Section>();

        void run()
        {
            DirectoryInfo directoryinfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (FileInfo fileinfo in directoryinfo.GetFiles())//遍历当前文件夹下的所有csv文件
            {
                if (fileinfo.Extension != ".csv")
                    continue;
                Section section = new Section();//新建一个模块（总共三个）
                section.mode = fileinfo.Name.Replace(fileinfo.Extension,"");
                if (fileinfo.Name == "经济巡航.csv")
                    section.hasCI = true;

                FileStream thisFile = new FileStream(fileinfo.FullName, FileMode.Open);
                StreamReader sr = new StreamReader(thisFile);
                String oneline;

                int CIhere = 0 ;
                while ((oneline = sr.ReadLine()) != null) //读入每一模块的每一行
                {
                    Item item = new Item();
                    string[] words = oneline.Split(',');

                    try
                    {
                        item.ALT = int.Parse(words[0]);
                        item.ISA = int.Parse(words[1]);
                        item.WEIGHT = int.Parse(words[2]);
                        item.MACH = double.Parse(words[3]);
                        item.FFENG = double.Parse(words[4]);
                        item.TAS = int.Parse(words[5]);
                        if (section.hasCI)
                            item.CI = CIhere;
                        section.itemList.Add(item);
                    }
                    catch(FormatException 剖析错误)
                    {
                        //有一个例外，就是碰到了C.I.
                        if (words.Length == 6 && words[1] == "(C.I.")
                            CIhere = int.Parse(words[2]);
                    }
                }
                list.Add(section);
            }
        }

        void print()
        {
            foreach(Section sec in list)
            {
                Console.WriteLine("巡航模式：{0}",sec.mode);
                switch(sec.hasCI)
                {
                    case false:
                        Console.WriteLine("{0} {1} {2} {3} {4} {5}", "高度Hp(米)：", "温度偏差delta T(°C)：", "飞机质量m(千克)：", "飞行马赫数Ma：", "燃油流量wf(KG/h)：", "真空速Vti(KM/h)：");
                        foreach (Item item in sec.itemList)
                        {

                            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", item.ALT, item.ISA, item.WEIGHT, item.MACH, item.FFENG, item.getSpeed());
                        }
                        break;
                    case true:
                        Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "C.I.","高度Hp(米)：", "温度偏差delta T(°C)：", "飞机质量m(千克)：", "飞行马赫数Ma：", "燃油流量wf(KG/h)：", "真空速Vti(KM/h)：");
                        foreach (Item item in sec.itemList)
                        {

                            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", item.CI, item.ALT, item.ISA, item.WEIGHT, item.MACH, item.FFENG, item.getSpeed());
                        }
                        break;
                }
                
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.run();
            program.print();
        }
    }
}
