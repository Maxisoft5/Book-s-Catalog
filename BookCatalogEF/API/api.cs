using API.DAL.EF.Repositories;
using System;
namespace ConsoleApp5
{
     class Api
    {
        static BaseRepository baseBooRep;
        static void Main(string[] args)
        {
            baseBooRep = new BaseRepository();
            Console.WriteLine("\t\t\t<<Books' catalog>>");
            Console.WriteLine(new string('-', 70));
            while (true)
            {
                Console.WriteLine("Enter 1 - To get book catalog, 2 - To Insert operation, 3 - Delete operation, 4 - Update operation");
                string ch = Console.ReadLine();
                char[] charArr = ch.ToCharArray();
                char key = charArr[0];
                if (Char.IsDigit(key))
                {
                    switch (charArr[0])
                    {
                        case '1':
                            baseBooRep.Read();
                            break;
                        case '2':
                            baseBooRep.Insert();
                            break;
                        case '3':
                            baseBooRep.Delete();
                            break;
                        case '4':
                            baseBooRep.Update();
                            break;
                    }
                }
            }
        }
    }
}
