using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GitSync
{
    // security module - user information
    static class EncryptMessages
    {
        public static string Encrypt(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    byte[] bText = new UTF8Encoding().GetBytes(message);
                    MemoryStream mStream = new MemoryStream();
                    CryptoStream encryptor = new CryptoStream(
                        mStream,
                        new RijndaelManaged() { KeySize = 256, BlockSize = 128, Padding = PaddingMode.PKCS7 }.CreateEncryptor(GetKeyVector(), GetBIVVector()),
                        CryptoStreamMode.Write
                    );
                    encryptor.Write(bText, 0, bText.Length);
                    encryptor.FlushFinalBlock();
                    return Convert.ToBase64String(mStream.ToArray());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string Decrypt(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    byte[] bText = Convert.FromBase64String(message);
                    MemoryStream mStream = new MemoryStream();
                    CryptoStream encryptor = new CryptoStream(
                        mStream,
                        new RijndaelManaged() { KeySize = 256, BlockSize = 128, Padding = PaddingMode.PKCS7 }.CreateDecryptor(GetKeyVector(), GetBIVVector()),
                        CryptoStreamMode.Write
                    );
                    encryptor.Write(bText, 0, bText.Length);
                    encryptor.FlushFinalBlock();
                    return new UTF8Encoding().GetString(mStream.ToArray());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static byte[] GetKeyVector()
        {
            // just a sample array (not the real one) -- put your own vector here
            return new byte[] { 0, 136, 79, 185, 167, 70, 236, 179, 69, 144, 83, 0, 28, 104, 52, 45, 150, 164, 246, 195, 232, 230, 108, 116, 238, 139, 51, 186, 187, 148, 74, 28 };            
        }

        private static byte[] GetBIVVector()
        {
            // just a sample array (not the real one) -- put your own vector here
            return new byte[] { 27, 90, 184, 220, 61, 152, 84, 24, 211, 200, 164, 114, 96, 78, 71, 193 };
        }
    }
}
