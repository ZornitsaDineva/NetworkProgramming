using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkProgramming
{
    class SocketExample
    {
        public void ClientSocketTest()
        {
            IPAddress ip = IPAddress.Parse("157.240.9.35");
            IPEndPoint ep = new IPEndPoint(ip, 80);
            Socket s = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.IP);
            try
            {
                s.Connect(ep);
                if (s.Connected)
                {
                    String strSend = "GET\r\n\r\n";
                    s.Send(System.Text.Encoding.ASCII.
                    GetBytes(strSend));

                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = s.Receive(buffer);
                        string txt =
                        System.Text.Encoding.ASCII.
                        GetString(buffer, 0, l);
                        System.Console.Write(txt);
                    } while (l > 0);
                }
                else
                    System.Console.WriteLine("Error");
            }
            catch (SocketException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }

        public void StartServer()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1024);
            s.Bind(ep);
            s.Listen(10);
            try
            {
                while (true)
                {                   
                    Socket ns = s.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    ns.Send(System.Text.Encoding.ASCII.
                    GetBytes(DateTime.Now.ToString()));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
