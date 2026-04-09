using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


public class ProgresoApi : MonoBehaviour
{
    // URL base de la API CAMBIAR POR LA URL DE LA API REAL
    private const string URL_BASE = "https://api-progreso.onrender.com/api/progreso";


    //---guardar progreso---
    public IEnumerator GuardarProgreso( float dinero, System.Action onSuccess, System.Action<string> onError)
    {
        // necesitamos el token del login para autenticar la solicitud
        if ( string.IsNullOrEmpty(AuthManager.Token))
        {
            onError?.Invoke("Usuario no autenticado");
            yield break;
       
        }
        string json = "{" +"\"userId\": " + AuthManager.UserId + ", \"dinero\": " + dinero.ToString(System.Globalization.CultureInfo.InvariantCulture) + "}";
        using (UnityWebRequest request = new UnityWebRequest(URL_BASE, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + AuthManager.Token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Progreso guardado exitosamente: " );
                onSuccess();
            }
            else
            {
                Debug.LogWarning("Error al guardar progreso: " + request.error);
                onError("Error al guardar progreso: " + request.error);
        
            }
        }
    }
//---------gurdar ranking----------------
    public IEnumerator GuardarRanking( int puntos, System.Action onSuccess, System.Action<string> onError)
    {
        // necesitamos el token del login para autenticar la solicitud
        if ( string.IsNullOrEmpty(AuthManager.Token))
        {
            onError?.Invoke("Usuario no autenticado");
            yield break;
       
        }
        string json = "{" +"\"userId\": " + AuthManager.UserId + ", \"puntos\": " + puntos.ToString() + "}";
        using (UnityWebRequest request = new UnityWebRequest(URL_BASE + "/ranking", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + AuthManager.Token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Ranking guardado exitosamente: " );
                onSuccess();
            }
            else
            {
                Debug.LogWarning("Error al guardar ranking: " + request.error);
                onError("Error al guardar ranking: " + request.error);
        
            }
        }
    }

}
