using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Utilities
{
    public class PasswordUtility
    {
        public static string HashPassword(string password)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] passwordBytes = encoding.GetBytes(password);
            var sha = new SHA512Managed();
            byte[] hashed = sha.ComputeHash(passwordBytes);
            var hashedPasswordString = "";
            foreach (var b in hashed)
                hashedPasswordString += String.Format("{0:x2}", b);
            return hashedPasswordString;
        }
    }
}
