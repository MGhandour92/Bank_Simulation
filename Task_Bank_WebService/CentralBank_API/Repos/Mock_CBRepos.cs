using Account_API.Interfaces;
using Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account_API.Repos
{
    public class Mock_CBRepo_Accounts : ICBRespository<Account>
    {
        private readonly string _fullPath;
        public Mock_CBRepo_Accounts(string rootPath)
        {
            _fullPath = Path.Combine(rootPath, "Accounts_File.json");
        }

        /// <summary>
        /// Read the file of all accounts
        /// </summary>
        /// <returns>List of accounts</returns>
        public IQueryable<Account> ReadFile()
        {
            if (Helpers.CheckExistFile(_fullPath))
            {
                // Open the file to read from.
                string fileData = File.ReadAllText(_fullPath);
                //Deserialize to list of LogData
                return JsonConvert.DeserializeObject<IQueryable<Account>>(fileData);
            }
            else
                //create an empty file
                Helpers.CreateFile(_fullPath);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log">Data</param>
        /// <param name="action">Create - Update - Delete</param>
        /// <param name="response"></param>
        public void WriteToFile(Account log, string action, out string response)
        {
            response = "Success";

            //get all accounts
            var allAccount = ReadFile().ToList();

            var originalAcc = allAccount.FirstOrDefault(acc => acc.Id == log.Id);

            switch (action.ToLower())
            {
                case "create":
                    //add the new account
                    allAccount.Add(log);
                    break;

                case "update":
                    if (originalAcc == null)
                        response = "Account Not Found";

                    //remove the original account
                    allAccount.Remove(originalAcc);
                    //add the new account
                    allAccount.Add(log);
                    break;

                case "delete":
                    if (originalAcc == null)
                        response = "Account Not Found";

                    //remove the original account
                    allAccount.Remove(originalAcc);
                    //modify it
                    originalAcc.IsDeleted = true;
                    //add it again
                    allAccount.Add(originalAcc);
                    break;
            }

            //save data
            //build json string
            var jsonData = JsonConvert.SerializeObject(allAccount);

            //create an empty file To replace the old one
            Helpers.CreateFile(_fullPath);
            //save new list
            Helpers.WriteFile(_fullPath, jsonData);
        }
    }

    public class Mock_CBRepo_Transactions : ICBRespository<Transaction>
    {
        private readonly string _fullPath;
        public Mock_CBRepo_Transactions(string rootPath)
        {
            _fullPath = Path.Combine(rootPath, "Accounts_Transactions.json");
        }

        /// <summary>
        /// Read the file of all transactions
        /// </summary>
        /// <returns>List of transactions</returns>
        public IQueryable<Transaction> ReadFile()
        {
            if (Helpers.CheckExistFile(_fullPath))
            {
                // Open the file to read from.
                string fileData = File.ReadAllText(_fullPath);
                //Deserialize to list of LogData
                return JsonConvert.DeserializeObject<IQueryable<Transaction>>(fileData);
            }
            else
                //create an empty file
                Helpers.CreateFile(_fullPath);
            return null;
        }

        public void WriteToFile(Transaction log, string action, out string response)
        {
            response = "Success";

            //get all transactions
            var allTransaction = ReadFile().ToList();

            //Add the new transaction
            allTransaction.Add(log);            

            //save data
            //build json string
            var jsonData = JsonConvert.SerializeObject(allTransaction);

            //create an empty file To replace the old one
            Helpers.CreateFile(_fullPath);
            //save new list
            Helpers.WriteFile(_fullPath, jsonData);
        }
    }

    internal static class Helpers
    {
        /// <summary>
        /// Check if the file Exists
        /// </summary>
        /// <param name="path_name">Path + File Name</param>
        public static bool CheckExistFile(string path_name) => File.Exists(path_name);

        /// <summary>
        /// Create a new file
        /// </summary>
        /// <param name="path_name">Path + File Name</param>
        public static void CreateFile(string path_name)
        {
            //dispose because create leaves the file open
            File.Create(path_name).Dispose();
        }

        /// <summary>
        /// Write to a file
        /// </summary>
        /// <param name="path_name">Path + File Name</param>
        /// <param name="data">data to be written</param>
        public static void WriteFile(string path_name, string data)
        {
            File.WriteAllText(path_name, data); ;
        }
    }
}
