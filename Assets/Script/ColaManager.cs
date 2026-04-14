using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaManager : MonoBehaviour
{
    //Array de posiciones de la cola(gameObjects vacios)
    //Se asigna en editor
    [SerializeField] private Transform[] posisCola;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    // lista de clientes actuales
    private List<Cliente> clientes = new List<Cliente>();

    // añadir cliente a la cola
    public int ACola(Cliente cliente)
    {
        clientes.Add(cliente);
        UpdateQueuePositions();
        return clientes.Count - 1;
    }

    // Quitar cliente de la cola
    public void SalirCola(Cliente customer)
    {
        clientes.Remove(customer);
        UpdateQueuePositions();
    }

    // Actualizar posiciones de todos los clientes
    void UpdateQueuePositions()
    {
        for (int i = 0; i < clientes.Count; i++)
        {
            clientes[i].MoverseACaja(posisCola[i]);
        }

    }

    // Comprobar si es el primero en la cola
    public bool EsPrimero(Cliente customer)
    {
        return clientes.Count > 0 && clientes[0] == customer;
    }

    // comprobar si la cola está llena
    public bool ColaLlena()
    {
        // si hay más clientes que puntos disponibles
        return clientes.Count >= posisCola.Length;
    }
}
