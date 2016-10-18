using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _7dtdManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //varibles
        private string serverIp;
        private int serverPort;
        private string password;

        private TcpClient client;
        private NetworkStream networkStream;
        private NetworkStream stream;
        //private ThreadStart threadStart1;
        //private ThreadStart threadStart2;
        private bool connected = false;

        private List<Player> playerList;


        public MainWindow()
        {
            playerList = new List<Player>();
            InitializeComponent();
        }



        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!iptextBox.Text.Equals(""))
            {
                outputWindow.AppendText("***DEBUG*** : Ip not empty \n");

                this.serverIp = iptextBox.Text;
                this.serverPort = int.Parse(porttextBox.Text);
                this.password = passwordBox.Password;

                outputWindow.AppendText("Ip: " + serverIp + ", Port: " + serverPort + "\n");

                Connect();
            }
            else
            {
                outputWindow.AppendText("***DEBUG*** : Ip empty \n");

                this.serverIp = "80.4.92.204";
                // this.serverIp = "192.168.0.8";
                this.serverPort = 8081;
                this.password = "abertawe";

                Connect();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            connected = false;
            client.Close();
            outputWindow.AppendText("***DEBUG*** : Disconnected \n");
        }





        public async void Connect()
        {
            try
            {
                client = new TcpClient(serverIp, serverPort);
                // Console.WriteLine("Connected to " + serverIp + " on Port: " + serverPort);

                //Assign networkstream
                networkStream = client.GetStream();

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                stream = client.GetStream();

                //connected
                connected = true;

                //initial read and pasword send
                //InitialConnection();
                await Task.Run(() => Read(this));
                await Task.Run(() => Write(this, password));
                Task.Run(() => ReadData(this));
            }
            catch (SocketException e)
            {
                //Console.WriteLine("SocketException: " + e + ", failed to connect");
                outputWindow.AppendText("***DEBUG*** : Disconnected error " + e + " \n");
            }


            //client.Close();
            outputWindow.AppendText("***DEBUG*** : Disconnected \n");
        }

        public void Read(MainWindow gui)
        {
      
            // String to store the response ASCII representation.
            string responseData = string.Empty;
            Byte[] data = new Byte[1024];
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //  outputWindow.AppendText("***DEBUG*** : " + responseData + "\n");
            gui.UpdateConsoleWindow("***DEBUG** - Data read Complete!!!!!");
   
        }

        public void Write(MainWindow gui, string message)
        {
            Byte[] data = new Byte[1024];
            message += Environment.NewLine;
            data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);      
            gui.UpdateConsoleWindow("***DEBUG** - writing to output: " + message);
            Thread.Sleep(1000);
            stream.Flush();
        }


        public void ReadData(MainWindow gui)
        {
            // gui.UpdateConsoleWindow("Working!!!!!");            
            var responseData = "";
                while (connected)
                {
                       gui.UpdateConsoleWindow("Server: ***inside readdata outer*****");
              
                       gui.UpdateConsoleWindow("Server: ***inside readdata  inner*****");

                        responseData = ReadFromServer();
                        //Console.WriteLine("length: " + responseData.Length);
                        if (responseData.Length > 2)
                        {
                            gui.UpdateConsoleWindow("Server: " + responseData);
                            // outputWindow.AppendText("Server: " + responseData);
                        }
                        else
                        {
                            gui.UpdateConsoleWindow("Server: Nothing to  report");
                        }

                 Thread.Sleep(60000);
                }

            gui.UpdateConsoleWindow("***DEBUG** - ReadData Completed!!!!!");


        }

        void UpdateConsoleWindow(string text)
        {
            var textTrim = text.Trim();
            Dispatcher.Invoke(() =>
            {
                outputWindow.AppendText(textTrim + "\n");
            });
        }
        

        public string ReadFromServer()
        {
            String responseData = String.Empty;
            Byte[] data = new Byte[1024];
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            return responseData;
        }

        void UpdatePlayerWindow(List<Player> playerList)
        {
            Dispatcher.Invoke(() =>
            {
                listBox.Items.Clear();
                foreach (Player p in playerList)
                listBox.Items.Add(p.Name);
            });
        }


       void GetPayersFromServerString()
        {

            //listplayers
            //2016-10-18T10:59:08 445354.197 INF Executing command 'listplayers' by Telnet from 158.234.250.7:28820
            //1. id=14588, sfur, pos=(1847.5, 4.1, -270.5), rot=(-29.5, 59.1, 0.0), remote=True, health=100, deaths=7, zombies=1177, players=0, score=1142, level=144, steamid=76561198015252609, ip=80.89.51.95, ping=76
            //2. id=15983, Emanuel Kant, pos = (122.5, 41.5, 505.5), rot=(90.0, -4052.8, 0.0), remote=True, health=166, deaths=1, zombies=554, players=0, score=549, level=98, steamid=76561198060542438, ip=86.6.32.123, ping=23
            //Total of 2 in the game

            List<Player> temp = new List<Player>();
            //remove first line
            //string[] lines = data.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
            //split data up from each playerstats

            var message = "";
            if (!consoleBox.Text.Equals(""))
            {
                outputWindow.AppendText("***DEBUG***  - Command " + consoleBox.Text + "\n");
                message = consoleBox.Text;
                Write(this, message);
            }
            message = "";
            consoleBox.Text = "";
            string str = ReadFromServer();


            MessageBox.Show("***DEBUG****" + str);
        }

   
        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            GetPayersFromServerString();
        }

       

    }
}


//listplayers
//2016-10-18T10:59:08 445354.197 INF Executing command 'listplayers' by Telnet from 158.234.250.7:28820
//1. id=14588, sfur, pos=(1847.5, 4.1, -270.5), rot=(-29.5, 59.1, 0.0), remote=True, health=100, deaths=7, zombies=1177, players=0, score=1142, level=144, steamid=76561198015252609, ip=80.89.51.95, ping=76
//2. id=15983, Emanuel Kant, pos = (122.5, 41.5, 505.5), rot=(90.0, -4052.8, 0.0), remote=True, health=166, deaths=1, zombies=554, players=0, score=549, level=98, steamid=76561198060542438, ip=86.6.32.123, ping=23
//Total of 2 in the game
