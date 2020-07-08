using System;
using DAL.Repositories;


namespace DevArtTask
{
    internal class API
    {
        static BookRepository bookRep;
        static void Main(string[] args)
        {
            bookRep = new BookRepository();
            Console.WriteLine("\t\t\t<<Books' catalog>>");
            Console.WriteLine(new string('-',70));
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
                            bookRep.Read();
                            break;
                        case '2':
                            bookRep.Insert();
                            break;
                        case '3':
                            bookRep.Delete();
                            break;
                        case '4':
                            bookRep.Update();
                            break;
                    }
                }
            }
        }
    }
}
