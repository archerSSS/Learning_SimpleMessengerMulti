using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SimpleMessengerClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimpleTcpClient client;
        private string nickName;

        public MainWindow()
        {
            InitializeComponent();

            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += DataReceived;
        }

        private void DataReceived(object sender, Message e)
        {
            Dispatcher.Invoke(() => TextMessages.Text += e.MessageString + "\r");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.WriteLineAndGetReply(nickName + ": " + TextMessageField.Text, TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                TextMessageField.Text = "Error";
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextName.Text))
            {
                TextMessageField.Text = "Connection failed. Type your nickname";
                return;
            }
                
                //client.WriteLineAndGetReply("From Client", TimeSpan.FromSeconds(3));
            try
            {
                client.Connect(TextHost.Text, Convert.ToInt32(TextPort.Text));
                nickName = TextName.Text;
                TextMessageField.Text = "Connection Success";
            }
            catch (Exception ex)
            {
                TextMessageField.Text = "Connection failed";
            }

        }
    }
}
