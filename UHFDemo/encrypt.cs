using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace UHFDemo
{
    class encrypt
    {
        public static string GetSHA256(string laCadena)
        {
            SHA1CryptoServiceProvider elProveedor = new SHA1CryptoServiceProvider();
            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(laCadena);
            byte[] inArray = elProveedor.ComputeHash(vectoBytes);
            elProveedor.Clear();
            return Convert.ToBase64String(inArray);
        }
    }
}
