using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteSpawner : MonoBehaviour
{
    // Prefab del NPC cliente
    [Header("Prefab cliente")]
    [SerializeField] private GameObject customerPrefab;


    // Donde aparecen los clientes
    [Header("Punto de spawn")]
    [SerializeField] private Transform spawnPoint;

    // cada cuantos segundos aparece un cliente
    [Header("Tiempo spawn")]
    [SerializeField] private float spawnInterval = 6f;

    // empezar automáticamente
    [SerializeField] private bool spawnOnStart = true;

    // numero maximo de clientes
    private int maxCustomers = 9;

    public int clientesActual;

    void Start()
    {
        // si está activado, iniciar spawner
        if (spawnOnStart)
            StartCoroutine(SpawnRoutine());
    }
    void SpawnCustomer()
    {
        if (FindObjectsOfType<Cliente>().Length >= maxCustomers)
        {
            return;
        }

        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // esperar tiempo entre spawns
            yield return new WaitForSeconds(spawnInterval);

            SpawnCustomer();
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        clientesActual = FindObjectsOfType<Cliente>().Length;
    }
}
