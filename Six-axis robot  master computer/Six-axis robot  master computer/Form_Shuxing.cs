using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Six_axis_robot__master_computer
{
    public partial class Form_Shuxing : Form
    {
        public Form_Shuxing()
        {
            InitializeComponent();
        }

        //X
        private void button12_Click(object sender, EventArgs e)
        {
            kuachuangkong_XYZ_Zhilin_Weizhizhijiegaibian("X", Convert.ToDouble(label_X_Sudu.Text), Convert.ToDouble(textBox_X_Sheding.Text));
        }
        //Y
        private void button13_Click(object sender, EventArgs e)
        {
            kuachuangkong_XYZ_Zhilin_Weizhizhijiegaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), Convert.ToDouble(textBox_Y_Sheding.Text));
        }
        //z
        private void button14_Click(object sender, EventArgs e)
        {
            kuachuangkong_XYZ_Zhilin_Weizhizhijiegaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), Convert.ToDouble(textBox_Z_Sheding.Text));
        }
        //E0
        private void button15_Click(object sender, EventArgs e)
        {
            kuachuangkong_T0T1_Zhilin_Weizhizhijiegaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), Convert.ToDouble(textBox_E0_Sheding.Text));
        }
        //E1
        private void button16_Click(object sender, EventArgs e)
        {
            kuachuangkong_T0T1_Zhilin_Weizhizhijiegaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), Convert.ToDouble(textBox_E1_Sheding.Text));
        }

        //跨窗口方法的调用
        public void kuachuangkong_XYZ_Zhilin_Weizhizhijiegaibian(string XYZ, Double Sudu, Double Zhi)
        {
            //新建一个窗体,并将主窗体的引用赋给新窗体
            Form f = this.Owner;
            //强制将新窗体转换为主窗体,这一步不能少，不然会编译不通过。
            //强制转换后，你就可以调用其中的public方法了。
            ((Form_Main)f).Zhilin_Weizhizhijiegaibian(XYZ, Sudu, Zhi);
        }
        public void kuachuangkong_T0T1_Zhilin_Weizhizhijiegaibian(string T0T1, Double Sudu, Double Zhi)
        {
            //新建一个窗体,并将主窗体的引用赋给新窗体
            Form f = this.Owner;
            //强制将新窗体转换为主窗体,这一步不能少，不然会编译不通过。
            //强制转换后，你就可以调用其中的public方法了。
            ((Form_Main)f).Zhilin_T0T1_Weizhizhijiegaibian(T0T1, Sudu, Zhi);
        }




        //全部设定
        private void button18_Click(object sender, EventArgs e)
        {
            button12.PerformClick();
            button13.PerformClick();
            button14.PerformClick();
            button15.PerformClick();
            button16.PerformClick();
        }
        //全部设置为零点
        private void button17_Click(object sender, EventArgs e)
        {
            textBox_X_Sheding.Text = "0";
            textBox_Y_Sheding.Text = "0";
            textBox_Z_Sheding.Text = "0";
            textBox_E0_Sheding.Text = "0";
            textBox_E1_Sheding.Text = "0";
        }
        //X+1
        private void Button2_Click_1(object sender, EventArgs e)
        {
            Jia1(textBox_X_Sheding);
        }

        //X-1
        private void Button3_Click_1(object sender, EventArgs e)
        {
            Jian1(textBox_X_Sheding);
        }
        //Y+1
        private void Button5_Click_1(object sender, EventArgs e)
        {
            Jia1(textBox_Y_Sheding);
        }
        //Y-1
        private void Button4_Click_1(object sender, EventArgs e)
        {
            Jian1(textBox_Y_Sheding);
        }
        //Z+1
        private void Button7_Click_1(object sender, EventArgs e)
        {
            Jia1(textBox_Z_Sheding);
        }
        //Z-1
        private void Button6_Click_1(object sender, EventArgs e)
        {
            Jian1(textBox_Z_Sheding);
        }
        //E0+1
        private void Button9_Click_1(object sender, EventArgs e)
        {
            Jia1(textBox_E0_Sheding);
        }
        //E0-1
        private void Button8_Click_1(object sender, EventArgs e)
        {
            Jian1(textBox_E0_Sheding);
        }
        //E1+1
        private void Button11_Click_1(object sender, EventArgs e)
        {
            Jia1(textBox_E1_Sheding);
        }
        //E1-1
        private void Button10_Click_1(object sender, EventArgs e)
        {
            Jian1(textBox_E1_Sheding);
        }
        //位置设定中加1
        private void Jia1(TextBox textBox)
        {
            string text = textBox.Text;
            Double num = Convert.ToDouble(text);
            num += 1;
            textBox.Text = num.ToString();
        }
        //位置设定中减1
        private void Jian1(TextBox textBox)
        {
            string text = textBox.Text;
            Double num = Convert.ToDouble(text);
            num -= 1;
            textBox.Text = num.ToString();
        }



        //修改X移动速度
        private void button_Sudu_X_Click(object sender, EventArgs e)
        {
            label_X_Sudu.Text = textBox_Xuigaisudu_X.Text;
        }
        //修改Y移动速度
        private void button_Sudu_Y_Click(object sender, EventArgs e)
        {
            label_Y_Sudu.Text = textBox_Xuigaisudu_Y.Text;
        }
        //修改Z移动速度
        private void button_Sudu_Z_Click(object sender, EventArgs e)
        {
            label_Z_Sudu.Text = textBox_Xuigaisudu_Z.Text;
        }
        //修改E0移动速度
        private void button_Sudu_E0_Click(object sender, EventArgs e)
        {
            label_E0_Sudu.Text = textBox_Xuigaisudu_E0.Text;
        }
        //修改E1移动速度
        private void button_Sudu_E1_Click(object sender, EventArgs e)
        {
            label_E1_Sudu.Text = textBox_Xuigaisudu_E1.Text;
        }

        private void Form_Shuxing_Load(object sender, EventArgs e)
        {
            label_X_Sudu.Text = Convert.ToString(Bianliang.X_Sudu);
            label_Y_Sudu.Text = Convert.ToString(Bianliang.Y_Sudu);
            label_Z_Sudu.Text = Convert.ToString(Bianliang.Z_Sudu);
            label_E0_Sudu.Text = Convert.ToString(Bianliang.E0_Sudu);
            label_E1_Sudu.Text = Convert.ToString(Bianliang.E1_Sudu);
        }

        private void Button19_Click(object sender, EventArgs e)
        {

        }

        private void Form_Shuxing_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bianliang.X_Sudu = Convert.ToDouble(label_X_Sudu.Text);
            Bianliang.Y_Sudu = Convert.ToDouble(label_Y_Sudu.Text);
            Bianliang.Z_Sudu = Convert.ToDouble(label_Z_Sudu.Text);
            Bianliang.E0_Sudu = Convert.ToDouble(label_E0_Sudu.Text);
            Bianliang.E1_Sudu = Convert.ToDouble(label_E1_Sudu.Text);

        }
    }



}
