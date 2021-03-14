using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace SimpleMessenger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimpleTcpServer server;

        public MainWindow()
        {
            InitializeComponent();

            server = new SimpleTcpServer();
            server.Delimiter = 19;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += DataReceived;
        }

        private void DataReceived(object sender, Message e)
        {
            Dispatcher.Invoke(() =>
            {
                TextSend.Text = e.MessageString;
                server.BroadcastLine(TextSend.Text);
            });   
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(TextHost.Text);
            server.Start(ip, Convert.ToInt32(TextPort.Text));
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (server.IsStarted)
            {
                server.Stop();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
