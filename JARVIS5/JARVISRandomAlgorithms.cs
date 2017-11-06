﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public static class JARVISRandomAlgorithms
    {
        public static string RandomAlgorithmOutputPath = @"C:\JARVIS5\RandomAlgorithmOutput";
        public static StatusObject BuildUUIDTable()
        {
            StatusObject SO = new StatusObject();
            try
            {
                JARVISDataSource newDataSource = new JARVISDataSource("sql2008kl", "shawn_db", "sa", "password");
                Guid.NewGuid();
            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject BuildHashTable()
        {
            StatusObject SO = new StatusObject();
            try
            {
                // Drop table RainbowTable
                // Create table RainbowTable
                // Dictionary words
                // Take l33t5p34k into consideration
            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject BuildStringPermutationTable(string WordLength, JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
                var q = alphabet.Select(x => x.ToString());
                int size = Convert.ToInt32(WordLength);
                for (int i = 0; i < size - 1; i++)
                    q = q.SelectMany(x => alphabet, (x, y) => x + y);
                
                foreach (var item in q)
                {
                    string insertQuery = String.Format("insert into Dictonary values ('{0}','{1}',{2})", item, item[0], item.Length);
                    DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                    Console.WriteLine(item);
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }

        public static StatusObject BuildStringPermutationTable(string WordLength, char FirstLetter, JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                string TableQuery =
                    "CREATE TABLE [dbo].[RAINBOW_{0}](" +
                        "[ID] [bigint] NOT NULL," +
                        "[Word] [varchar](255) NOT NULL," +
                        "[FirstLetter] [varchar](1) NOT NULL," +
                        "[LetterCount] [int] NOT NULL," +
                     "CONSTRAINT [PK_RAINBOW_{0}] PRIMARY KEY CLUSTERED " +
                    "(" +
                        "[ID] ASC" +
                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                    ") ON [PRIMARY]";
                string TargetString = "";
                for (int smallAlphabet = 97; smallAlphabet <= 122; smallAlphabet++)
                {
                    TargetString += (char)smallAlphabet;
                }
                for (int Numeric = 48; Numeric <= 57; Numeric++)
                {
                    TargetString += (char)Numeric;
                }
                for(int Symbol = 32; Symbol <=47; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 58; Symbol <= 64; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 91; Symbol <= 96; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 123; Symbol <= 126; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                //Create Tables
                foreach(char Character in TargetString)
                {
                    StatusObject SO_CreateTable = DictionaryStorage.ExecuteNonReaderQuery(String.Format(TableQuery, (int)Character));
                    Console.WriteLine("Creating Table RAINBOW_{0}", (int)Character);
                    if (SO_CreateTable.Status == StatusCode.FAILURE)
                    {
                        Console.WriteLine(SO_CreateTable.ErrorMessage);
                    }
                }
                var q = TargetString.Select(x => x.ToString());
                int size = Convert.ToInt32(WordLength);
                for (int i = 0; i < size - 1; i++)
                    q = q.SelectMany(x => TargetString, (x, y) => x + y);

                foreach (var item in q)
                {
                    string insertQuery = String.Format("insert into RAINBOW_{3} values ('{0}','{1}',{2})", item, item[0], item.Length, (int)item[0]);
                    Console.WriteLine(item);
                    StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                    if (SO_AddRecord.Status == StatusCode.FAILURE)
                    {
                        Console.WriteLine(SO_AddRecord.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }
        public static void DoSomething()
        {
            try
            {
                Directory.CreateDirectory(RandomAlgorithmOutputPath);
                StreamWriter TestFile = new StreamWriter(String.Format(@"{0}\DoSomething.txt", RandomAlgorithmOutputPath), append: true);
                for(int i = 0; i < 1000; i++)
                {
                    TestFile.WriteLine(i);
                }
                TestFile.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
