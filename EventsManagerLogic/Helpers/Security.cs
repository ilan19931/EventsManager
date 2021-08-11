using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Helpers
{
    public class Security
    {
        public string GenerateAccessKey()
        {
            int length = 50;
            string accessKey = null;
            Random rand = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            Sql sql = new Sql();

            bool isValid = false;
            while (isValid == false)
            {
                // generate random word
                for (int i = 0; i < length; i++)
                {
                    stringBuilder.Append(rand.Next(33, 127));
                }

                accessKey = this.Sha1Hash(Sha1Hash(stringBuilder.ToString()));

                //check if accessKey is allready taken
                string query = $"SELECT id FROM loggedInAccounts WHERE accessKey = '{accessKey}'";
                DataRowCollection drc = sql.SelectQuery(query);
                if (drc.Count == 0)
                    isValid = true;
            }

            return accessKey;
        }

        public string Sha1Hash(string i_Password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(i_Password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
