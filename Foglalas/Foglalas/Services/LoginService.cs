using Foglalas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foglalas.Services
{
    public class LoginService : ILoginService
    {
        public async Task<User> Login(string email, string password)
        {
            try
            {
                if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var userInfo = new List<User>();
                    var client = new HttpClient();
                    string url = "http://localhost:8099/api/user/Login/"+email+ "/"+password;
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync("");
                    if(response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        userInfo = JsonConvert.DeserializeObject<List<User>>(json);
                        return await Task.FromResult(userInfo.FirstOrDefault());
                    }
                    else
                    {
                        return null;
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
