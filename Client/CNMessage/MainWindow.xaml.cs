using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNMessage
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    ///

    public static class CNM
    {
        static CNM()
        {
            IsLogin = false;
            MyUsername = "";
            serverAddress = "127.0.0.1";
            sendport = 35645;
            SendSock = null;
        }

        public static bool IsLogin;
        public static string MyUsername;

        static readonly string serverAddress;
        static readonly int sendport;

        public static Socket SendSock;

        public static void SendSockConnect()
        {
            SendSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SendSock.Connect(new IPEndPoint(IPAddress.Parse(serverAddress), sendport));
        }

        public static void Reset()
        {
            IsLogin = false;
            MyUsername = "";
            SendSock = null;
        }

        public static void ReceiveAll(byte[] msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
            
            int size = msg.Length;
            for (int bytesLeft = size, receivedBytes = 0; bytesLeft > 0; bytesLeft -= receivedBytes)
                receivedBytes = SendSock.Receive(msg, size - bytesLeft, bytesLeft, 0);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            contentControl.Content = new LoginPage();
        }

        public void OnLogin()
        {
            contentControl.Content = new ChatPage();
            Title = "CNMessage";
        }

        public void OnNewAcc()
        {
            contentControl.Content = new RegisterPage();
            Title = "Register";
        }

        public void OnRegister()
        {
            contentControl.Content = new LoginPage();
            Title = "Login";
        }

        public void OnLogout()
        {
            if (CNM.IsLogin)
                CNM.SendSock.Send(new byte[1] { 5 });
            CNM.SendSock?.Shutdown(SocketShutdown.Both);
            CNM.SendSock?.Close();
            CNM.Reset();
            contentControl.Content = new LoginPage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnLogout();
        }
    }
}
