using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NidecUniform.Repositories.Interface
{
    public interface IUIServices
    {
        void ShowProgressDialog();
        void HideProgressDialog();
    }
}
