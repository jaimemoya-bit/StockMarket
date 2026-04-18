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
        using (UnityWebRequest req = UnityWebRequest.Get(URL_BASE))
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
    // TEST — devuelve datos falsos sin necesitar servidor
     public static List<RankingData> GetTop10Falso()
    {
        return new List<RankingData>
        {
            new RankingData { rank = 1, userName = "Carlos", score = 4200 },
            new RankingData { rank = 2, userName = "Maria", score = 3800 },
            new RankingData { rank = 3, userName = "Juanma", score = 3100 },
            new RankingData { rank = 4, userName = "Ana", score = 2900 },
            new RankingData { rank = 5, userName = "Pedro", score = 2400 },
            new RankingData { rank = 6, userName = "Laura", score = 2100 },
            new RankingData { rank = 7, userName = "Sergio", score = 1800 },
            new RankingData { rank = 8, userName = "Sofia", score = 1500 },
            new RankingData { rank = 9, userName = "Miguel", score = 1200 },
            new RankingData { rank = 10, userName = "Elena", score = 900 }
        };
    }
        }
    