using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private ThreadStart threadStart1;
        private ThreadStart threadStart2;
        private bool exitSystem = false;


        public MainWindow()
        {
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


                this.serverIp = "192.168.0.8";
                this.serverPort = 8081;
                this.password = "abertawe";

                Connect();
            }
        }



        public void Connect()
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

                InitialConnection();


                //ReadData();
                //threadStart1 = new ThreadStart(ReadData);
                //Thread thread1 = new Thread(threadStart1);
                //thread1.Start();

                //threadStart2 = new ThreadStart(WriteData);
                //Thread thread2 = new Thread(threadStart2);
                //thread2.Start();

            }
            catch (SocketException e)
            {
                //Console.WriteLine("SocketException: " + e + ", failed to connect");
            }

            //Console.WriteLine("Connection complete - closing");
            // client.Close();
        }


        public void InitialConnection()
        {
            //read
            Read();
            //write password
            Write(password);
        }


        public void Read()
        {
            // String to store the response ASCII representation.
            string responseData = string.Empty;
            Byte[] data = new Byte[1024];
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            outputWindow.AppendText("Received: " + responseData);
            //Console.WriteLine("Received: {0}", responseData);
        }


        public void Write(string message)
        {
            Byte[] data = new Byte[1024];
            message += Environment.NewLine;
            data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public void ReadData()
        {
            while (!exitSystem)
            {
                //read
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                Byte[] data = new Byte[1024];
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("length: " + responseData.Length);
                if (responseData.Length > 2)
                {
                    //Console.WriteLine("Server: {0}", responseData);
                    outputWindow.AppendText("Server: " + responseData);
                }

                //Thread.Sleep(10);
            }
            exitSystem = true;
            client.Close();
        }


        private void WriteData()
        {

            string message = "";
            Byte[] data = new Byte[1024];
            while (!exitSystem)
            {
                Console.WriteLine("Input: ");
                message = Console.ReadLine();

                if (message.Equals("lplay"))
                {
                    message = "listplayers";
                }

                message += Environment.NewLine;
                data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                stream.Flush();
                Thread.Sleep(10);
                Console.Write("Input: ");
            }

        }


    }
}
