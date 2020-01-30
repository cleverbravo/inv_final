using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace UHFDemo
{
   internal class encriptacion
    {
        public static string encriptar(string laCadena)
        {
            SHA1CryptoServiceProvider elProveedor = new SHA1CryptoServiceProvider();
            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(laCadena);
            byte[] inArray = elProveedor.ComputeHash(vectoBytes);
            elProveedor.Clear();
            return Convert.ToBase64String(inArray);
        }
    }
}
