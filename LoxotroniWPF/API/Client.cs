using LoxotroniAPI.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoxotroniWPF.HelperLogin;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Json;

namespace LoxotroniWPF.API
{
    public class Client
    {
        HttpClient httpClient = new HttpClient();
        public Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7004/");
        }

        //public async Task<UserDTO> UserLogin(string login, string password)
        //{
        //    var response = await httpClient.PostAsync("User/UserLogin");
        //}
        public async Task<UserDTO> UserLogin(UserDTO user, string login, string password)
        {

            using (var client = new HttpClient())
            {
                var loginUser = new LoginUser
                {
                    Login = login,
                    Password = password
                };
                var jsonContent = JsonConvert.SerializeObject(loginUser);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("User/UserLogin", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Неправильный логин или пароль");
                }
                var answerUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
                return answerUser;
            }
        }
        public async Task<UserDTO> Classic(string color, decimal stake, UserDTO user, string winColor)
        {
                var classicWheel = new ClassicWheel
                {
                    Stake = stake,
                    Color = color,
                    User = user,
                    WinColor = winColor
                };
                var jsonContent = JsonConvert.SerializeObject(classicWheel);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync("Roulette/Classic", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Что-то пошло не так");
                }
                var answerUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
                return answerUser;
        }
        public async Task<UserDTO> Wheel(decimal stake, UserDTO user, string thing)
        {
            var wheeel = new Wheeel
            {
                Stake = stake,
                User = user,
                Thing = thing
            };
            var jsonContent = JsonConvert.SerializeObject(wheeel);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync("Roulette/Wheel", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Что-то пошло не так");
            }
            var answerUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
            return answerUser;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            try
            {
                var response = await httpClient.GetAsync("User/GetUser");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserDTO>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static Client instance = new();
        public static Client Instance
        {
            get
            {
                if (instance == null)
                    instance = new Client();
                return instance;
            }
        }
    }
}
