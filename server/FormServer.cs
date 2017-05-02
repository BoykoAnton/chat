using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLibrary;
using System.Threading;

namespace server
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
            main_func();
        }

        static Server server; // сервер
        //static Thread listenThread; // потока для прослушивания

        public void main_func()
        {

            try
            {
                server = new Server(richTextBox1);
                server.Listen();
                //listenThread = new Thread(new ThreadStart(server.Listen));
                //listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                server.Disconnect();
            }
        }

    }
}
