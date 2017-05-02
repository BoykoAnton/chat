using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat
{
    public partial class Form1 : Form
    {
        const int LOCALPORT = 8001; //port for receive messages
        const int REMOTEPORT = 8001; //port for sending messages
        const int TTL = 20; //time to live packet
        const string HOST = "228.2.2.8"; //host for group distribution

        bool alive = false; //will work stream for receive
        UdpClient client; //creating udp client
        IPAddress groupIP; //addres for group distribution
        string nickname; // name of user in chat

        public Form1() //std constructor
        {
            InitializeComponent();

            groupIP = IPAddress.Parse(HOST); //convetr ip from string to type "IPAddress"
        }

        private void button1_Click(object sender, EventArgs e) //click on login button
        {
            nickname = textBox1.Text; //get nickname from textbox
            textBox1.ReadOnly = true; //ban editing textbox with username
            
            try
            {
                //client = new UdpClient(LOCALPORT); //bind client on LOCALPORT
                //
                //client.JoinMulticastGroup(groupIP, TTL); //conect to group distribution
                //
                //Task receiveTask = new Task(receiveMsg); //creating async operation for receiving messages
                //receiveTask.Start(); // start receiveTask asynk operation
                //
                //string message = nickname + " now online"; //creating a string about new member in chat
                //byte[] data = Encoding.Unicode.GetBytes(message); //creating buffer data and converting message into that buffer
                //client.Send(data, data.Length, HOST, REMOTEPORT); //sending our message in buffer of bytes to REMOTEPORT

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                textBox2.Enabled = true;
            }   
            catch
            {   

            }
        }
    }
}
