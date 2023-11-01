// Program.cs
//
// This file is integrated part of "Lazy Vinke DbCheck" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 01

using System;
using System.IO;
using System.Reflection;

using Lazy.Vinke;
using Lazy.Vinke.Database;

namespace Lazy.Vinke.DbCheck
{
    public static class Program
    {
        [STAThread()]
        public static void Main(String[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += LazyAssemblyResolver.Resolve;

            Console.WriteLine();
            Console.WriteLine(" - Which database would you like to connect?");
            Console.WriteLine();
            Console.WriteLine("\t1. MySql");
            Console.WriteLine("\t2. Oracle");
            Console.WriteLine("\t3. Postgre");
            Console.WriteLine("\t4. SqlServer");
            Console.WriteLine("\t0. Give up!");
            Console.WriteLine();

            Console.Write("\tYour option is: ");
            Char ch = (Char)Console.Read();
            Console.WriteLine();

            switch (ch)
            {
                case '1': ConnectOnMySql(); break;
                case '2': ConnectOnOracle(); break;
                case '3': ConnectOnPostgre(); break;
                case '4': ConnectOnSqlServer(); break;
                default:
                    Console.WriteLine("\tGood bye!");
                    Console.WriteLine();
                    break;
            }
        }

        private static void ConnectOnMySql()
        {
            String connectionString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", "Lazy.Vinke.DbCheck.Connection.MySql.txt"));
            TryConnect("Lazy.Vinke.Database.MySql.dll", "Lazy.Vinke.Database.MySql.LazyDatabaseMySql", connectionString, "MySql");
        }

        private static void ConnectOnOracle()
        {
            String connectionString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", "Lazy.Vinke.DbCheck.Connection.Oracle.txt"));
            TryConnect("Lazy.Vinke.Database.Oracle.dll", "Lazy.Vinke.Database.Oracle.LazyDatabaseOracle", connectionString, "Oracle");
        }

        private static void ConnectOnPostgre()
        {
            String connectionString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", "Lazy.Vinke.DbCheck.Connection.Postgre.txt"));
            TryConnect("Lazy.Vinke.Database.Postgre.dll", "Lazy.Vinke.Database.Postgre.LazyDatabasePostgre", connectionString, "Postgre");
        }

        private static void ConnectOnSqlServer()
        {
            String connectionString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", "Lazy.Vinke.DbCheck.Connection.SqlServer.txt"));
            TryConnect("Lazy.Vinke.Database.SqlServer.dll", "Lazy.Vinke.Database.SqlServer.LazyDatabaseSqlServer", connectionString, "SqlServer");
        }

        private static void TryConnect(String assemblyName, String classFullName, String connectionString, String dbmsName)
        {
            try
            {
                Type type = Assembly.Load(assemblyName).GetType(classFullName);
                LazyDatabase database = (LazyDatabase)Activator.CreateInstance(type, new Object[] { connectionString, null });

                database.OpenConnection();
                database.CloseConnection();

                Console.WriteLine(String.Format("\tConnected successfully on {0}!", dbmsName));
                Console.WriteLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine(String.Format("\tUnable to connect on {0}!", dbmsName));
                Console.WriteLine();
                Console.WriteLine(exp.GetBaseException().Message);
                Console.WriteLine();
            }
        }
    }
}
