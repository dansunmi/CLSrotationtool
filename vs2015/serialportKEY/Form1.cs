using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;




namespace serialportKEY
{
    
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(int wCode, int wMapType);
        SerialPort sp;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "COM6";
            textBox2.Text = "9600";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == sp)
                {
                    sp = new SerialPort();
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);


                    sp.PortName = textBox1.Text.ToString();
                    sp.BaudRate = Convert.ToInt32(textBox2.Text);
                    sp.DataBits = (int)8;
                    sp.StopBits = StopBits.One;
                    sp.ReadTimeout = (int)500;
                    sp.WriteTimeout = (int)500;
                    sp.Open();
                }
                if (sp.IsOpen)
                {
                    MessageBox.Show("연결성공");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int intRecSize = sp.BytesToRead;

            if (intRecSize != 0)
            {

                
                byte[] buff = new byte[intRecSize];
                sp.Read(buff, 0, intRecSize);
                
                for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                {
                    if(buff[iTemp] == 0x52)
                    {
                        //MessageBox.Show("R");
                        keybd_event(0xbb, 0, 0x00, 0);
                        keybd_event(0xbb, 0, 0x02, 0);
                    }
                    else if(buff[iTemp] == 0x4C)
                    {
                        //MessageBox.Show("L");
                        keybd_event(0xbd, 0, 0x00, 0);
                        keybd_event(0xbd, 0, 0x02, 0);
                    }
                }

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(null != sp)
            {
                if(sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                    sp = null;
                }
            }
        }
      
    }
}
