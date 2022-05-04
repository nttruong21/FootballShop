using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class AccountDAO
    {
        private FootballEntities db;
        public AccountDAO()
        {
            db = new FootballEntities();
        }

        public bool login(string email, string password)
        {
            string passwordToHash = Common.MD5Hash(password);
            var account = db.Accounts.FirstOrDefault(x => x.email.Equals(email) && x.password.Equals(passwordToHash));
            if(account == null)
            {
                return false;
            }
            return true;
        }

        // Get all accounts
        public List<Account> getAllAccount()
        {
            return db.Accounts.ToList();
        }

        // Get one account by id
        public Account getAccountById(int id)
        {
            return db.Accounts.FirstOrDefault(acc => acc.id == id);
        }

        // Get one account by email
        public Account getAccountByEmail(string email)
        {
            return db.Accounts.FirstOrDefault(acc => acc.email.Trim() == email.Trim());
        }


        // Create 
        public bool CreateAccount(Account account)
        {
            try
            {
                account.password = Common.MD5Hash(account.password);
                db.Accounts.Add(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update
        public bool UpdateAccount(Account account)
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete
        public bool DeleteAccount(Account account)
        {
            try
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}   
