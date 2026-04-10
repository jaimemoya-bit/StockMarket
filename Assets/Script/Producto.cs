using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producto : MonoBehaviour
{
    //Creamos variable de tipo GameManager para poder llamar al metodo Cobro()
    private GameManager gameManager;

    //Variable en la que guardaremos el precio del producto
    public int precio;

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos precio aleatorio como placeholder (To do)
        precio = Random.Range(1, 100);
        //Asignamos el objeto GameManager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Cuando clickamos en el objeto (Placeholder) se llama al metodo Cobro()
     * y como atributo int requerido le pasamos la variable precio
     */
    //El metodo Cobro() añade el precio del producto al dinero del jugador
    public void OnMouseDown()
    {
        gameManager.Cobro(precio);
    }

}
