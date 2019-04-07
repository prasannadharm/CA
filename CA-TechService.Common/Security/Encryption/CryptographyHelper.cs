
#region ProgramInformation
/*------------------------------------------------------------------------------------------------------------------------------
	Module & SubModule Name	: Common
    Package Name            : TFO.COMMON.Security
	Description	            : Helper class for Cryptography.
    Author(s)               : Krishna Mohan
    Date Created        	: Oct 11, 2010
------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Imports

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace CA_TechService.Common.Security.Encryption
{
    /// <summary>
    /// Facade for the supported cryptography algorithms.
    /// </summary>
    public class CryptographyHelper : ICryptographyHelper
    {
        private readonly int blockSize = 128;

        private static string rijndaelSecretKey = "FD655156B30812ECF9D558C7D7A5F102F0C0EE9295C7C3FBA1ECAF48E6BC0A07";
        private static string rijndaelInitVector = "0000000000000000";
        private static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        private static CryptographyHelper instance;
        private static string urlEncryptionKey = "#@$756850kxkdkamspskfoflmcossps0-eje0e-e4r4kf-34=3pdd=kfr-4-444";

        private ICryptoTransform m_rijndaelEncryptor = null; // Only one instance of the encryptor is needed.
        private ICryptoTransform m_rijndaelDecryptor = null; // Only one instance of the decryptor is needed.
        private HashAlgorithm m_keyHashAlgorithm = null;

        private MD5CryptoServiceProvider m_md5CryptoServiceProvider = null; // Only one instance of the encryptor is needed.

        public static CryptographyHelper Instance
        { 
            get 
            {
                if (instance == null)
                {
                    lock (typeof(CryptographyHelper))
                    {
                        if (instance == null)
                        {
                            CryptographyHelper.instance = new CryptographyHelper();
                        }
                    }
                }
                return instance;
            }
        }

        protected ICryptoTransform Encryptor
        {
            get
            {
                return m_rijndaelEncryptor;
            }
        }

        protected ICryptoTransform Decryptor
        {
            get
            {
                return m_rijndaelDecryptor;
            }
        }

        protected HashAlgorithm KeyHashAlgorithm
        {
            get 
            {
                return m_keyHashAlgorithm;
            }
        }

        protected CryptographyHelper()
        {
            RijndaelManaged rijndaelEngine = new RijndaelManaged();
            rijndaelEngine.Key = HexStringToBytes(rijndaelSecretKey);
            rijndaelEngine.IV = Encoding.ASCII.GetBytes(rijndaelInitVector);
            rijndaelEngine.Mode = CipherMode.ECB;
            rijndaelEngine.Padding = PaddingMode.PKCS7;
            rijndaelEngine.BlockSize = blockSize;

            m_rijndaelEncryptor = rijndaelEngine.CreateEncryptor();
            m_rijndaelDecryptor = rijndaelEngine.CreateDecryptor();
            m_keyHashAlgorithm = new SHA512Managed();

            m_md5CryptoServiceProvider = new MD5CryptoServiceProvider(); 
        }


        /// <summary>
        /// Encrypt the given decrypted byte array using the current encryption algorithm.
        /// </summary>
        /// <param name="plainBytes">An unencrypted byte array</param>
        /// <returns>An encrypted <code>String</code></returns>
        /// <remarks>
        /// If plainBytes is null then null is returned. If plainBytes is
        /// empty then an empty string is returned.
        /// </remarks>
        public string Encrypt(byte[] plainBytes)
        {
            return (plainBytes != null && plainBytes.Length > 0) ?
                InternalEncrypt(plainBytes) : (plainBytes != null) ? string.Empty : null;
        }

        /// <summary>
        /// Encrypt the given decrypted <code>String</code> using the current encryption algorithm.
        /// </summary>
        /// <param name="plainText">An unencrypted <code>String</code></param>
        /// <returns>An encrypted <code>String</code></returns>
        /// <remarks>
        /// If plainText is null then null is returned. If plainText is
        /// empty then an empty string is returned.
        /// </remarks>
        public string Encrypt(string plainText)
        {
            return (plainText != null && plainText.Length > 0) ?
                InternalEncrypt(Encoding.ASCII.GetBytes(plainText)) : plainText;
        }

        /// <summary>
        /// Decrypt the given encrypted byte array using the current decryption algorithm.
        /// </summary>
        /// <param name="encryptedBytes">An encrypted byte array</param>
        /// <returns>The unencrypted <code>String</code></returns>
        /// <remarks>
        /// If encryptedBytes is null then null is returned. If encryptedBytes is
        /// empty then an empty string is returned.
        /// </remarks>
        public string Decrypt(byte[] encryptedBytes)
        {
            return (encryptedBytes != null && encryptedBytes.Length > 0) ?
                InternalDecrypt(encryptedBytes) : (encryptedBytes != null) ? string.Empty : null;
        }

        /// <summary>
        /// Decrypt the given encrypted <code>String</code> using the current decryption algorithm.
        /// </summary>
        /// <param name="encryptedText">An encrypted <code>String</code></param>
        /// <returns>The unencrypted <code>String</code></returns>
        /// <remarks>
        /// If encryptedText is null then null is returned. If encryptedText is
        /// empty then an empty string is returned.
        /// </remarks>
        public string Decrypt(string encryptedText)
        {
            return (encryptedText != null && encryptedText.Length > 0) ?
                InternalDecrypt(HexStringToBytes(encryptedText)) : encryptedText;
        }

        /// <summary>
        /// Generates a cryptographic hash for the given byte array.
        /// </summary>
        /// <param name="plainBytes">A byte array to hash</param>
        /// <returns>Hashed string value.</returns>
        /// <remarks>
        /// If plainBytes is null then null is returned. If plainBytes is
        /// empty then an empty string is returned.
        /// </remarks>
        public string CreateHash(byte[] plainBytes)
        {
            return (plainBytes != null && plainBytes.Length > 0) ?
                InternalHash(plainBytes) : (plainBytes != null) ? string.Empty : null;
        }

        /// <summary>
        /// Generates a cryptographic hash for the given <code>String</code>.
        /// </summary>
        /// <param name="plainText">A <code>String</code> to hash</param>
        /// <returns>Hashed string value.</returns>
        /// <remarks>
        /// If plainText is null then null is returned. If plainText is
        /// empty then an empty string is returned.
        /// </remarks>
        public string CreateHash(string plainText)
        {
            return (plainText != null && plainText.Length > 0) ?
                InternalHash(Encoding.ASCII.GetBytes(plainText)) : string.Empty;
        }

        /// <summary>
        /// Compares the hash generated from the plainText with the
        /// given hashText.
        /// </summary>
        /// <param name="plainText">The plainText used to generate the hash for comparison</param>
        /// <param name="hashText">The hashText used for comparison</param>
        /// <returns>True of the hash generated from plainText is equal to the hashText otherwise false.</returns>
        public bool CompareHash(string plainText, string hashText)
        {
            return hashText == CreateHash(plainText);
        }

        private string InternalEncrypt(byte[] plainBytes)
        {
            byte[] encryptedBytes = null;
            encryptedBytes = Encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return BytesToHexString(encryptedBytes);
        }

        private string InternalDecrypt(byte[] encryptedBytes)
        {
            byte[] decrypted = null;
            decrypted = Decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.ASCII.GetString(decrypted);
        }

        private string InternalHash(byte[] plainBytes)
        {
            byte[] hashedBytes = m_keyHashAlgorithm.ComputeHash(plainBytes, 0, plainBytes.Length);
            return BytesToHexString(hashedBytes);
        }

        /// <summary>
        /// Converts the given string to the corresponding array of Bytes.  
        /// The string is assumed to consist only of an even number of legal hexidecimal characters.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static Byte[] HexStringToBytes(string hexString)
        {
            Byte[] resultArray = null;
            if (hexString != null)
            {
                int hexPairCount = hexString.Length / 2;
                resultArray = new Byte[hexPairCount];
                for (int i = 0; i < hexPairCount; i++)
                {
                    string currentPair = hexString.Substring(i * 2, 2);
                    resultArray[i] = Byte.Parse(currentPair, NumberStyles.AllowHexSpecifier);
                }
            }
            return resultArray;
        }

        /// <summary>
        /// Convert the given byte array to a representative hexidecimal string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] bytes)
        {
            string hexString = null;
            if (bytes != null)
            {
                Char[] chars = new Char[bytes.Length * 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    int b = bytes[i];
                    chars[i * 2] = hexDigits[b >> 4];
                    chars[i * 2 + 1] = hexDigits[b & 0xF];
                }
                hexString = new string(chars);
            }
            return hexString;
        }


        /// <summary>
        /// Encrypt the given plainText array using the current decryption algorithm.
        /// </summary>
        /// <param name="encryptedBytes">An encrypted byte array</param>
        /// <returns>The unencrypted <code>String</code></returns>
        /// <remarks>
        /// If encryptedBytes is null then null is returned. If encryptedBytes is
        /// empty then an empty string is returned.
        /// </remarks>
        public string EncryptQueryString(string plainText)
        {
            string encryptedQueryString = string.Empty;
            string interimText = string.Empty;

            interimText = string.Concat(urlEncryptionKey, plainText, urlEncryptionKey);

            byte[] hashedDataBytes; //The array of bytes that will contain the encrypted value of input.

            //The encoder class used to convert strPlainText to an array of bytes.
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(); 

            //Create an instance of the MD5CryptoServiceProvider class.
            //System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //Call ComputeHash, passing in the plain-text string as an array of bytes 
            //The return value is the encrypted value, as an array of bytes 
            //hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(interimText));
            //hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(interimText));
            hashedDataBytes = m_md5CryptoServiceProvider.ComputeHash(encoder.GetBytes(interimText));

            //Base-64 Encode the results and strip off ending '==', if it exists.
            encryptedQueryString = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

            return encryptedQueryString;
        }
    }
}
