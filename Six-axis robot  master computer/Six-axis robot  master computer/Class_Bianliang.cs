using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Six_axis_robot__master_computer
{
    public class Bianliang  //用静态变量(必须使用静态变量，必须！！！)作全局变量，看看与上面的区别，上面的方法更灵活多变
    {
        //public static string ss; //静态变量用 “类名.变量名“ 的方式访问，注意中间的点。不用采用对类进行实例引用的方式访问
        public static Double X_Sudu = 0;
        public static Double Y_Sudu = 0;
        public static Double Z_Sudu = 0;
        public static Double E0_Sudu = 0;
        public static Double E1_Sudu = 0;
    }
}
