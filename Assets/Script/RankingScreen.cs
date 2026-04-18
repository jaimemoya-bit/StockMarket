using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingScreen : MonoBehaviour
{
    public Transform   contenedor;     // Contenedor_Ranking
    public GameObject  filaPrefab;     // prefab FilaRanking
    private RankingAPI _rankingAPI;

    public static string escenaRetorno = "MenuScene"; // Escena a la que volverá al pulsar "Volver"

    void Start()
    {
        _rankingAPI = FindObjectOfType<RankingAPI>();
        CargarRanking();
    }
    void CargarRanking()
    {
        // Limpia filas anteriores
        foreach (Transform h in contenedor)
            Destroy(h.gameObject);

        if (_rankingAPI == null)
        {
            Debug.LogWarning("RankingAPI no encontrada — usando datos de prueba");
            MostrarLista(RankingAPI.GetTop10Falso());
            return;
        }

        StartCoroutine(_rankingAPI.GetTop10(
            onOk: (lista) =>
            {
                MostrarLista(lista);
            },
            onError: (msg) =>
            {
                Debug.LogWarning(msg + " — usando datos de prueba");
                MostrarLista(RankingAPI.GetTop10Falso());
            }
        ));
    }

    void MostrarLista(List<RankingData> lista)
    {
        foreach (var entry in lista)
        {
            GameObject obj  = Instantiate(filaPrefab, contenedor);
            FileRanking fila = obj.GetComponent<FileRanking>();
            fila.Init(entry.rank, entry.userName, entry.score);
        }
    }

    public void OnClickVolver()
    {
        Debug.Log("OnClickVolver llamado — escenaRetorno: " + escenaRetorno);
        SceneManager.LoadScene(escenaRetorno);
    }
}