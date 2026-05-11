using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnailConsole
{
    public class ServerConnection
    {
        HttpClient client=new HttpClient();
        public ServerConnection(string url) { 
        client.BaseAddress = new Uri(url);
        }
        public async Task<bool> Login(string username, string password)
        {
            try
            {
                User newUser=new User(username,password);
                string jsonString=JsonSerializer.Serialize(newUser);
                StringContent sendThis=new StringContent(jsonString,Encoding.UTF8,"Application/JSON");
                HttpResponseMessage res = await client.PostAsync("/login",sendThis);
                res.EnsureSuccessStatusCode();
                string responsestring=await res.Content.ReadAsStringAsync();
                Message result=JsonSerializer.Deserialize<Message>(responsestring);
                client.DefaultRequestHeaders.Add("authorization",$"Bearer {result.token}");
                return true;

            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<bool> createSnail(string snailName, string species,int shellDiameter,string habitat)
        {
            try
            {
                Snail newSnail = new Snail( snailName,  species,  shellDiameter,  habitat);
                string jsonString = JsonSerializer.Serialize(newSnail);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage res = await client.PostAsync("/snail", sendThis);
                res.EnsureSuccessStatusCode();
                string responsestring = await res.Content.ReadAsStringAsync();
                Message result = JsonSerializer.Deserialize<Message>(responsestring);
                client.DefaultRequestHeaders.Add("authorization", $"Bearer {result.token}");
                return true;

            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return false;
            }
        }
        public async Task<List<Snail>> ListAllSnail()
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync("/snails");
                res.EnsureSuccessStatusCode();
                string responsestring = await res.Content.ReadAsStringAsync();
                List<Snail> result = JsonSerializer.Deserialize<List<Snail>>(responsestring);
                return result;

            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return null;
            }
        }
        public async Task<List<Snail>> MyListSnails()
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync("/mysnails");
                res.EnsureSuccessStatusCode();
                string responsestring = await res.Content.ReadAsStringAsync();
                List<Snail> result = JsonSerializer.Deserialize<List<Snail>>(responsestring);
                return result;

            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return null;
            }
        }
        public void Logout()
        {
            client.DefaultRequestHeaders.Clear();
        }
    }
}
