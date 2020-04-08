using System;
using System.Collections.Generic;
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
    /// LoginPage.xaml 的互動邏輯
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            CNM.SendSockConnect();

            CNM.SendSock.Send(new byte[1] { 0 });
            CNM.SendSock.Send(BitConverter.GetBytes(User.Text.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(User.Text));
            CNM.SendSock.Send(BitConverter.GetBytes(Pwd.Password.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(Pwd.Password));

            byte[] msg = new byte[1];
            CNM.ReceiveAll(msg);
            if (msg[0] == BitConverter.GetBytes(0)[0])
            {
                CNM.SendSock.Shutdown(SocketShutdown.Both);
                CNM.SendSock.Close();
                CNM.Reset();
                MessageBox.Show("Username does not exist or wrong password!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                CNM.IsLogin = true;
                CNM.MyUsername = User.Text;
                ((MainWindow)Application.Current.MainWindow).OnLogin();
            }
        }

        private void OnCreateAccountClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).OnNewAcc();
        }
    }
}
