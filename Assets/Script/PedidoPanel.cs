using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedidoPanel : MonoBehaviour
{
    public Transform contenedor;
    public GameObject itemPrefab;

    void OnEnable()
    {
        GameEvents.OnPedidoActualizado += Mostrar;
    }

    void OnDisable()
    {
        GameEvents.OnPedidoActualizado -= Mostrar;
    }

    void Mostrar(string[] productos)
    {
        foreach (Transform h in contenedor)
            Destroy(h.gameObject);

        foreach (string p in productos)
        {
            GameObject obj = Instantiate(itemPrefab, contenedor);
            obj.GetComponent<ItemPedido>().Init(p);
        }
    }
}
