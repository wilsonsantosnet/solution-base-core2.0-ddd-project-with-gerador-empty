using Common.Domain.Enums;
using Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Cripto
{

    public class Cripto : ICripto
    {

        public string ComputeHashMd5(string value, string salt)
        {
            if (salt.IsNullOrEmpaty())
                throw new InvalidOperationException("Salt not found");

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            using (var tripleDES = TripleDES.Create())
            {
                byte[] Results;
                UTF8Encoding UTF8 = new UTF8Encoding();
                MD5 MD5 = MD5.Create();

                byte[] TDESKey = MD5.ComputeHash(UTF8.GetBytes(salt));

                if (TDESKey.Length == 16)
                {
                    byte[] keyTemp = new byte[24];
                    Buffer.BlockCopy(TDESKey, 0, keyTemp, 0, TDESKey.Length);
                    Buffer.BlockCopy(TDESKey, 0, keyTemp, TDESKey.Length, 8);
                    TDESKey = keyTemp;
                }

                tripleDES.Key = TDESKey;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;

                byte[] DataToEncrypt = UTF8.GetBytes(value);

                ICryptoTransform Encryptor = tripleDES.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

                return Convert.ToBase64String(Results);
            }
        }

        public string Encrypt(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public string Decrypt(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText.Replace(" ","+"));

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        private static byte[] MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(input));

                //Fix para key 24
                if (result.Length < 24)
                {
                    List<byte> byteList = new List<byte>(result);
                    for (int i = result.Length - 1; i < 24; i++)
                    {
                        byteList.Add(0);
                    }
                    result = byteList.Take(24).ToArray();
                }

                return result;
            }
        }
    }
}
