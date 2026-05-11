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
                int number = 0;
                if (command.ToLower() == "notloggedin")
                {
                    number = WriteMenu();
                    command = await DoStg(number, command);
                }
                else {
                
                number=WriteLoggedInMenu();
                    command = await DoStg(number, command);
                }
            Console.ReadKey();
            }
        }

        private static async Task<string> DoStg(int number, string command)
        {
            if (command.ToLower()=="notloggedin")
            {
                switch (number)
                {
                    case 1:
                        ListSnails();
                        break;
                    case 2:
                        FilterSnails();
                        break;
                    case 3:
                        SearchName();
                        break;
                    case 4:
                        GroupSnails();
                        break;
                    case 5:
                       await Login();
                        command = "loggedin";
                        break;
                    case 6:
                        return "Exit";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (number)
                {
                    case 1:
                        await ListMySnails();
                        break;
                    case 2:
                        await CreateSnail();
                        break;
                    case 3:
                        return "notloggedin";
                    default:
                        break;
                }
            }
            return command;
        }

        private static async Task CreateSnail()
        {
            Console.WriteLine("Add meg a csiga neve fajtája shelldiametert meg a habitatot");
           string snailName= Console.ReadLine().Trim();
            Console.WriteLine("fajtája");
            string species = Console.ReadLine().Trim();
            
            Console.WriteLine("elohelye");
            string habitat = Console.ReadLine().Trim();
            Console.WriteLine("ház");
            if (double.TryParse(Console.ReadLine().Trim(), out double number))
            {
                if (number < 0) { Console.WriteLine("Negatív szám :/"); return; }
                await connection.createSnail(snailName, species, habitat,number);

            }
            else
            {
                Console.WriteLine("Nem számot adtál meg");

            }


        }

        private static async Task ListMySnails()
        {
           List<Snail> mysnails=await connection.MyListSnails();
            mysnails.ForEach(snail => Console.WriteLine(snail));
        }

        private static async Task<bool>  Login()
        {
            Console.WriteLine("Add meg a felhasználónevedet!");
            string username = Console.ReadLine().Trim();
            Console.WriteLine("Add meg a jelszavad");
            string password = Console.ReadLine().Trim();
            return await connection.Login(username,password);
        }

        private static void GroupSnails()
        {
            int under20=all.Where(snail=>snail.shellDiameter<20).Count();
            int uder50andover20 = all.Where(snail => snail.shellDiameter > 20&&snail.shellDiameter<50).Count();
            int over50= all.Where(snail => snail.shellDiameter > 50).Count();
            Console.WriteLine($"20 mm alatt {under20}");
            Console.WriteLine($"20 és 50 kozott {uder50andover20}");
            Console.WriteLine($"50 felett {over50}");
        }

        private static void SearchName()
        {
            string name = Console.ReadLine().Trim();
           List<Snail> search= all.Where(snail=>snail.snailName.Contains(name)).ToList();
            if (search.Count < 1) return;
            search.ForEach(snail => Console.WriteLine(snail));
        }

        private static void FilterSnails()
        {
            Console.WriteLine("Add meg a minimum csigahaz atmerojet!");
            if (double.TryParse(Console.ReadLine().Trim(), out double number))
            {
                if (number < 0) { Console.WriteLine("Negatív szám :/"); return; };
               List<Snail> filtered= all.Where(snail => snail.shellDiameter > number).ToList();
                if (filtered.Count < 0) return;
                filtered.ForEach(snail => Console.WriteLine(snail));
            }
            else {
                Console.WriteLine("Nem számot adtál meg");
            
            }
            
        }

        private static void ListSnails()
        {
            if (all.Count < 1) return;
            all.ForEach(snail => Console.WriteLine(snail));
        }

        private static int WriteMenu()
        {
            Console.WriteLine("1.Csigák listázása");
            Console.WriteLine("2. Szűrés megadott hárztető szerint");
            Console.WriteLine("3.Név szerint");
            Console.WriteLine("4.Csigák csoportositasa hazteto");
            Console.WriteLine("5.Bejelentkezés login");
            Console.WriteLine("6. kilépés");
            Console.WriteLine("");
            return GetNumber(1, 6);
        }
        private static int WriteLoggedInMenu()
        {
            Console.WriteLine("1. Saját Csigák listázása");
            Console.WriteLine("2. Új csiga");
            Console.WriteLine("3.kijelentkezés");
            Console.WriteLine("");
            return GetNumber(1, 3);
        }
        static int GetNumber(int min, int max)
        {
            Console.WriteLine($"Add me a szamot a {min} és {max} között");
            if (int.TryParse(Console.ReadLine().Trim(), out int number))
            {
                if (number >= min && number <= max)
                {
                    return number;
                    Console.WriteLine("A határ értékeken kívülre esett! Nigger");

                }
            }
            else
            {
                Console.WriteLine("Számot kell megadni");
                return GetNumber(min, max);
            }
        }

        private static void WriteNotLoggedIn(string command)
        {
            throw new NotImplementedException();
        }
    }
}
