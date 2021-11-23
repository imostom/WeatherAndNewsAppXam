using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;

namespace WeatherAppXam.Services
{
    public class CryptographyService
    {
        public static async Task<string> DoEncryption(string plainText)
        {
            var encText = "";
            try
            {
                var salt = await GenerateSalt();
                encText = CryptographyUtil.EncryptText(plainText, Constants.EncryptionPassword, salt);
                //CryptographyUtil.DecryptText(userInfo.Address, Constants.IndexerPassword, salt)
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return encText;
        }

        public static async Task<string> GenerateSalt()
        {
            var saltBytes = GetRandomBytes();
            var salt = Convert.ToBase64String(saltBytes);
            return salt;
        }

        private static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        private static int GetSaltLength()
        {
            return 50;
        }
    }
}
