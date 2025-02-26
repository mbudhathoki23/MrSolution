using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MrDAL.Core.Utils;

public class EncryptOrDecrypt
{
    public static string DecryptString(string message, string passphrase)
    {
        byte[] results;
        var utf8 = new UTF8Encoding();
        var hashProvider = new MD5CryptoServiceProvider();
        var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));
        var tdesAlgorithm = new TripleDESCryptoServiceProvider
        {
            Key = tdesKey,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        var dataToDecrypt = Convert.FromBase64String(message.Replace(" ", "+"));
        try
        {
            var decryptor = tdesAlgorithm.CreateDecryptor();
            results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
        }
        finally
        {
            tdesAlgorithm.Clear();
            hashProvider.Clear();
        }

        return utf8.GetString(results);
    }

    public static string EncryptString(string message, string passphrase)
    {
        byte[] results;
        var utf8 = new UTF8Encoding();
        var hashProvider = new MD5CryptoServiceProvider();
        var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passphrase));
        var tdesAlgorithm = new TripleDESCryptoServiceProvider
        {
            Key = tdesKey,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        var dataToEncrypt = utf8.GetBytes(message);
        try
        {
            var encryptor = tdesAlgorithm.CreateEncryptor();
            results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
        }
        finally
        {
            tdesAlgorithm.Clear();
            hashProvider.Clear();
        }

        return Convert.ToBase64String(results);
    }

    public static string PasswordDecrypt(string cryptTxt, string key)
    {
        cryptTxt = cryptTxt.Replace(" ", "+");
        var bytesBuff = Convert.FromBase64String(cryptTxt);
        var aes = Aes.Create();
        try
        {
            var crypto = new Rfc2898DeriveBytes(key,
                new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
            aes.Key = crypto.GetBytes(32);
            aes.IV = crypto.GetBytes(16);
            var mStream = new MemoryStream();
            try
            {
                var cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                try
                {
                    cStream.Write(bytesBuff, 0, bytesBuff.Length);
                    cStream.Close();
                }
                finally
                {
                    if (cStream != null) ((IDisposable)cStream).Dispose();
                }

                cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
            }
            finally
            {
                if (mStream != null) ((IDisposable)mStream).Dispose();
            }
        }
        finally
        {
            if (aes != null) ((IDisposable)aes).Dispose();
        }

        return cryptTxt;
    }

    public static string PasswordEncrypt(string inText, string key)
    {
        var bytesBuff = Encoding.Unicode.GetBytes(inText);
        var aes = Aes.Create();
        try
        {
            var crypto = new Rfc2898DeriveBytes(key, "Ivan Medvedev"u8.ToArray());
            aes.Key = crypto.GetBytes(32);
            aes.IV = crypto.GetBytes(16);
            var mStream = new MemoryStream();
            try
            {
                var cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                try
                {
                    cStream.Write(bytesBuff, 0, bytesBuff.Length);
                    cStream.Close();
                }
                finally
                {
                    if (cStream != null) ((IDisposable)cStream).Dispose();
                }

                inText = Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                if (mStream != null) ((IDisposable)mStream).Dispose();
            }
        }
        finally
        {
            if (aes != null) ((IDisposable)aes).Dispose();
        }

        return inText;
    }
}