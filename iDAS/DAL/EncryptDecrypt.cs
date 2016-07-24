using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace iDAS.DAL
{
    public class EncryptDecrypt
    {
        private static TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        //private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        private static string key = "q&8gq)@&Re)d";
        private static string passPhrase = "qru$ipaSt55htm";
        private static string saltValue = "u9d8r%^@5hjm";
        private static string hashAlgorithm = "SHA1";
        private static int passwordIterations = 7;
        private static string initVector = "y8&jq{u=17>@j?rpox+";
        private static int keySize = 192;

        public static string Encrypt(string plainText)
        {
            string functionReturnValue = null;

            if ((string.IsNullOrEmpty(plainText)))
            {
                return string.Empty;
            }

            try
            {
                // Convert strings into byte arrays.
                // Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                // encoding.
                byte[] initVectorBytes = null;
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                byte[] saltValueBytes = null;
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our plaintext into a byte array.
                // Let us assume that plaintext contains UTF8-encoded characters.
                byte[] plainTextBytes = null;
                plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified passphrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.

                //Dim password As PasswordDeriveBytes
                //password = New PasswordDeriveBytes(passPhrase, _
                //                                   saltValueBytes, _
                //                                   hashAlgorithm, _
                //                                   passwordIterations)

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).

                byte[] keyBytes = null;
                //keyBytes = password.GetBytes(keySize / 8)

                Rfc2898DeriveBytes password = default(Rfc2898DeriveBytes);

                password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);
                keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = default(RijndaelManaged);
                symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform encryptor = default(ICryptoTransform);
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = default(MemoryStream);
                memoryStream = new MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).
                CryptoStream cryptoStream = default(CryptoStream);
                cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = null;
                cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                string cipherText = null;
                cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                functionReturnValue = cipherText;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return functionReturnValue;
        }

        public static string Decrypt(string cipherText)
        {
            string functionReturnValue = null;

            try
            {
                if ((string.IsNullOrEmpty(cipherText)))
                {
                    return string.Empty;
                }
                // Convert strings defining encryption key characteristics into byte
                // arrays. Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8
                // encoding.
                byte[] initVectorBytes = null;
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                byte[] saltValueBytes = null;
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our ciphertext into a byte array.
                byte[] cipherTextBytes = null;
                cipherTextBytes = Convert.FromBase64String(cipherText);

                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.

                //Dim password As PasswordDeriveBytes
                //password = New PasswordDeriveBytes(passPhrase, _
                //                                   saltValueBytes, _
                //                                   hashAlgorithm, _
                //                                   passwordIterations)


                //' Use the password to generate pseudo-random bytes for the encryption
                //' key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = null;
                Rfc2898DeriveBytes password = default(Rfc2898DeriveBytes);

                password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);
                keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = default(RijndaelManaged);
                symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor = default(ICryptoTransform);
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = default(MemoryStream);
                memoryStream = new MemoryStream(cipherTextBytes);

                // Define memory stream which will be used to hold encrypted data.
                System.Security.Cryptography.CryptoStream cryptoStream = default(System.Security.Cryptography.CryptoStream);
                cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                byte[] plainTextBytes = null;
                plainTextBytes = new byte[cipherTextBytes.Length + 1];

                // Start decrypting.
                int decryptedByteCount = 0;
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.
                string plainText = null;
                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                // Return decrypted string.
                functionReturnValue = plainText;

            }
            catch (Exception)
            {
                return string.Empty;
            }
            return functionReturnValue;
        }

        public static string Base64Encode(string plainText)
        {
            try
            {
                dynamic plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                dynamic base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetMd5HashKey(string input)
        {

            //step 1, calculate MD5 hash from input
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string. 
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            int i = 0;
            for (i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();

        }
        //GetMd5Hash

        private static string keyQueryString = "App987123654NewsBreak";
        private static string saltQueryString = "00SaltAppNews33221166554400";

        public static string EncryptQueryString(string inputText)
        {
            byte[] plainText = Encoding.UTF8.GetBytes(inputText);

            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(keyQueryString), Encoding.ASCII.GetBytes(saltQueryString));
                using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainText, 0, plainText.Length);
                            cryptoStream.FlushFinalBlock();
                            string base64 = Convert.ToBase64String(memoryStream.ToArray());

                            // Generate a string that won't get screwed up when passed as a query string.
                            string urlEncoded = HttpUtility.UrlEncode(base64);
                            return urlEncoded;
                        }
                    }
                }
            }
        }

        public static string DecryptQueryString(string inputText)
        {
            byte[] encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(keyQueryString), Encoding.ASCII.GetBytes(saltQueryString));

            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainText = new byte[encryptedData.Length];
                            cryptoStream.Read(plainText, 0, plainText.Length);
                            string utf8 = Encoding.UTF8.GetString(plainText);
                            return utf8;
                        }
                    }
                }
            }
        }

        public static string GenerateHashCode(string valueWantToGenerateHashCode)
        {
            string generatedHashCode = string.Empty;

            try
            {
                byte[] btyScr = ASCIIEncoding.ASCII.GetBytes(valueWantToGenerateHashCode);
                MD5CryptoServiceProvider objMd5 = new MD5CryptoServiceProvider();
                byte[] btyRes = objMd5.ComputeHash(btyScr);
                int intTotal = (btyRes.Length * 2 + (btyRes.Length / 8));
                StringBuilder strRes = new StringBuilder(intTotal);
                int intI = 0;
                for (intI = 0; intI <= btyRes.Length - 1; intI++)
                {
                    strRes.Append(BitConverter.ToString(btyRes, intI, 1));
                }

                generatedHashCode = strRes.ToString().TrimEnd(new char[] { ' ' }).ToLower();


            }
            catch (Exception ex)
            {
            }

            return generatedHashCode;
        }
    }
}