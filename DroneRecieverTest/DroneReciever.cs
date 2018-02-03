using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DroneRecieverTest
{
    public class DroneReciever
    {
        private TcpClient clientSocket;
        private TcpListener serverSocket;

        public DroneReciever()
        {
            connect();

            Thread ctThread = new Thread(recieve);
            ctThread.Start();
        }
        private void connect()
        {
            try
            {
                Console.WriteLine("Please input the IP of the phone's server:");
                string ip = Console.ReadLine().Trim();
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect(ip, 9001);

                //THE APP IS IPv6 SO MODIFY THIS TO DO THAT AND MODIFY APP TO SHOW THAT

                    //SERVER CODE
                    /*serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), 9001);
                    clientSocket = default(TcpClient);

                    serverSocket.Start();
                    Console.WriteLine(" >> " + "Drone Started");


                    clientSocket = serverSocket.AcceptTcpClient();*/

                 Console.WriteLine(" >> " + "App Connected");
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input. " + e.ToString());
            }
        }
        private void recieve()
        {
            while (true)
            {
                try
                {
                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                    char[] temp = new char[1];
                    temp[0] = '\0';
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream).Trim(temp);

                    Console.WriteLine(returndata);
                }
                catch (NullReferenceException e)
                {
                   
                }

                //SERVER CODE
                /*byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
                string dataFromClient = null;
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                char[] temp = new char[1];
                temp[0] = '\0';
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom).Trim(temp);

                Console.WriteLine(dataFromClient);*/
            }

            clientSocket.Close();
            serverSocket.Stop();
        }
    }
}
