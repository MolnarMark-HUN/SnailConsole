using System.Threading.Tasks;

namespace SnailConsole
{
    internal class Program
    {
       static ServerConnection connection = new ServerConnection("http://127.1.1.1:3000");
         static  List<Snail> all=new List<Snail>();
        static async Task Main(string[] args)
        {
            all=await connection.ListAllSnail();
            string command = "notLoggedIn";
            while (command.ToLower()!="exit") {
                   command=WriteMenu(command);
            
            }
        }

        private static string WriteMenu(string command)
        {
                if (command.ToLower() == "notLoggedIn")
                {

                }
        }

        private static void WriteNotLoggedIn(string command)
        {
            throw new NotImplementedException();
        }
    }
}
