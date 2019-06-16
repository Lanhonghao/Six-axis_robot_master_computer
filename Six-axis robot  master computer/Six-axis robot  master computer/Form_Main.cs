using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Six_axis_robot__master_computer
{
    public partial class Form_Main : Form
    {
        /////////////////////////////////////////////////////
        Double X_pianyi = 0;
        Double Y_pianyi = 0;
        Double Z_pianyi = 0;
        Double E0_pianyi = 0;
        Double E1_pianyi = 0;
        Double Jia_pianyi = 0;
        Double X_shangci= 0;
        Double Y_shangci = 0;
        Double Z_shangci = 0;
        Double E0_shangci = 0;
        Double E1_shangci = 0;
        Double Tingzhi = 0;

        int Xianwei_X_max = 0;  //X最高软限位
        int Xianwei_Y_max = 0;  //Y最高软限位
        int Xianwei_Z_max = 0;  //Z最高软限位
        int Xianwei_E0_max = 0;  //E0最高软限位
        int Xianwei_E1_max = 0;  //E1最高软限位
        int Xianwei_X_min = 0;  //X最低软限位
        int Xianwei_Y_min = 0;  //Y最低软限位
        int Xianwei_Z_min = 0;  //Z最低软限位
        int Xianwei_E0_min = 0;  //E0最低软限位
        int Xianwei_E1_min = 0;  //E1最低软限位
        /////////////////////////////////////////////////////


        public Form_Main()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

            chuankou_chushihua();
            backgroundWorker1.RunWorkerAsync();


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
            Double X_jieshu = -Convert.ToDouble(label_Weizhi_X.Text);
            Double Y_jieshu = -Convert.ToDouble(label_Weizhi_Y.Text);
            Double Z_jieshu = -Convert.ToDouble(label_Weizhi_Z.Text);
            this.textBox_Daochu.AppendText(";[G228#" + P + "]");
            string Weizhi = "G90\r\n" + "G1" + " " + "X" + X_jieshu + " " + "Y" + Y_jieshu + " " + "Z" + Z_jieshu + " " + "F" + "1000";
            textBox_Daochu.AppendText(Weizhi);
            textBox_Daochu.AppendText(Environment.NewLine);



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
            MessageBox.Show("控制文件导入成功");
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
            //Saomiao(serialPort1, comboBox1);

            chuankou_chushihua();
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
                label_CK_Xinxi.Text = "COM串口已关闭";

            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    //注册事件。
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(this.serialPort1_DataReceived);

                    button21.Text = "关闭串口";
                    button20.Enabled = false;
                    label_CK_Xinxi.Text = comboBox1.Text + "串口已打开";
                }
                catch
                {
                    MessageBox.Show("串口打开失败", "错误");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                //serialPort1.WriteLine(textBox_Send.Text);    //写入数据，此数据没经过实时位置反馈，需走输出程序检测
                serialPort1_shuchu(textBox_Send.Text);

                this.textBox_zhukong.Text += textBox_Send.Text + "\r\n";
            }
        }
        //串口初始化
        private void chuankou_chushihua()
        {
            //清空comboBox1下的所有可以选择数据项
            comboBox1.Items.Clear();
            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口项目
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {//获取有多少个COM口
                //System.Diagnostics.Debug.WriteLine(s);
                comboBox1.Items.Add(s);
            }

            //串口设置默认选择项
            comboBox1.SelectedIndex = 1;         //note：获得COM9口，但别忘修改
                                                 //cbBaudRate.SelectedIndex = 5;
                                                 // cbDataBits.SelectedIndex = 3;
                                                 // cbStop.SelectedIndex = 0;
                                                 //  cbParity.SelectedIndex = 0;


            Control.CheckForIllegalCrossThreadCalls = false;    //这个类中我们不检查跨线程的调用是否合法(因为.net 2.0以后加强了安全机制,，不允许在winform中直接跨线程访问控件的属性)
            //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            //sp1.ReceivedBytesThreshold = 1;

            //准备就绪              
            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
            //设置数据读取超时为1秒
            serialPort1.ReadTimeout = 1000;

            serialPort1.Close();

        }

        //后台调用串口
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
             char[] c;
             while (true)
             {
                 try
                 {
                    if (serialPort1.IsOpen)
                     {

                         c = new char[serialPort1.BytesToRead];
                         serialPort1.Read(c, 0, c.Length);
                         if (c.Length > 0)
                         {
                            SetText(new string(c));                          
                         }
                        string data = serialPort1.ReadExisting();
                        if (data != string.Empty)
                        {
                             //this.txtb_receive.Text = data;这种方法因为线程不同步，只能接收一次数据,连续发送时程序崩溃
                             SetText(data);
                        }
                          }
                    }
                catch (Exception) { }
            }*/

        }
        /*
        //打印串口数据
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            try
            {
                if (this.textBox_zhukong.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.textBox_zhukong.AppendText(text);
                    textBox_zhukong.AppendText(Environment.NewLine);
                    textBox_zhukong.SelectionStart = textBox_zhukong.Text.Length; //设定光标位置
                    textBox_zhukong.ScrollToCaret(); //滚动到光标处 
                }
            }
            catch (Exception)
            {
            }
        }*/

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //接收数据事件
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                string data = serialPort1.ReadExisting();
                if (data != string.Empty)
                {
                    //this.txtb_receive.Text = data;这种方法因为线程不同步，只能接收一次数据,连续发送时程序崩溃
                    SetData(data);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }


        delegate void SetTextCallback(string text);

        string serialPort1_shuchu_jieshou_text =" ";

        private void SetData(string text)
        {
            if (this.textBox_zhukong.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetData);
                this.Invoke(d, new object[] { text + Environment.NewLine });
            }
            else
            {
                //this.txtb_receive.Text += text+Environment.NewLine;//这种方法会多出空行
                this.textBox_zhukong.AppendText(text);
                serialPort1_shuchu_jieshou_text = text;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////



        private void textBox_Daochu_TextChanged(object sender, EventArgs e)
        {

        }

        //位置改变指令0.1,1,10,100
        private void Zhilin_Weizhigaibian(string XYZ, Double Sudu, Double Zhi)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string XYZ_O = "G91\r\n" + "G0 " + XYZ + Zhi + " " + "F" + Sudu + "\r\n" + "G90\r\n" + "M114\r\n";

                //serialPort1.WriteLine(XYZ_O);    //串口写入数据     
                serialPort1_shuchu(XYZ_O);

                this.textBox_zhukong.AppendText(XYZ_O);
                if (XYZ == "X")
                {
                    Double NUM = Convert.ToDouble(label_Weizhi_X.Text);
                    NUM += Zhi;
                    label_Weizhi_X.Text = NUM.ToString();

                }
                else if (XYZ == "Y")
                {
                    Double NUM = Convert.ToDouble(label_Weizhi_Y.Text);
                    NUM += Zhi;
                    label_Weizhi_Y.Text = NUM.ToString();
                }
                else if (XYZ == "Z")
                {
                    Double NUM = Convert.ToDouble(label_Weizhi_Z.Text);
                    NUM += Zhi;
                    label_Weizhi_Z.Text = NUM.ToString();
                }
            }
        }
        //x,0.1
        private void button26_Click(object sender, EventArgs e)
        {
            /*
            string XYZ = "X";
            Double Sudu = Convert.ToDouble(label_Sudu.Text);
            Double Zhi = 0.1;
            Zhilin_Weizhigaibian(XYZ,Sudu,Zhi);*/

            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), 0.1);
        }
        //x,1 
        private void button25_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), 1);
        }
        //x,10
        private void button24_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), 10);
        }
        //x,100
        private void button23_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), 100);
        }
        //x,-0.1
        private void button27_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), -0.1);
        }
        //x,-1
        private void button28_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), -1);
        }
        //x,-10
        private void button29_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), -10);
        }
        //x,-100
        private void button30_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("X", Convert.ToDouble(label_X_Sudu.Text), -100);
        }
        //Y,0.1
        private void button37_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), 0.1);
        }
        //Y,1
        private void button38_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), 1);
        }
        //Y,10
        private void button39_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), 10);
        }
        //Y,100
        private void button40_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), 100);
        }
        //Y,-0.1
        private void button36_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), -0.1);
        }
        //Y,-1
        private void button35_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), -1);
        }
        //Y,-10
        private void button34_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), -10);
        }
        //Y,-100
        private void button33_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), -100);
        }
        //Z,0.1
        private void button46_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), 0.1);
        }
        //Z,1
        private void button47_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), 1);
        }
        //Z,10
        private void button48_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), 10);
        }
        //Z,100
        private void button49_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), 100);
        }
        //Z,-0.1
        private void button45_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), -0.1);
        }
        //Z,-1
        private void button44_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), -1);
        }
        //Z,-10
        private void button43_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), -10);
        }
        //Z,-100
        private void button42_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhigaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), -100);
        }


        //位置回家指令
        private void Zhilin_Home(string XYZ)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string XYZ_O = "G28" + " " + XYZ + "0" + "\r\n" + "M114\r\n";

                //serialPort1.WriteLine(XYZ_O);    //串口写入数据         
                serialPort1_shuchu(XYZ_O);

                this.textBox_zhukong.AppendText(XYZ_O);
            }
        }
        //X_Home
        private void button31_Click(object sender, EventArgs e)
        {
            Zhilin_Home("X");
        }
        //Y_Home
        private void button32_Click(object sender, EventArgs e)
        {
            Zhilin_Home("Y");
        }
        //Z_Home
        private void button41_Click(object sender, EventArgs e)
        {
            Zhilin_Home("Z");
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox_zhukong.Clear();//清空
        }

        //T0T1位置改变指令0.1,1,10,100
        private void Zhilin_T0T1_Weizhigaibian(string T0T1, Double Sudu, Double Zhi)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string T0T1_0 = T0T1 + "\r\n" + "G91\r\n" + "G1 " + "E" + Zhi + " " + "F" + Sudu + "\r\n" + "G90\r\n" + "M114\r\n";

                //serialPort1.WriteLine(T0T1_0);    //串口写入数据    
                serialPort1_shuchu(T0T1_0);

                this.textBox_zhukong.AppendText(T0T1_0);
                if (T0T1 == "T0")
                {
                    Double NUM = Convert.ToDouble(label_Weizhi_E0.Text);
                    NUM += Zhi;
                    label_Weizhi_E0.Text = NUM.ToString();

                }
                else if (T0T1 == "T1")
                {
                    Double NUM = Convert.ToDouble(label_Weizhi_E1.Text);
                    NUM += Zhi;
                    label_Weizhi_E1.Text = NUM.ToString();
                }
            }
        }

        //T0,1
        private void button56_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), 1);
        }
        //T0,0.1
        private void button55_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), 0.1);
        }
        //T0,-0.1
        private void button54_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), -0.1);
        }
        //T0,-1
        private void button53_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), -1);
        }
        //T1,1
        private void button65_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), 1);
        }
        //T1,0.1
        private void button64_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), 0.1);
        }
        //T1,-0.1
        private void button63_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), -0.1);
        }
        //T1,-1
        private void button62_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhigaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), -1);
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
        //夹取命令
        private void button22_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string Jia = "M280 P0 S80\r\n";

                //serialPort1.WriteLine(Jia);    //串口写入数据  
                serialPort1_shuchu(Jia);

                this.textBox_zhukong.AppendText(Jia);
                label_Jiaqu.Text = "夹取中";
                Jia_pianyi = 1;
            }
        }
        //放开命令
        private void button_Fang_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string Fang = "M280 P0 S0\r\n";

                //serialPort1.WriteLine(Fang);    //串口写入数据 
                serialPort1_shuchu(Fang);

                this.textBox_zhukong.AppendText(Fang);
                label_Jiaqu.Text = "未夹取";
                Jia_pianyi = 0;
            }
        }
        //STOP
        private void button68_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                Tingzhi = 1;
                string STOP = "M18\r\n";

                //serialPort1.WriteLine(STOP);    //串口写入数据  
                serialPort1_shuchu(STOP);

                this.textBox_zhukong.AppendText(STOP);
                label_huifu.Visible = true;

            }
        }
        //查询位置
        private void button19_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string Chaxun = "M114\r\n";

                //serialPort1.WriteLine(Chaxun);    //串口写入数据   
                serialPort1_shuchu(Chaxun);

                this.textBox_zhukong.AppendText(Chaxun);
            }
        }

        //位置直接改变指令
        private void Zhilin_Weizhizhijiegaibian(string XYZ, Double Sudu, Double Zhi)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string XYZ_O = "G91\r\n" + "G0 " + XYZ + Zhi + " " + "F" + Sudu + "\r\n" + "G90\r\n" + "M114\r\n";

                //serialPort1.WriteLine(XYZ_O);    //串口写入数据        
                serialPort1_shuchu(XYZ_O);

                this.textBox_zhukong.AppendText(XYZ_O);
                if (XYZ == "X")
                {
                    label_Weizhi_X.Text = Zhi.ToString();

                }
                else if (XYZ == "Y")
                {
                    label_Weizhi_Y.Text = Zhi.ToString();
                }
                else if (XYZ == "Z")
                {
                    label_Weizhi_Z.Text = Zhi.ToString();
                }
            }
        }
        //T0T1位置直接改变指令
        private void Zhilin_T0T1_Weizhizhijiegaibian(string T0T1, Double Sudu, Double Zhi)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                string T0T1_0 = T0T1 + "\r\n" + "G91\r\n" + "G1 " + "E" + Zhi + " " + "F" + Sudu + "\r\n" + "G90\r\n" + "M114\r\n";

                //serialPort1.WriteLine(T0T1_0);    //串口写入数据
                serialPort1_shuchu(T0T1_0);

                this.textBox_zhukong.AppendText(T0T1_0);
                if (T0T1 == "T0")
                {
                    label_Weizhi_E0.Text = Zhi.ToString();
                }
                else if (T0T1 == "T1")
                {
                    label_Weizhi_E1.Text = Zhi.ToString();
                }
            }
        }
        //x
        private void button12_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhizhijiegaibian("X", Convert.ToDouble(label_X_Sudu.Text), Convert.ToDouble(textBox_X_Sheding.Text));
        }
        //Y
        private void button13_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhizhijiegaibian("Y", Convert.ToDouble(label_Y_Sudu.Text), Convert.ToDouble(textBox_Y_Sheding.Text));
        }
        //z
        private void button14_Click(object sender, EventArgs e)
        {
            Zhilin_Weizhizhijiegaibian("Z", Convert.ToDouble(label_Z_Sudu.Text), Convert.ToDouble(textBox_Z_Sheding.Text));
        }
        //E0
        private void button15_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhizhijiegaibian("T0", Convert.ToDouble(label_E0_Sudu.Text), Convert.ToDouble(textBox_E0_Sheding.Text));
        }
        //E1
        private void button16_Click(object sender, EventArgs e)
        {
            Zhilin_T0T1_Weizhizhijiegaibian("T1", Convert.ToDouble(label_E1_Sudu.Text), Convert.ToDouble(textBox_E1_Sheding.Text));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button12.PerformClick();
            button13.PerformClick();
            button14.PerformClick();
            button15.PerformClick();
            button16.PerformClick();
        }
        //记录机械臂位置
        private void button69_Click(object sender, EventArgs e)
        {
            JiluXYZ();
            JiluE0E1();

        }
        //记录夹具状态
        private void button22_Click_1(object sender, EventArgs e)
        {
            Jilu_Jiaju();
        }

        int P = 0;
        private void JiluXYZ()
        {
            P += 1;
            X_pianyi = Convert.ToDouble(label_Weizhi_X.Text) - X_shangci;
            Y_pianyi = Convert.ToDouble(label_Weizhi_Y.Text) - Y_shangci;
            Z_pianyi = Convert.ToDouble(label_Weizhi_Z.Text) - Z_shangci;
            
            this.textBox_Daochu.AppendText(";[G228#"+P+"]");
            string Weizhi = "G91\r\n" + "G1" + " " + "X" +X_pianyi + " " + "Y" + Y_pianyi + " " + "Z" + Z_pianyi + " " + "F" + "3000";
            textBox_Daochu.AppendText(Weizhi);
            textBox_Daochu.AppendText(Environment.NewLine);

            X_shangci = Convert.ToDouble(label_Weizhi_X.Text);
            Y_shangci = Convert.ToDouble(label_Weizhi_Y.Text);
            Z_shangci = Convert.ToDouble(label_Weizhi_Z.Text);
        }

        private void JiluE0E1()
        {
            P += 1;
            E0_pianyi = Convert.ToDouble(label_Weizhi_E0.Text) - E0_shangci;
            E1_pianyi = Convert.ToDouble(label_Weizhi_E1.Text) - E1_shangci;

            this.textBox_Daochu.AppendText(";[G228#" + P + "]");
            textBox_Daochu.AppendText(Environment.NewLine);
            string Weizhi = "T0\r\n" + "G91\r\n" + "G1 " + "E" + E0_pianyi + " " + "F" + "1000";
            textBox_Daochu.AppendText(Weizhi);
            textBox_Daochu.AppendText(Environment.NewLine);
            Weizhi = "T1\r\n" + "G91\r\n" + "G1 " + "E" + E1_pianyi + " " + "F" + "1000";
            textBox_Daochu.AppendText(Weizhi);
            textBox_Daochu.AppendText(Environment.NewLine);

            E0_shangci = Convert.ToDouble(label_Weizhi_E0.Text);
            E1_shangci = Convert.ToDouble(label_Weizhi_E1.Text);
        }

        private void Jilu_Jiaju()
        {
            P += 1;
            this.textBox_Daochu.AppendText(";[G228#" + P + "]");
            textBox_Daochu.AppendText(Environment.NewLine);
            if (Jia_pianyi ==0)
            {
                textBox_Daochu.AppendText("M280 P0 S0\r\n"); 
            }
            else
            {
                textBox_Daochu.AppendText("M280 P0 S80\r\n"); 
            }
        }


        private void button23_Click_1(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) //如果没打开
            {
                MessageBox.Show("请先打开串口！", "Error");
                return;
            }
            else
            {
                Zhixing();
            }           
        }

        private void Zhixing()
        {
            
           /* foreach (string s in textBox_Daoru.Lines)
            {
 
                serialPort1.WriteLine(s);    //写入数据
                this.textBox_zhukong.Text += s + "\r\n";
                //System.Threading.Thread.Sleep(3000);
                
            }*/


            //string Content= textBox_Daoru.Text;//读取所有导入执行信息
            //string[] ContentLines = Content.Split(new string[] { "\r\n" }, StringSplitOptions.None);//不忽略空行
            //string[] ContentLines = Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);//忽略空行

            string[] str = new string[textBox_Daoru.Lines.Length];

            progressBar_fasong.Maximum = textBox_Daoru.Lines.Length;//progressBar_fasong设置最大长度值
            progressBar_fasong.Value = 0;//progressBar_fasong设置当前值
            progressBar_fasong.Step = 1;//progressBar_fasong设置没次增长多少

            for (int i = 0; i < textBox_Daoru.Lines.Length; i++)
            {

                str[i] = textBox_Daoru.Lines[i];
                serialPort1_shuchu(str[i]);
                for (int n = 0; n == 1; )
                {
                    if (serialPort1_shuchu_jieshou_text.Contains("ok"))
                    {
                        n = 1;
                    }
                }
                this.textBox_zhukong.Text += str[i] + "\r\n";
                progressBar_fasong.Value += progressBar_fasong.Step; //让进度条增加一次
            }
                


        }

        private void serialPort1_shuchu(string shuchu)  //串口1输出数据的检测以及发送，用于软限位的控制
        {
            if (Convert.ToDouble(label_Weizhi_X.Text) < Xianwei_X_max && Convert.ToDouble(label_Weizhi_Y.Text) < Xianwei_Y_max && Convert.ToDouble(label_Weizhi_Z.Text) < Xianwei_Z_max && Convert.ToDouble(label_Weizhi_E0.Text) < Xianwei_E0_max && Convert.ToDouble(label_Weizhi_E1.Text) < Xianwei_E1_max && Convert.ToDouble(label_Weizhi_X.Text) > Xianwei_X_min && Convert.ToDouble(label_Weizhi_Y.Text) > Xianwei_Y_min && Convert.ToDouble(label_Weizhi_Z.Text) > Xianwei_Z_min && Convert.ToDouble(label_Weizhi_E0.Text) > Xianwei_E0_min && Convert.ToDouble(label_Weizhi_E1.Text) > Xianwei_E1_min)
            {
                serialPort1.WriteLine(shuchu);
            }
            else
            {
                MessageBox.Show("超出限位！", "Error");
            }


        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }


}
