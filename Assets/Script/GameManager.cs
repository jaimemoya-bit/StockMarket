using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
    void Start()
    {
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

        //actualizar el tiempo
        tiempoRestante -= Time.deltaTime;
        GameEvents.CambiarTiempo(tiempoRestante);
        //fin del juego si el tiempo se acaba
        if (tiempoRestante < 0)
        {
            tiempoRestante = 0;
            jugando = false;
            Debug.Log("¡Tiempo terminado! Fin del juego.");
        }
        GameEvents.CambiarTiempo(tiempoRestante);
            //----------test----------------
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameEvents.NuevoPedido(new[] { "Refresco", "Snack", "Agua" });
        }
    }

//--------------------Botones----------------------
    void onRecoger()
    {
        Debug.Log("Recoger pedido");
        //aumentar  la satisfaccion al recoger un pedido
        
        satisfaccion += 0.1f;
        if (satisfaccion > 1f) satisfaccion = 1f;

        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    void onCobrar()
    {
        Debug.Log("Cobrar pedido");
        //aumentar el dinero y la satisfaccion al cobrar un pedido
        dinero += 150f;
        satisfaccion += 0.05f;
        if (satisfaccion > 1f) satisfaccion = 1f;

        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    public void SubirNivel()
    {
        nivel++;
        GameEvents.CambiarNivel(nivel);
    }   

}