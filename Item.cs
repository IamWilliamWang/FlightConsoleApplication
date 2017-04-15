using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConsoleApplication
{
    class Item
    {
        public int ALT { get; set; }//高度Hp，单位m
        public int ISA { get; set; }//温度偏差delta T,单位。C
        public int WEIGHT { get; set; }//飞机质量m，单位KG
        public double MACH { get; set; }//飞行马赫数Ma，无单位
        public double FFENG { get; set; }//燃油流量wf，单位KG/h
        public int TAS {
            get { return tas; }
            set { tas = value; speed = (double)value * 1.852; }
                 }//真空速Vti，单位knots节，提取后化为KM/h,1knots=1.852KM/h
        private int tas;
        private double speed;
        public double getSpeed()
        {
            return speed;
        }
        public int CI;
    }
}
