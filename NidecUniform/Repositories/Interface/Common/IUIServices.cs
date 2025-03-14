using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface
{
    public interface IUIServices
    {
        void ShowProgressDialog()
        {
            // Triển khai mặc định cho ShowProgressDialog
            Console.WriteLine("Showing progress dialog (default implementation)");
        }

        void HideProgressDialog()
        {
            // Triển khai mặc định cho HideProgressDialog
            Console.WriteLine("Hiding progress dialog (default implementation)");
        }

    }
}
