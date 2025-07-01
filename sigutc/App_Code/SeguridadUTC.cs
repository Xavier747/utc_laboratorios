using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SeguridadUTC
{
    // La clave debe ser de 16, 24 o 32 caracteres
    private readonly string key = "claveSecreta1234"; // 16 caracteres (AES-128)
    private readonly string ivStr = "vectorInit123456"; // 16 caracteres

    public string Encripta(string texto)
    {
        byte[] iv = Encoding.UTF8.GetBytes(ivStr);
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(texto);
                sw.Close();
                array = ms.ToArray();
            }
        }

        return Convert.ToBase64String(array);
    }

    public string Desencripta(string textoEncriptado)
    {
        byte[] iv = Encoding.UTF8.GetBytes(ivStr);
        byte[] buffer = Convert.FromBase64String(textoEncriptado);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(buffer))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
