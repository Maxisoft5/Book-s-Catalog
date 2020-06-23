using DAL.EF;
using DAL.Enteties;
using DAL.Repositories.Interfaces;
using Devart.Data.SQLite;
using NLog;
using System;

namespace DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository<Author>
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        ConnectedLayer layer;
        public AuthorRepository(BookCatalogContext context)
        {

        }
        public AuthorRepository()
        {
            layer = new ConnectedLayer();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            string query = $"SELECT * FROM Author";
            using (SQLiteConnection conn = new SQLiteConnection(layer.connectionString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = query;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // printing the column names
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.Write(Environment.NewLine);
                        while (reader.Read())
                        {
                            // printing the table content
                            for (int i = 0; i < reader.FieldCount; i++)
                                Console.Write(reader.GetValue(i).ToString() + "\t");
                            Console.Write(Environment.NewLine);
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (SQLiteException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (Exception ex)
                {
                    logger.Trace($"{ex.Message}.Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"{ex.Message}.Error encountered during READING operation. See details in log file");
                }
                finally
                {
                    logger.Trace($"READING successfully completed at {DateTime.Now}");
                    conn.Close();
                }
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
