using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace IntroSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IntroSQL!");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var exitLoop = false;
            var whichTable = "";
            var userInput = "";

            do
            {
                Console.Write("Which table do you want to work with? (C)ategories, (D)epartment, (E)mployess, (P)roduct, (R)eviews, (S)ales, (Q)uit ");
                whichTable = Console.ReadLine().ToLower();

                if (whichTable == "q")
                {
                    exitLoop = true;
                }
                else
                {

                    Console.Write("What would you like to do with the table? (L)ist (A)dd (U)pdate (D)elete ");
                    userInput = Console.ReadLine().ToLower();

                    switch (userInput)
                    {
                        case "l":
                            // Create a new instance of your ListTable class
                            var listTable = new ListTable();
                            listTable.ListTableContents(conn, whichTable);
                            break;
                        case "a":
                            var addToTable = new AddToTable();
                            addToTable.AddToTableContents(conn, whichTable);
                            break;
                        case "u":
                            var updateTable = new UpdateTable();
                            updateTable.UpdateTableContents(conn, whichTable);
                            break;
                        case "d":
                            var deleteTable = new DeleteFromTable();
                            deleteTable.DeleteFromTableContents(conn, whichTable);
                            break;
                        default:
                            Console.WriteLine("That is not a valid input. No action will be taken.");
                            break;
                    }
                }
            }
            while (!exitLoop);

            //Console.Write("What departement would you like to add? ");
            //var newDepartment = Console.ReadLine();
            //repo.InsertDepartment(newDepartment);
        }
    }
}

