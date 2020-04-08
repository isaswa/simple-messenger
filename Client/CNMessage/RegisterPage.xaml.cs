using System;
using System.Collections.Generic;
using System.Linq;
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
    /// RegisterPage.xaml 的互動邏輯
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        public bool IsFieldNotEmpty => !(User.Text.Length == 0) & !(Pwd.Password.Length == 0);

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            CNM.SendSockConnect();

            CNM.SendSock.Send(new byte[1] { 1 });
            CNM.SendSock.Send(BitConverter.GetBytes(User.Text.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(User.Text));
            CNM.SendSock.Send(BitConverter.GetBytes(Pwd.Password.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(Pwd.Password));

            byte[] msg = new byte[1];
            CNM.ReceiveAll(msg);
            CNM.SendSock.Shutdown(SocketShutdown.Both);
            CNM.SendSock.Close();
            CNM.Reset();
            if (msg[0] == BitConverter.GetBytes(0)[0])
            {
                MessageBox.Show("Username already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).OnRegister();
                MessageBox.Show("Registered!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).OnRegister();
        }
    }
}
