using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace FootballShopModel
{
    public class Common
    {
        public static string MD5Hash(string text)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(text);

            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            string hashString = "";
            for(int i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }
            return hashString.PadLeft(32, '0');
        }

        public SelectList ToSelectList(DataTable table, string key, string value)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[key].ToString(),
                    Value = row[value].ToString(),
                });
            }
            return new SelectList(list, "Key", "Value");
        }
    }
}
