using NidecUniform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NidecUniform.CustomControl
{
    class RadioButtonControl:RadioButton
    {
        static RadioButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonControl), new FrameworkPropertyMetadata(typeof(RadioButtonControl)));
        }
    }
}
