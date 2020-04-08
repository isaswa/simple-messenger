using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CNMessage
{
    /// <summary>
    /// ChatPage.xaml 的互動邏輯
    /// </summary>
    
    public class FileGridEntry : INotifyPropertyChanged
    {
        public FileGridEntry(string _sender, string _filename, byte[] _file, string _size)
        {
            Sender = _sender;
            Filename = _filename;
            File = _file;
            Size = _size + 'B';
        }

        string sender;
        string filename;
        string size;

        public string Sender
        {
            get => sender;
            set
            {
                sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        public string Filename
        {
            get => filename;
            set
            {
                filename = value;
                OnPropertyChanged(nameof(Filename));
            }
        }

        public string Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public byte[] File { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public partial class ChatPage : UserControl
    {
        public bool IsChatNotEmpty => PreparetoSend.Text.Length != 0;

        DispatcherTimer PeekTimer;

        ObservableCollection<FileGridEntry> DTSource;

        public ChatPage()
        {
            InitializeComponent();
            DTSource = new ObservableCollection<FileGridEntry>();
            FilesGrid.ItemsSource = DTSource;
            RetrieveHistory();
            PeekTimer = new DispatcherTimer();
            PeekTimer.Tick += new EventHandler(PeekMessage);
            PeekTimer.Interval = new TimeSpan(0, 0, 1);
            PeekTimer.Start();
        }

        private void RetrieveHistory()
        {
            CNM.SendSock.Send(new byte[1] { 2 });

            byte[] msg = new byte[4];
            ChatBox.Text = "";
            CNM.ReceiveAll(msg);
            uint recordcount = BitConverter.ToUInt32(msg, 0);

            for (uint i = 0; i < recordcount ; i++)
            {
                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint usernamesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[usernamesize];
                CNM.ReceiveAll(msg);
                string username = Encoding.ASCII.GetString(msg);

                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint messagesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[messagesize];
                CNM.ReceiveAll(msg);
                string message = Encoding.ASCII.GetString(msg);

                ChatBox.Text += (username + ": " + message + Environment.NewLine);
            }         
        }

        private void PeekText()
        {
            CNM.SendSock.Send(new byte[1] { 13 });

            byte[] msg = new byte[4];
            CNM.ReceiveAll(msg);
            uint recordcount = BitConverter.ToUInt32(msg, 0);

            for (uint i = 0; i < recordcount; i++)
            {
                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint usernamesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[usernamesize];
                CNM.ReceiveAll(msg);
                string username = Encoding.ASCII.GetString(msg);

                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint messagesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[messagesize];
                CNM.ReceiveAll(msg);
                string message = Encoding.ASCII.GetString(msg);

                ChatBox.Text += (username + ": " + message + Environment.NewLine);
            }
        }

        private void PeekFile()
        {
            CNM.SendSock.Send(new byte[1] { 14 });

            byte[] msg = new byte[4];
            CNM.ReceiveAll(msg);
            uint recordcount = BitConverter.ToUInt32(msg, 0);

            for (uint i = 0; i < recordcount; i++)
            {
                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint usernamesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[usernamesize];
                CNM.ReceiveAll(msg);
                string username = Encoding.ASCII.GetString(msg);

                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint filenamesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[filenamesize];
                CNM.ReceiveAll(msg);
                string filename = Encoding.ASCII.GetString(msg);

                msg = new byte[4];
                CNM.ReceiveAll(msg);
                uint filesize = BitConverter.ToUInt32(msg, 0);

                msg = new byte[filesize];
                CNM.ReceiveAll(msg);

                DTSource.Add(new FileGridEntry(username, filename, msg, filesize.ToString()));
                ChatBox.Text += (username + ": " + filename + Environment.NewLine);
            }
        }

        private void PeekMessage(object sender, EventArgs e)
        {
            if (!CNM.IsLogin)
                return;
            PeekText();
            PeekFile();
        }

        private void OnSendClick(object sender, RoutedEventArgs e)
        {
            CNM.SendSock.Send(new byte[1] { 3 });
            CNM.SendSock.Send(BitConverter.GetBytes(TargetUser.Text.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(TargetUser.Text));
            CNM.SendSock.Send(BitConverter.GetBytes(PreparetoSend.Text.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(PreparetoSend.Text));

            byte[] msg = new byte[1];
            CNM.ReceiveAll(msg);
            if (msg[0] == BitConverter.GetBytes(0)[0])
            {
                MessageBox.Show("Server error!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            PreparetoSend.Text = "";
        }

        private void OnFileClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != true)
                return;

            byte[] file = File.ReadAllBytes(ofd.FileName);
            CNM.SendSock.Send(new byte[1] { 4 });
            CNM.SendSock.Send(BitConverter.GetBytes(TargetUser.Text.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(TargetUser.Text));
            CNM.SendSock.Send(BitConverter.GetBytes(ofd.SafeFileName.Length));
            CNM.SendSock.Send(Encoding.ASCII.GetBytes(ofd.SafeFileName));
            CNM.SendSock.Send(BitConverter.GetBytes(file.Length));
            CNM.SendSock.Send(file);

            byte[] msg = new byte[1];
            CNM.ReceiveAll(msg);
            if (msg[0] == BitConverter.GetBytes(0)[0])
            {
                MessageBox.Show("Failed to send file!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).OnLogout();
        }

        private void OnFilesGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileGridEntry selected = (FileGridEntry)FilesGrid.SelectedItem;
            var sfd = new SaveFileDialog();
            sfd.FileName = selected.Filename;

            if(sfd.ShowDialog() == true)
            {
                File.WriteAllBytes(sfd.FileName, selected.File);
            }
        }
    }
}
