using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class HashHelper
    {
        private static HashAlgorithm algo = SHA512.Create();

        public static string CreateHash(string text)
        {
            byte[] input = Encoding.Default.GetBytes(text); // String => Bytes[]
            byte[] output = algo.ComputeHash(input);
            string ret = Convert.ToBase64String(output);
            return ret;
        }
    }
}