using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Para poder usar los Input Fields
using UnityEngine.SceneManagement; // Para poder cambiar de pantalla

public class LoginManager : MonoBehaviour
{
    [Header("Conexiones de la Interfaz")]
    public TMP_InputField inputUsuario;
    public TMP_InputField inputPassword;
    public GameObject textError;

    // URL real de tu API cuando este lista
    private string urlAPI = "http://localhost:3000/api/auth/login";

    // Esta función es la que lanzaremos cuando el jugador pulse el botón "Entrar"
    public void IntentarLogin()
    {
        // 1. Apagamos el error por si estaba encendido de un intento anterior fallido
        textError.SetActive(false);

        // 2. Iniciamos la conexión pasando lo que el usuario ha escrito en las cajas
        StartCoroutine(LlamadaAlServidor(inputUsuario.text, inputPassword.text));
    }

    IEnumerator LlamadaAlServidor(string usuario, string password)
    {
        // 3. Empaquetamos los datos (como si fuera un paquete de correos)
        WWWForm formulario = new WWWForm();
        formulario.AddField("username", usuario);
        formulario.AddField("password", password);

        // 4. Enviamos la petición POST por internet a la URL
        using (UnityWebRequest conexion = UnityWebRequest.Post(urlAPI, formulario))
        {
            // Le decimos a Unity que espere aquí hasta que el servidor nos conteste
            yield return conexion.SendWebRequest();

            // 5. ¿Hubo algún fallo? (Contraseña mal, servidor caído...)
            if (conexion.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("El servidor rechazó el login: " + conexion.error);
                textError.SetActive(true); // ¡Encendemos el texto rojo de error!
            }
            else
            {
                // 6. ¡Login Correcto! Leemos lo que nos manda el servidor
                string respuestaServidor = conexion.downloadHandler.text;
                Debug.Log("Login exitoso. El servidor dice: " + respuestaServidor);

                // 7. PERSISTENCIA DAM: Guardamos la respuesta (el JSON con el token) en el disco duro
                PlayerPrefs.SetString("TokenUsuario", respuestaServidor);
                PlayerPrefs.Save();

                // 8. Viajamos a la escena del juego principal
                SceneManager.LoadScene("Supermarket");
            }
        }
    }
}