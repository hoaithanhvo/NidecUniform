using NidecUniform.Repositories;
using NidecUniform.ViewModels;
using System.Net.WebSockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private readonly IEmpoloyee _empoloyeeRepository;
        public MainWindow(IEmpoloyee empoloyeeRepository)
        {
            InitializeComponent();
            _empoloyeeRepository = empoloyeeRepository;
            this.DataContext = new NavigationVM();
              
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
       
    }
}