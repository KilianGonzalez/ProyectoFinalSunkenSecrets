using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.text;
using System;

public class GestorGuardado
{
    private static string carpetaGuardado => Application.persistentDataPath + "/Partidas";
    private static string clave = "clave_segura_de_32_bytes_1234567890abcd";
    private static string iv = "vector_inicial_16b";

    public static void GuardarPartida(DatosPartida datos, string ranura)
    {
        string json = JsonUtility.ToJson(datos);
        string encriptado = Encriptar(json);

        if(!Directory.Exists(carpetaGuardado))
            Directory.CreateDirectory(carpetaGuardado);
    

        File.WriteAllText(carpetaGuardado + ranura + ".dat", encriptado);
    }

    public static DatosPartida CargarPartida(string ranura)
    {
        string ruta = carpetaGuardado + ranura + ".dat";
        if(!File.Exists(ruta)) return null;

        string encriptado = File.ReadAllText(ruta);
        string desencriptado = Desencriptar(encriptado);
        return JsonUtility.FromJson<DatosPartida>(desencriptado);
    }

    public static void BorrarPartida(string ranura)
    {
        string ruta = (carpetaGuardado + ranura + ".dat");
        if (File.Exists(ruta))
            File.Delete(ruta);
    }

    public static bool ExistePartida(string ranura)
    {
        return File.Exists(carpetaGuardado + ranura + ".dat");
    }

    public static string[] ObtenerRanuras()
    {
        return new string[] { "ranura1", "ranura2", "ranura3" };
    }

    private static string Encriptar(string textoPlano)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Enconding.UTF8.GetBytes(clave);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            using var cifrador = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, cifrador, CrytoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(textoPlano);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
    }

    private static string Desencriptar(string textoCifrado)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(clave);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            using var descifrador = aes.Create.Decryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(textoCifrado));
            using var cs = new CryptoStream(ms, descifrador, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }

}