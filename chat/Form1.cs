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

            groupIP = IPAddress.Parse(HOST);
        }
    }
}
