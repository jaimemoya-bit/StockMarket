using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RankingAPI : MonoBehaviour
{
    // ranking Api endpoint CAMBIAR POR EL ENDPOINT REAL
    private const string URL_BASE = "http://localhost:8080/api/ranking";
 public IEnumerator GetTop10(System.Action<List<RankingData>> onOk,
                                System.Action<string> onError)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL_BASE + "/ranking"))
        {
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                string json = req.downloadHandler.text;
                Debug.Log("Ranking recibido: " + json);

                RankingResponse resp = JsonUtility.FromJson<RankingResponse>(json);

                if (resp != null && resp.rankingList != null)
                {
                    onOk(resp.rankingList);
                }
                else
                {
                    onError("Respuesta vacía del servidor");
                }
            }
            else
            {
                Debug.LogWarning("Error al obtener ranking: " + req.error);
                onError("No se pudo cargar el ranking");
            }
        }
    }
        }
    