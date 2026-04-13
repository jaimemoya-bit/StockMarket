using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingScreen : MonoBehaviour
{
    public Transform   contenedor;     // Contenedor_Ranking
    public GameObject  filaPrefab;     // prefab FilaRanking

    private RankingAPI _rankingAPI;

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

        // Intenta cargar desde servidor
        // Si falla usa datos falsos
        StartCoroutine(_rankingAPI.GetTop10(
            onOk: (lista) =>
            {
                MostrarLista(lista);
            },
            onError: (msg) =>
            {
                Debug.LogWarning(msg + " — usando datos de prueba");
                MostrarLista(_rankingAPI.GetTop10Falso());
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
        SceneManager.LoadScene("LoginScene");
    }
}