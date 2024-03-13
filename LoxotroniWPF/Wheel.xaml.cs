using LoxotroniAPI.DTO;
using LoxotroniWPF.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoxotroniWPF
{
    /// <summary>
    /// Логика взаимодействия для Wheel.xaml
    /// </summary>
    public partial class Wheel : Window, INotifyPropertyChanged
    {
        public string Thing { get; set; }
        public UserDTO User { get; set; }
        public decimal Balance 
        { 
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public decimal Stake { get; set; }
        private int angle;
        private decimal balance;

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Wheel(UserDTO user)
        {
            InitializeComponent();
            this.User = user;
            Balance = User.Balance;
            DataContext = this;
        }
        private void Spin(object sender, RoutedEventArgs e)
        {
            Random rndSpin = new Random();
            int chance = rndSpin.Next(1, 17);
            if (chance <= 6)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 4);
                if (chanceAngle == 1)
                {
                    angle = 670;
                }
                if (chanceAngle == 2)
                {
                    angle = 550;
                }
                if (chanceAngle == 3)
                {
                    angle = 485;
                }
                Thing = "-x0.5";
            }
            else if (chance > 6 && chance <= 10)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 4);
                if (chanceAngle == 1)
                {
                    angle = 410;
                }
                if (chanceAngle == 2)
                {
                    angle = 525;
                }
                if (chanceAngle == 3)
                {
                    angle = 590;
                }
                Thing = "+x1.2";
            }
            else if (chance > 10 && chance <= 14)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 3);
                if (chanceAngle == 1)
                {
                    angle = 450;
                }
                if (chanceAngle == 2)
                {
                    angle = 630;
                }
                Thing = "-stake";
            }
            else if (chance == 15)
            {
                angle = 380;
                Thing = "-x2";
            }
            else if (chance == 16)
            {
                angle = 705;
                Thing = "+x3";
            }

            DoubleAnimation animation2 = new DoubleAnimation
            {
                From = 0,
                To = angle,
                Duration = new Duration(TimeSpan.FromSeconds(3)),
            };
            RotateTransformWheel.BeginAnimation(RotateTransform.AngleProperty, animation2);
            DoStuff();
        }

        private async void DoStuff()
        {
            var user = await Client.Instance.Wheel(Stake, User, Thing);
            User = user;
            DoDelay(user.Balance);
        }

        public async void DoDelay(decimal newBalance)
        {
            Task task = new Task(() =>
            {
                Thread.Sleep(3000);
                Balance = newBalance;
            });
            task.Start();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            RoulletPage roullet = new RoulletPage(User);
            roullet.Show();
        }
    }
}
