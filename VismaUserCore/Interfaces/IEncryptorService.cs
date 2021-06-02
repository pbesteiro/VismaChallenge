namespace VismaUserCore.Interfaces
{
    /// <summary>
    /// Encryptor Service Interface
    /// </summary>
    public interface IEncryptorService
    {
        /// <summary>
        /// create Salt
        /// </summary>
        /// <returns></returns>
        string CreateSalt();

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Encrypt(string value);

        /// <summary>
        /// Verify
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <param name="unencryptedValue"></param>
        /// <returns></returns>
        bool Verify(string encryptedValue, string unencryptedValue);

        /// <summary>
        /// Generate MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string GenerateMd5(string value);
    }
}
