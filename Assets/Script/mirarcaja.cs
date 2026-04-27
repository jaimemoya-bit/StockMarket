using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirarcaja : MonoBehaviour
{
   GameObject[] cajas;
    GameObject cajaMasCercana;

    void Update()
    {
        cajas = GameObject.FindGameObjectsWithTag("caja");

        float distanciaMinima = Mathf.Infinity;
        cajaMasCercana = null;

        foreach (GameObject caja in cajas)
        {
            float distancia = Vector3.Distance(transform.position, caja.transform.position);

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                cajaMasCercana = caja;
            }
        }

        if (cajaMasCercana != null)
        {
            Vector3 direccion = cajaMasCercana.transform.position - transform.position;
            direccion.y = 0; // solo gira en horizontal

            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

}
