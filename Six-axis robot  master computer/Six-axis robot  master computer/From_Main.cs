using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Six_axis_robot__master_computer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button70_Click(object sender, EventArgs e)
        {
            textBox_Daochu.Clear();//导出框清空
        }

        private void button73_Click(object sender, EventArgs e)
        {
            textBox_Daoru.Clear();//导入框清空
        }

        //导出控制文件
        private void button71_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            save.Filter = "输出.G228文件 (*.G228)|*.G228";
            save.FileName = "输出_" + DateTime.Now.ToString("yyyyMMddHHmmss");//年月日时分秒
            if (save.ShowDialog() == DialogResult.OK && save.FileName != "")
            {
                var sw = new StreamWriter(save.FileName);
                for (var i = 0; i < textBox_Daochu.Lines.Length; i++)
                {
                    sw.WriteLine(textBox_Daochu.Lines.GetValue(i).ToString());
                }
                sw.Close();
            }
            MessageBox.Show("控制文件保存成功");
        }

        //导入控制文件
        private void button72_Click(object sender, EventArgs e)
        {
            string file = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择.G228文件";
            dialog.Filter = ".G228文件(*.G228*)|*.G228*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
            }
            Read(file);
            
        }

        //导入控制文件中Read的调用
        public void Read(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    textBox_Daoru.AppendText(line);
                    textBox_Daoru.AppendText(Environment.NewLine);
                    MessageBox.Show("控制文件导入成功");
                }
            }
            catch
            {
                MessageBox.Show("取消控制文件导入");
            }
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
        //X+1
        private void button2_Click(object sender, EventArgs e)
        {
            Jia1(textBox_X_Sheding);
        }
        //X-1
        private void button3_Click(object sender, EventArgs e)
        {
            Jian1(textBox_X_Sheding);
        }
        //Y+1
        private void button5_Click(object sender, EventArgs e)
        {
            Jia1(textBox_Y_Sheding);
        }
        //Y-1
        private void button4_Click(object sender, EventArgs e)
        {
            Jian1(textBox_Y_Sheding);
        }
        //Z+1
        private void button7_Click(object sender, EventArgs e)
        {
            Jia1(textBox_Z_Sheding);
        }
        //Z-1
        private void button6_Click(object sender, EventArgs e)
        {
            Jian1(textBox_Z_Sheding);
        }
        //E0+1
        private void button9_Click(object sender, EventArgs e)
        {
            Jia1(textBox_E0_Sheding);
        }
        //E0-1
        private void button8_Click(object sender, EventArgs e)
        {
            Jian1(textBox_E0_Sheding);
        }
        //E1+1
        private void button11_Click(object sender, EventArgs e)
        {
            Jia1(textBox_E1_Sheding);
        }
        //E1-1
        private void button10_Click(object sender, EventArgs e)
        {
            Jian1(textBox_E1_Sheding);
        }
        //扫描控制串口
        private void button20_Click(object sender, EventArgs e)
        {
            Saomiao(serialPort1, comboBox1);
        }
        //扫描串口
        private void Saomiao(SerialPort MyPort, ComboBox MyBox)
        {
            string[] MyString = new string[20];
            string Buffer;
            MyBox.Items.Clear();
            for (int i = 1; i < 20; i++)
            {
                try
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();
                    MyString[i - 1] = Buffer;
                    MyBox.Items.Add(Buffer);
                    MyPort.Close();
                }
                catch
                {

                }
            }
            MyBox.Text = MyString[0];
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                }
                catch { }
                button21.Text = "打开串口";
                button20.Enabled = true;

            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button21.Text = "关闭串口";
                    button20.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("串口打开失败", "错误");
                }
            }
        }
    }
}
