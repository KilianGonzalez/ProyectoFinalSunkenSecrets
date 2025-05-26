using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class GestorGuardado
{
    private static string carpetaGuardado => Application.persistentDataPath;
    private static readonly string clave = "SubmarinoTesoroClave1234567890";
    private static string iv = "vectorinicial16b";

    private static string ranuraActiva;


    public static void EstablecerRanuraActiva(string ranura)
    {
        ranuraActiva = ranura;
    }

    public static string ObtenerRanuraActiva()
    {
        return ranuraActiva;
    }

    private static byte[] ObtenerClave()
    {
        // Convierte el string en bytes usando UTF8
        byte[] claveBytes = Encoding.UTF8.GetBytes(clave);

        // Si no tiene 32 bytes exactos, la ajustamos
        if (claveBytes.Length != 32)
        {
            // Opcional: Puedes usar un hash como SHA256 para asegurarte de que la clave sea siempre de 32 bytes
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        return claveBytes;
    }

    private static byte[] ObtenerIV()
    {
        // Convierte el string en bytes usando UTF8
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

        // Si no tiene 16 bytes exactos, la ajustamos
        if (ivBytes.Length != 16)
        {
            throw new Exception("El IV debe tener exactamente 16 bytes.");
        }

        return ivBytes;
    }


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
            aes.Key = ObtenerClave(); // Usamos la clave ajustada
            aes.IV = ObtenerIV(); // Usamos el IV ajustado

            using var cifrador = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, cifrador, CryptoStreamMode.Write);
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
            aes.Key = ObtenerClave(); // Usamos la clave ajustada
            aes.IV = ObtenerIV(); // Usamos el IV ajustado

            using var descifrador = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(textoCifrado));
            using var cs = new CryptoStream(ms, descifrador, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }

}