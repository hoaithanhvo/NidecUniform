
using System.Windows;
using System.Windows.Controls;


namespace NidecUniform.UserControls
{
  
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(InfoCard));

        public string Quantity
        {
            get { return (string)GetValue(QuantityProperty); }
            set { SetValue(QuantityProperty, value); }
        }
        public static readonly DependencyProperty QuantityProperty = DependencyProperty.Register("Quantity", typeof(string), typeof(InfoCard));

        public bool CardIsActive
        {
            get { return (bool)GetValue(CardIsActiveProperty); }
            set { SetValue(CardIsActiveProperty, value); }
        }
        public static readonly DependencyProperty CardIsActiveProperty = DependencyProperty.Register("CardIsActive", typeof(bool), typeof(InfoCard));


        public MahApps.Metro.IconPacks.PackIconMaterialKind Icon
        {
            get { return (MahApps.Metro.IconPacks.PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(MahApps.Metro.IconPacks.PackIconMaterialKind), typeof(InfoCard));
    }
}
