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

namespace Plantjes.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlPassword.xaml
    /// </summary>
    public partial class UserControlPassword : UserControl
    {

        //Using a DependencyProperty as the backing store for Password.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(UserControlPassword),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));
        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public UserControlPassword()
        {
            InitializeComponent();
        }

    }
}
