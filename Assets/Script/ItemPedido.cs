using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ItemPedido : MonoBehaviour
{
    public TextMeshProUGUI Txt_Nombre;
    public TextMeshProUGUI Txt_Estado;

    public void Init(string nombre)
    {
        Txt_Nombre.text = nombre;
        Txt_Estado.text = "pendiente";
        Txt_Estado.color = Color.gray;
    }

    public void Recogido()
    {
        Txt_Estado.text = "listo";
        Txt_Estado.color = Color.green;
    }
}