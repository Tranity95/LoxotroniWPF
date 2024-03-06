using LoxotroniAPI.DTO;
using LoxotroniWPF.API;
using LoxotroniWPF.HelperLogin;
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

namespace LoxotroniWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserDTO User { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await Client.Instance.UserLogin(User, Login, Password);
                RoulletPage roulletPage = new RoulletPage(user);
                roulletPage.Show();
                //MessageBox.Show("Пиривет Лох Безденежный");
                Close();
            }
            catch (Exception ex) {
            MessageBox.Show(ex.Message);    
            
            }
        }

        private void GoSignUp(object sender, RoutedEventArgs e)
        {

        }
    }
}
