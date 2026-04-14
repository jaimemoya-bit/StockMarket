using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteSpawner : MonoBehaviour
{
    [Header("Prefab cliente")]
    [SerializeField] private GameObject customerPrefab;
    // Prefab del NPC cliente

    // Donde aparecen los clientes
    [Header("Punto de spawn")]
    [SerializeField] private Transform spawnPoint;
    

    //manager de cola
    [Header("Sistema de cola")]
    [SerializeField] private ColaManager colaManager;
    

    [Header("Tiempo spawn")]
    [SerializeField] private float spawnInterval = 6f;
    // cada cuantos segundos aparece un cliente

    [SerializeField] private bool spawnOnStart = true;
    // empezar automáticamente

    void Start()
    {
        // si está activado, iniciar spawner
        if (spawnOnStart)
            StartCoroutine(SpawnRoutine());
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

    void SpawnCustomer()
    {
        // comprobar si la cola está llena
        if (colaManager.ColaLlena())
        {
            Debug.Log("Cola llena, no spawn");
            return;
        }

        // instanciar cliente
        GameObject customer = Instantiate(
            customerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // asignar queue manager al cliente
        Cliente npc = customer.GetComponent<Cliente>();

        // por seguridad comprobar que existe
        if (npc != null)
        {
            // asignar referencia
            // (solo si tu variable es pública o SerializeField)
            npc.SetQueue(colaManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
