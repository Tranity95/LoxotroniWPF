using LoxotroniAPI.DTO;
using LoxotroniWPF.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
    /// Логика взаимодействия для Classic.xaml
    /// </summary>
    public partial class Classic : Window, INotifyPropertyChanged
    {
        public decimal Balance 
        { 
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            } 
        }
        
        public string SelectedColor { get; set; }
        public decimal Stake { get; set; }
        public UserDTO User { get; set; }
        public List<string> Colors { get; set; }

        private int angle;
        private string winColor;
        private decimal balance;

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Classic(UserDTO user)
        {
            InitializeComponent();
            this.User = user;
            Balance = user.Balance;
            Colors = new List<string>(new string[]
            {
                "Red",
                "Black",
                "Green"
            });
            DataContext = this;
            OnPropertyChanged(nameof(Balance));
        }

        private void Spin(object sender, RoutedEventArgs e)
        {
            Random rndSpin = new Random();
            int chance = rndSpin.Next(1, 101);
            if (chance <= 49)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 6);
                if (chanceAngle == 1)
                {
                    angle = 360;
                }
                if (chanceAngle == 2)
                {
                    angle = 420;
                }
                if (chanceAngle == 3)
                {
                    angle = 480;
                }
                if (chanceAngle == 4)
                {
                    angle = 550;
                }
                if (chanceAngle == 5)
                {
                    angle = 675;
                }
                winColor = "Black";
            }
            else if (chance <= 99 && chance > 49)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 6);
                if (chanceAngle == 1)
                {
                    angle = 685;
                }
                if (chanceAngle == 2)
                {
                    angle = 460;
                }
                if (chanceAngle == 3)
                {
                    angle = 510;
                }
                if (chanceAngle == 4)
                {
                    angle = 575;
                }
                if (chanceAngle == 5)
                {
                    angle = 635;
                }
                winColor = "Red";
            }
            else if (chance > 99)
            {
                Random angleChance = new Random();
                int chanceAngle = angleChance.Next(1, 3);
                if (chanceAngle == 1)
                {
                    angle = 390;
                }
                if (chanceAngle == 2)
                {
                    angle = 610;
                }
                winColor = "Green";
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
           var user = await Client.Instance.Classic(SelectedColor, Stake, User, winColor);
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
