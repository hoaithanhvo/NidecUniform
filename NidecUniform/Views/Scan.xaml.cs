using NidecUniform.ConnectZebra;
using NidecUniform.Models.Model;
using NidecUniform.Repositories.Interface;
using NidecUniform.ViewModels;
using NidecUniform.Views.Common;
using STC;
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
    /// Interaction logic for Scan.xaml
    /// </summary>
    public partial class Scan : UserControl, IUIServices
    {
        public Scan()
        {
            InitializeComponent();
            this.DataContext = new ScanVM(this);
        }

        public Task<PieModel> getDataPieChart()
        {
            throw new NotImplementedException();
        }

        public void HideProgressDialog()
        {
            ShowProgressDialogCommon.HideProgressDialog(this);
        }

        public void ShowProgressDialog()
        {
            ShowProgressDialogCommon.ShowProgressDialog(this);
        }

       
    }
}
