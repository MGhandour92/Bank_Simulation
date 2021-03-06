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
        public IEnumerable<Account> ReadFile()
        {
            if (Helpers.CheckExistFile(_fullPath))
            {
                // Open the file to read from.
                string fileData = File.ReadAllText(_fullPath);
                //Deserialize to list of LogData
                return JsonConvert.DeserializeObject<IEnumerable<Account>>(fileData);
            }
            else
                //create an empty file
                Helpers.CreateFile(_fullPath);
            return Enumerable.Empty<Account>().AsQueryable();
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
            var allAccount = ReadFile();
            var acountsList = new List<Account>();
            var originalAcc = new Account();

            if (allAccount is not null)
            {
                acountsList = allAccount.ToList();
                originalAcc = allAccount.FirstOrDefault(acc => acc.Id == log.Id);
            }

            switch (action.ToLower())
                {
                    case "create":
                        //add the new account
                        acountsList.Add(log);
                        break;

                    case "update":
                        if (originalAcc == null)
                            response = "Account Not Found";

                        //remove the original account
                        acountsList.Remove(originalAcc);
                        //add the new account
                        acountsList.Add(log);
                        break;

                    case "delete":
                        if (originalAcc == null)
                            response = "Account Not Found";

                        //remove the original account
                        acountsList.Remove(originalAcc);
                        //modify it
                        originalAcc.IsDeleted = true;
                        //add it again
                        acountsList.Add(originalAcc);
                        break;
                }

                //save data
                //build json string
                var jsonData = JsonConvert.SerializeObject(acountsList);

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
        public IEnumerable<Transaction> ReadFile()
        {
            if (Helpers.CheckExistFile(_fullPath))
            {
                // Open the file to read from.
                string fileData = File.ReadAllText(_fullPath);
                //Deserialize to list of LogData
                return JsonConvert.DeserializeObject<IEnumerable<Transaction>>(fileData);
            }
            else
                //create an empty file
                Helpers.CreateFile(_fullPath);
            return Enumerable.Empty<Transaction>().AsQueryable();
        }

        public void WriteToFile(Transaction log, string action, out string response)
        {
            response = "Success";

            //get all transactions
            var allTransaction = ReadFile();
            var trxList = new List<Transaction>();

            if (allTransaction is not null)
            {
                trxList = allTransaction.ToList();
            }

            //Add the new transaction
            trxList.Add(log);            

            //save data
            //build json string
            var jsonData = JsonConvert.SerializeObject(trxList);

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
