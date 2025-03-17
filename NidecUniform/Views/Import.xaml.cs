using NidecUniform.Models.Model;
using NidecUniform.Repositories.Interface;
using NidecUniform.ViewModels;
using NidecUniform.Views.Common;
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
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : UserControl, IUIServices
    {
        public Import()
        {
            InitializeComponent();
            DataContext = new ImportVM(this);
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
