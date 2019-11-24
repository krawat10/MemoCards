using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoCards.Models;

namespace MemoCards.ExtensionMethods
{
    public static class CryptoExtensionMethods
    {
        public static Password CreatePasswordHashAndSalt(this string password)
        {
            if(string.IsNullOrEmpty(password)) throw new ArgumentException("Password is empty");

            using var encryptor = new System.Security.Cryptography.HMACSHA512();
            return new Password
            {
                Salt = encryptor.Key,
                Hash = encryptor.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            };
        }

        public static bool VerifyPassword(this string password, Password hashedPasswordAndSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password is empty");

            using (var decryptor = new System.Security.Cryptography.HMACSHA512(hashedPasswordAndSalt.Salt))
            {
                var currentHash = decryptor.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return currentHash.IsEqualWith(hashedPasswordAndSalt.Hash);
            }
        }

        private static bool IsEqualWith(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i]) return false;
            }

            return true;
        }
    }
}
