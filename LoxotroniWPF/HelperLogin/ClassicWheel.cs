using LoxotroniAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoxotroniWPF.HelperLogin
{
    public class ClassicWheel
    {
        public decimal Stake { get; set; }
        public string Color { get; set; }
        public UserDTO User { get; set; }
        public string WinColor { get; set; }
    }
}
