using NidecUniform.Repositories;
using System.Net.WebSockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NidecUniform
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmpoloyeeRepository _empoloyeeRepository;

        public MainWindow(IEmpoloyeeRepository empoloyeeRepository)
        {
            InitializeComponent();
            _empoloyeeRepository = empoloyeeRepository;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = _empoloyeeRepository.GetEmployee(44177);
        }
    }
}