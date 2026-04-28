using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
     * Declaramos variables para el dinero del jugador
     * y su correspondiente elemento de interfaz
    */
    private int dinero;
    //El elemento de interfaz se asigna mediante editor
    public TextMeshProUGUI dineroText;

    //Variable para la satisfaccion general de la tienda
    private int satisfaccion;
    //Elemento de interfaz satisfaccion
    public TextMeshProUGUI satisfaccionText;
    /*Elemento de texto para mostrar cuanta satisfaccion
     * se añade*/
    public TextMeshProUGUI satisAddedText;


    // Start is called before the first frame update
    void Start()
    {
        //Asignamos el dinero a 0 On Start
        dinero = 0;
        //Lo mismo con el elemento de interfaz
        dineroText.text = "$ " + dinero;

    }

    // Update is called once per frame
    void Update()
    {


    }

    //Añade el precio del producto al dinero del jugador
    //Actualiza el elemento de la interfaz que muestra el dinero
    public void Cobro(int precioProd)
    {
        dinero += precioProd;
        dineroText.text = "$ " + dinero;
    }

    //Añade la satisfacción actual del cliente a la general
    //To do: Hacer media con cada cliente instanciado?
    public void AnadirSatis(int clientSatis)
    {
        satisfaccion += clientSatis;
        satisfaccionText.text = "Satisfacción general: " + satisfaccion;
        satisAddedText.text = "+ " + clientSatis;
        StartCoroutine(PopUpAddedSatis());
    }

    public IEnumerator PopUpAddedSatis()
    {
        /*
         * Cuando añadimos satisfaccion mostramos por un momento la cantidad
         * añadida (satisfaccion actual del cliente)
         */
        satisAddedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        satisAddedText.gameObject.SetActive(false);
    }
}
