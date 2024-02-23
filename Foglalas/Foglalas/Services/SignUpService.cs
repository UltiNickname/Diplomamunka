using Foglalas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    internal class SignUpService : ISignUpService
    {
        public async Task<string> SignUp(User user)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var userInfo = new List<User>();
                    var client = new HttpClient();
                    string url = "http://192.168.0.80:8099/api/user/AddNewUser";
                    client.BaseAddress = new Uri(url);
                    var body = JsonConvert.SerializeObject(user);
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, user);
                    if (response.IsSuccessStatusCode)
                    {
                        return "User created!";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
