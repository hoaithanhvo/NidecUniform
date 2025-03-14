using NidecUniform.Models.Model;
using NidecUniform.Repositories.Interface;
using NidecUniform.ViewModels;
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

namespace NidecUniform.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IUIServices
    {
        public Home()
        {
            InitializeComponent();
            this.DataContext = new HomeVM(this);
        }

        public Task<PieModel> getDataPieChart()
        {
            throw new NotImplementedException();
        }

        public void HideProgressDialog()
        {
            throw new NotImplementedException();
        }

        public void ShowProgressDialog()
        {
            throw new NotImplementedException();
        }
    }
}
