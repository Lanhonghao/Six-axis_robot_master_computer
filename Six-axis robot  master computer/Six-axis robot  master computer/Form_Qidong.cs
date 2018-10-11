using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Six_axis_robot__master_computer
{
    public partial class Form_Qidong : Form
    {
        public Form_Qidong()
        {
            InitializeComponent();
        }

        private void Form_Qidong_Load(object sender, EventArgs e)
        {

        }

        //关闭自身 
        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }
        /// <summary> 
        /// 加载并显示主窗体 
        /// </summary> 
        /// <param name="form">主窗体</param> 
        public static void LoadAndRun(Form form)
        {
            //订阅主窗体的句柄创建事件 
            form.HandleCreated += delegate
            {
                //启动新线程来显示Qidong窗体 
                new Thread(new ThreadStart(delegate
                {
                    Form_Qidong Qidong = new Form_Qidong();
                    //订阅主窗体的Qidong事件 
                    form.Shown += delegate
                    {
                        //通知Qidong窗体关闭自身 
                        Qidong.Invoke(new EventHandler(Qidong.KillMe));
                        Qidong.Dispose();
                    };
                    //显示Qidong窗体 
                    Application.Run(Qidong);

                })).Start();
            };

            
            //显示主窗体 
            Application.Run(form);
        }

    }
}
