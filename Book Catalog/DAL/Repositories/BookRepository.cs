﻿using DAL.Enteties;
using DAL.Repositories.Interfaces;
using Devart.Data.SQLite;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class BookRepository : IBookRepository<Book>
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ConnectedLayer layer;

        public BookRepository()
        {
            layer = new ConnectedLayer();
        }
        public void Delete()
        {
            bool IsDigit = true;
            while (IsDigit)
            {
                Console.WriteLine("Enter a book's id");
                string id = Console.ReadLine();
                id.ToCharArray();
                if (Char.IsDigit(id.First()))
                {
                    string query = $"DELETE FROM Book WHERE id = {id}; DELETE FROM BookAuthor WHERE id = {id}";
                    using (SQLiteConnection conn = new SQLiteConnection(layer.connectionString))
                    {
                        int aff = 0;
                        conn.Open();
                        SQLiteCommand command = conn.CreateCommand();
                        try
                        {
                            aff = command.ExecuteNonQuery();
                            command.CommandText = query;
                            command.ExecuteNonQuery();

                        }
                        catch (NullReferenceException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during DELETE operation. See details in log file");
                        }
                        catch (SQLiteException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during DELETE operation. See details in log file");
                        }
                        catch (Exception ex)
                        {
                            logger.Trace($"{ex.Message}.Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"{ex.Message}.Error encountered during DELETE operation. See details in log file");
                        }
                        finally
                        {
                            logger.Trace($"DELETE operation successfully completed at {DateTime.Now};{aff} rows were affected");
                            command.Dispose();
                            conn.Close();
                            IsDigit = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Enter an id");
                }
            }
        }
        public void Insert()
        {
            List<string> bookValues = new List<string>();
            string firstname = "";
            string lastname = "";
            StringBuilder sbFirstname = new StringBuilder(firstname);
            StringBuilder sbLastname = new StringBuilder(lastname);
            bool isNotselected = true;
            while (isNotselected)
            {
                Console.WriteLine("Choose books' author: 1 - Nassim Taleb, 2 - Gerbert Shield, 3 - Alexandr Pushkin, 4 - Book doesn't have author");
                string ch = Console.ReadLine();
                char[] charArr = ch.ToCharArray();
                char key = charArr[0];
                if (Char.IsDigit(key))
                {
                    switch (charArr[0])
                    {
                        case '1':
                            sbFirstname.Append("Nassim");
                            sbLastname.Append("Taleb");
                            isNotselected = false;
                            break;
                        case '2':
                            sbFirstname.Append("Gerbert");
                            sbLastname.Append("Shield");
                            isNotselected = false;
                            break;
                        case '3':
                            sbFirstname.Append("Alexandr");
                            sbLastname.Append("Pushkin");
                            isNotselected = false;
                            break;
                        case '4':
                            isNotselected = false;
                            break;
                    }
                }
            }
            Console.WriteLine("Enter a books's name");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter a books's count");
            string bookCount = Console.ReadLine();
            Console.WriteLine("Enter a books's price");
            string price = Console.ReadLine();
            Console.WriteLine("Enter a books's genre");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter a books's assessments");
            string assessments = Console.ReadLine();
            bookValues.AddRange(new List<string> { bookName, bookCount, price, genre, assessments });
            string lastBookindex = assessments;
            StringBuilder sbBook = new StringBuilder("");
            foreach (string val in bookValues)
            {
                if (val == lastBookindex)
                {
                    sbBook.Append($"'{val}'");
                }
                else
                {
                    sbBook.Append($"'{val}',");
                }
            }
            string query = "";
            string queryBookauthor = "";
            query = $"Insert Into Book (bookname,countinstance,price,genre,assessments) Values ({sbBook});";
            using (SQLiteConnection conn = new SQLiteConnection(layer.connectionString))
            {
                int aff = 0;
                conn.Open();
                // Start a local transaction
                SQLiteTransaction myTrans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                //Execute book insert 
                SQLiteCommand command = conn.CreateCommand();
                try
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    aff = command.ExecuteNonQuery();
                    //Get the last one inserted id  
                    command.CommandText = "SELECT last_insert_rowid();";
                    int lastID = Int32.Parse(command.ExecuteScalar().ToString());
                    //Checking has the book author
                    if (sbLastname.ToString() != "" && sbFirstname.ToString() != "")
                    {
                        queryBookauthor = $"Insert into BookAuthor (bookid, authorid) Values ('{lastID}','{lastID}')";
                    }
                    else
                    {
                        queryBookauthor = $"Insert into BookAuthor (bookid) Values ('{lastID}')";
                    }
                    command.CommandText = queryBookauthor;
                    command.ExecuteNonQuery();
                    myTrans.Commit();
                }
                catch (NullReferenceException ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}. Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during INSERT operation. See details in log file");
                }
                catch (SQLiteException ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}. Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during INSERT operation. See details in log file");
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}.Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"{ex.Message}.Error encountered during INSERT operation. See details in log file");
                }
                finally
                {
                    logger.Trace($"INSERT operation ended at {DateTime.Now};");
                    command.Dispose();
                    myTrans.Dispose();
                }
            }
        }
        public void Read()
        {
            //Get books without authors
            string queryWithoutauthors = $"Select bookname,countinstance, price, genre, assessments From Book INNER JOIN BookAuthor on BookAuthor.bookid = Book.id WHERE BookAuthor.authorid IS NULL;";
            //Get books with authors
            string queryWithauthors = $"SELECT Author.firstname, Author.lastname, Book.bookname, Book.countinstance, Book.price, Book.genre, Book.assessments AS book FROM Author INNER JOIN BookAuthor ON Author.id = BookAuthor.authorid LEFT JOIN Book ON BookAuthor.bookid = Book.id;";

            using (SQLiteConnection conn = new SQLiteConnection(layer.connectionString))
            {
                conn.Open();
                // Start a local transaction
                SQLiteTransaction myTrans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                //Execute book insert 
                SQLiteCommand command = conn.CreateCommand();
                try
                {
                    command.CommandText = queryWithoutauthors;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // printing the column names 
                        Console.WriteLine("\t\tBooks without authors");
                        Console.Write(Environment.NewLine);
                        while (reader.Read())
                        {
                            // printing the table content
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(
                                    "{0}: {1}\n",
                                    reader.GetName(i).ToString(), reader.GetValue(i).ToString());

                            }
                            Console.WriteLine(new string('>', 40));
                            Console.Write(Environment.NewLine);
                        }
                        myTrans.Commit();
                        command.CommandText = queryWithauthors;
                    }
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // printing the column names 
                        Console.WriteLine("\t\tBooks with authors");
                        Console.Write(Environment.NewLine);
                        while (reader.Read())
                        {
                            // printing the table content
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(
                                    "{0}: {1}\n",
                                    reader.GetName(i).ToString(), reader.GetValue(i).ToString());

                            }
                            Console.WriteLine(new string('>', 40));
                            Console.Write(Environment.NewLine);
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (SQLiteException ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    logger.Trace($"{ex.Message}.Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"{ex.Message}.Error encountered during READING operation. See details in log file");
                }
                finally
                {
                    command.Dispose();
                    myTrans.Dispose();
                    logger.Trace($"READING operation ended at {DateTime.Now};");
                    conn.Close();
                }
            }
        }

        public void Update()
        {
            bool IsDigit = true;
            int aff = 0;
            while (IsDigit)
            {
                Console.WriteLine("Enter a book's id");
                string id = Console.ReadLine();
                id.ToCharArray();
                if (Char.IsDigit(id.First()))
                {
                    List<string> authorValues = new List<string>();
                    List<string> bookValues = new List<string>();
                    Console.WriteLine("Enter a author's firstname");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter a author's lastname");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Enter a author's middlename");
                    string middleName = Console.ReadLine();
                    Console.WriteLine("Enter a books's name");
                    string bookName = Console.ReadLine();
                    Console.WriteLine("Enter a books's count");
                    string bookCount = Console.ReadLine();
                    Console.WriteLine("Enter a books's price");
                    string price = Console.ReadLine();
                    Console.WriteLine("Enter a books's genre");
                    string genre = Console.ReadLine();
                    Console.WriteLine("Enter a books's assessments");
                    string assessments = Console.ReadLine();
                    authorValues.AddRange(new List<string> { firstName, lastName, middleName });
                    bookValues.AddRange(new List<string> { bookName, bookCount, price, genre, assessments });

                    string query = $"UPDATE Author SET firstname = '{firstName}', lastname = '{lastName}', middlename = '{middleName}' WHERE id = '{id}';" +
                        $"UPDATE Book SET bookname = '{bookName}', countinstance = '{bookCount}', price = '{price}', genre = '{genre}', assessments = '{assessments}' WHERE id = {id};";
                    using (SQLiteConnection conn = new SQLiteConnection(layer.connectionString))
                    {
                        conn.Open();
                        SQLiteCommand command = conn.CreateCommand();
                        try
                        {
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                            aff = command.ExecuteNonQuery();
                        }
                        catch (NullReferenceException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during UPDATE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during UPDATE operation. See details in log file");
                        }
                        catch (SQLiteException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during UPDATE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during UPDATE operation. See details in log file");
                        }
                        catch (Exception ex)
                        {
                            logger.Trace($"{ex.Message}.Error encountered during UPDATE operation. - {DateTime.Now}");
                            Console.WriteLine($"{ex.Message}.Error encountered during UPDATE operation. See details in log file");
                        }
                        finally
                        {
                            command.Dispose();
                            logger.Trace($"UPDATE operation ended at {DateTime.Now};");
                            conn.Close();
                            IsDigit = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Enter an id");
                }
            }
        }
    }
}
