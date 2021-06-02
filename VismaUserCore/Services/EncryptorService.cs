using System;
using System.Security.Cryptography;
using System.Text;
using VismaUserCore.Interfaces;

namespace VismaUserCore.Services
{
    public class EncryptorService : IEncryptorService
    {
        /// <summary>
        /// Create Salt
        /// </summary>
        /// <returns></returns>
        public string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[8];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            var sha512 = new SHA512Managed();
            var originalBytes = Encoding.Default.GetBytes(value);
            var encodedBytes = sha512.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        /// <summary>
        /// Verify
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <param name="unencryptedValue"></param>
        /// <returns></returns>
        public bool Verify(string encryptedValue, string unencryptedValue)
        {
            var sha512 = new SHA512Managed();
            var originalBytes = Encoding.Default.GetBytes(unencryptedValue);
            var encodedBytes = sha512.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Equals(encryptedValue);
        }

        /// <summary>
        /// Generate MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GenerateMd5(string value)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sbuilder = new StringBuilder();

            foreach (var item in data)
                sbuilder.Append(item.ToString("x2"));

            return sbuilder.ToString();
        }
    }
}
