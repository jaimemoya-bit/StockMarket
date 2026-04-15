using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    //url base para las peticiones de autenticación Cambiar por la url de tu backend
    private const string URL_BASE = "http://localhost:8080/api/auth/";

    //token jwt obtenido después del login
    public static string Token { get; private set; }
    public static int UserId{ get; private set; }
    public static string UserName { get; private set; }

    //------login------
    public IEnumerator Login(string email, string password, System.Action<string> onSucess, System.Action<string> onError)
    {
        // Crear el objeto con los datos de login
        string json ="{ \"email\": \"" + email + "password\": \"" + password + "\" }";

        // Crear la petición POST
          using (UnityWebRequest req = new UnityWebRequest(URL_BASE + "/auth/login", "POST"))
           {
                
               req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
               req.downloadHandler = new DownloadHandlerBuffer();
               req.SetRequestHeader("Content-Type", "application/json");

               yield return req.SendWebRequest();

               if (req.result == UnityWebRequest.Result.Success)
               {
                   // Procesar la respuesta y extraer el token
                   LoginResponse response = JsonUtility.FromJson<LoginResponse>(req.downloadHandler.text);
                   Token = response.token;
                   UserId = response.userId;
                   UserName = response.userName;
                   onSucess?.Invoke("Login exitoso");
               }
               else
               {
                   onError?.Invoke("Error en email o contraseña: " + req.error);
               }
           }
    }
//------registro------
    public IEnumerator Register(string name, string email, string password, System.Action<string> onSucess, System.Action<string> onError)
    {
        // Crear el objeto con los datos de registro
        string json ="{\"name\": \"" + name + "\", \"email\": \"" + email + "\"}, \"password\": \"" + password + "\" }";

        // Crear la petición POST
        using (UnityWebRequest req = new UnityWebRequest(URL_BASE + "register", "POST"))
        {
            
            req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                onSucess?.Invoke("Registro exitoso");
            }
            else
            {
                onError?.Invoke("Error en el registro: " + req.error);
            }
        }
    }
    //------cerrar sesion------
    public static void CerrarSesion()
    {
        Token = null;
        UserId = 0;
        UserName = null;
    }

    //---Clases para parsear el json de respuesta del login---
    [System.Serializable]   
    private class LoginResponse
    {
        public string token;
        public int userId;
        public string userName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
