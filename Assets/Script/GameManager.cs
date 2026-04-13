using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Configuracion del juego, variables globales
    [Header("Configuracion")]
    public float tiempototal=120f;
    public int nivelInicial=1;


    public float dinero;
    public float tiempoRestante;
    public int nivel;
    public float satisfaccion;
    public bool jugando;
    private ProgresoApi _progresoApi;
    void Start()
    {
        _progresoApi = GetComponent<ProgresoApi>();
        tiempoRestante = tiempototal;
        nivel = nivelInicial;
        jugando = true;
        dinero = 0f;
        satisfaccion = 1f;
        //suscribirse a eventos
        GameEvents.OnRecoger += onRecoger;
        GameEvents.OnCobrar += onCobrar;

        // lanzar el evento al inicio
        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarTiempo(tiempoRestante);
        GameEvents.CambiarNivel(nivel);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }
    void OnDestroy()
    {
        //desuscribirse de eventos
        GameEvents.OnRecoger -= onRecoger;
        GameEvents.OnCobrar -= onCobrar;
    }
    void Update()
{
    if (!jugando) return;

    tiempoRestante -= Time.deltaTime;
    GameEvents.CambiarTiempo(tiempoRestante);

    if (tiempoRestante < 0)
    {
        tiempoRestante = 0;
        jugando = false;
        Debug.Log("¡Tiempo terminado! Fin del juego.");
        FindeTurno();   // ← aquí
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
        RankingAPI rankingAPI = GetComponent<RankingAPI>();
        var datos = rankingAPI.GetTop10Falso();
        foreach (var entry in datos)
        {
            Debug.Log(entry.rank + ". " + entry.userName + " → " + entry.score);
        }
    }
}
//--------------------Botones----------------------
    public  void onRecoger()
    {
        Debug.Log("Recoger pedido");
        //aumentar  la satisfaccion al recoger un pedido
        
        satisfaccion += 0.1f;
        if (satisfaccion > 1f) satisfaccion = 1f;

        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    public void onCobrar()
    {
        Debug.Log("Cobrar pedido");
        //aumentar el dinero y la satisfaccion al cobrar un pedido
        dinero += 150f;
        satisfaccion += 0.05f;
        if (satisfaccion > 1f) satisfaccion = 1f;

        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    // --------------------Fin del turno----------------------
    
    void FindeTurno()
    {
        Debug.Log("¡Fin del turno! Dinero ganado: " + dinero);

        //notifica HUD
        GameEvents.FinTurno(dinero);
        //guardar progreso en la API
        StartCoroutine(_progresoApi.GuardarProgreso(dinero, 
            onSuccess: () => Debug.Log("Progreso guardado exitosamente"),
            onError: (error) => Debug.LogWarning("Error al guardar progreso: " + error)
        ));
    }
    void GuardarRanking()
    {
        Debug.Log("Guardando ranking... Puntos: " + nivel);

        //guardar ranking en la API
        StartCoroutine(_progresoApi.GuardarRanking(nivel, 
            onSuccess: () => Debug.Log("Ranking guardado exitosamente"),
            onError: (error) => Debug.LogWarning("Error al guardar ranking: " + error)
        ));
    }
    
    public void SubirNivel()
    {
        nivel++;
        GameEvents.CambiarNivel(nivel);
    }   


}