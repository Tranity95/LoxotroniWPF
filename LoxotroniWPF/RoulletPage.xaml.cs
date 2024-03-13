using LoxotroniAPI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace LoxotroniWPF
{
    /// <summary>
    /// Логика взаимодействия для RoulletPage.xaml
    /// </summary>
    public partial class RoulletPage : Window, INotifyPropertyChanged
    {
        public decimal Balance { get; set; }
        public UserDTO User { get; set; }
        public RoulletPage(UserDTO user)
        {
            InitializeComponent();
            this.User = user;
            Balance = user.Balance;
            DataContext = this;
            Signal(nameof(Balance));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        private void WheelRoulette(object sender, RoutedEventArgs e)
        {
            Wheel wheel = new Wheel(User);
            wheel.Show();
            Close();
        }

        private void ClassicRoulette(object sender, RoutedEventArgs e)
        {
            Classic classic = new Classic(User);
            classic.Show();
            Close();
        }
    }
}
