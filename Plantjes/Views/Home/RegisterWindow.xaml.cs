using Plantjes.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : UserControl
    {
        public RegisterWindow()
        {
            this.DataContext = GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<ViewModelRegister>();
            InitializeComponent();
        }

    }
}
