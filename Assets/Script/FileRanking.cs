using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FileRanking : MonoBehaviour
{
    public TextMeshProUGUI txtPosicion;
    public TextMeshProUGUI txtNombre;
    public TextMeshProUGUI txtPuntaje;

    public void Init(int posicion, string nombre, float puntaje)
    {
        // Top 3 con colores especiales
        if (posicion == 1)
            txtPosicion.color = Color.yellow;
        else if (posicion == 2)
            txtPosicion.color = Color.gray;
        else if (posicion == 3)
            txtPosicion.color = new Color(0.8f, 0.5f, 0.2f); // Bronce  
        else
            txtPosicion.color = Color.white;
            txtNombre.text = nombre;
            txtPuntaje.text = puntaje.ToString("N0") + " pts";
    }

}
