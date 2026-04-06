using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SatisfaccionUI : MonoBehaviour
{ // Referencias a los elementos de la UI
    [Header("Referencias")]
    public Slider satisfaccionSlider;
    public Image satisfaccionFill;
    public TextMeshProUGUI valPorcentaje;
    public TextMeshProUGUI lblImagen;

    //color para la barra de satisfacción
    public Color colorAlto = Color.green;
    public Color colorMedio = Color.yellow;
    public Color colorBajo = Color.red;
    void OnEnable()
    {
        // Suscribirse al evento de cambio de satisfacción
        GameEvents.OnSatisfaccionChanged += UpdateSatisfaccion;
    }
    void OnDisable()
    {
        // Desuscribirse del evento al desactivar el objeto
        GameEvents.OnSatisfaccionChanged -= UpdateSatisfaccion;
    }

    void UpdateSatisfaccion(float porcentaje)
    {

        // 1. Mover la barra
        satisfaccionSlider.value = porcentaje;

        // 2. Cambiar color del fill según el estado
        satisfaccionFill.color = porcentaje > 0.6f ? colorAlto
                               : porcentaje > 0.3f ? colorMedio
                               : colorBajo;

        // 3. Actualizar el porcentaje
        valPorcentaje.text = $"{Mathf.RoundToInt(porcentaje * 100)}%";

        // 4. Cambiar emoji según estado
        lblImagen.text = porcentaje > 0.6f ? "😊 CLIENTES"
                      : porcentaje > 0.3f ? "😐 CLIENTES"
                     : "😡 CLIENTES";
    }
}
