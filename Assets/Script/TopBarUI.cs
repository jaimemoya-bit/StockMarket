using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TopBarUI : MonoBehaviour
{
    [Header("referencias")]
    public TextMeshProUGUI valDinero;
    public TextMeshProUGUI valTiempo;
    public TextMeshProUGUI valNivel;
    void OnEnable()
    {

        //suscribirse a los eventos al iniciar el juego

        GameEvents.OnDineroChanged += UpdateDinero;
        GameEvents.OnTiempoChanged += UpdateTiempo;
        GameEvents.OnNivelChanged += UpdateNivel;

    }
    void OnDisable()
    {
        //desuscribirse de los eventos al desactivar el objeto
        GameEvents.OnDineroChanged -= UpdateDinero;
        GameEvents.OnTiempoChanged -= UpdateTiempo;
        GameEvents.OnNivelChanged -= UpdateNivel;
    }
    //metodos para actualizar la UI
    void UpdateDinero(float Cantidad)
    {
        valDinero.text = Cantidad.ToString();
    }
    void UpdateTiempo(float segundos)
    {
        valTiempo.text = segundos.ToString();
        int minutos = Mathf.FloorToInt(segundos / 60);
        int segundosRestantes = Mathf.FloorToInt(segundos % 60);
        valTiempo.text = minutos.ToString() + ":" + segundosRestantes.ToString("00");
        
    valTiempo.color = segundos <= 10 ? Color.red : Color.white; // Cambia el color a rojo si quedan 10 segundos o menos NECESITA CAMBIAR EL COLOR


    }
//metodo para actualizar el nivel NECESITA CAMBIAR QUE EL NIVEL SE MUESTRE
    void UpdateNivel(int nivel)
    {
        valNivel.text = nivel.ToString();
    }
}
