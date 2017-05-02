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
        UdpClient client; //creating UDP client
        IPAddress groupIP; //addres for group distribution
        string nickname; // name of user in chat

        public Form1() //std constructor
        {
            InitializeComponent();
            
            groupIP = IPAddress.Parse(HOST); //convetr ip from string to type "IPAddress"
        }

        private void receiveMsg() //func for receiving msg
        {
            alive = true; //stream for receive is working

            try
            {
                while (alive) // loop while receiving stream is alive
                {
                    IPEndPoint remoteIP = null; //creating endpoint ip + port
                    byte[] data = client.Receive(ref remoteIP); //receive data (remote ip use reference)
                    string message = Encoding.Unicode.GetString(data); //encoding buffer of bytes to string 

                    this.Invoke(new MethodInvoker(() =>
                    {
                        richTextBox1.Text += "[ " + DateTime.Now.ToShortTimeString() + " ] "+  message + "\n";
                    }));
                }
            }
            catch (ObjectDisposedException) //processing ObjectDisposedException
            {
                if (!alive) 
                {
                    return;
                }
                throw;
            }
            catch (Exception ex) //processing usual exceptions
            {
                MessageBox.Show("Exception :" + ex.Message); //show exception
            }
        } 
        
        private void exitChat() //func for exit from chat
        {
            string message = nickname + " exit chat"; //message about exit from chat
            byte[] data = Encoding.Unicode.GetBytes(message); //encoding to bytes
            client.Send(data, data.Length, HOST, REMOTEPORT); //sending message
            client.DropMulticastGroup(groupIP); //exit from group

            alive = false; //alive of receive stream statechanging to false
            client.Close(); //close conection

            //work with interface
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //click on login button
        {
            nickname = textBox1.Text; //get nickname from textbox
            textBox1.ReadOnly = true; //ban editing textbox with username
            
            try
            {
                client = new UdpClient(LOCALPORT); //bind client on LOCALPORT
                
                client.JoinMulticastGroup(groupIP, TTL); //conect to group distribution
                
                Task receiveTask = new Task(receiveMsg); //creating async operation for receiving messages
                receiveTask.Start(); // start receiveTask asynk operation
                
                string message = nickname + " now online"; //creating a string about new member in chat
                byte[] data = Encoding.Unicode.GetBytes(message); //creating buffer data and converting message into that buffer
                client.Send(data, data.Length, HOST, REMOTEPORT); //sending our message in buffer of bytes to REMOTEPORT

                //work with interface:
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                textBox2.Enabled = true;
            }   
            catch (Exception ex)//procesing exception
            {
                MessageBox.Show("Exception :" + ex.Message); //show exception
            }
        }

        private void button2_Click(object sender, EventArgs e) //click on logout button
        {
            exitChat(); //call function for exit from chat
        }

        private void button3_Click(object sender, EventArgs e) //click on send button
        {
            try
            {
                string message = String.Format("{0}: {1}", nickname, textBox2.Text); //formating message
                byte[] data = Encoding.Unicode.GetBytes(message); //converting message to buffer of bytes

                client.Send(data, data.Length, HOST, REMOTEPORT); //sending message

                textBox2.Clear(); //clearing textBox
            }
            catch (Exception ex) //processing exception
            {
                MessageBox.Show("Exception :" + ex.Message); //show exception
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //when form closing
        {
            if (alive) //if user in chat exit chat
                exitChat();
        }
    }
}