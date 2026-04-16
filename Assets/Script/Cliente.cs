using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Cliente : MonoBehaviour
{
    public mirarcaja mirar;
    //Variable de tipo GameManager para poder llamar al metodo AnadirSatis()
    private GameManager gameManager;

    //Para poder llamar al gestor de cola ColaManager
    [SerializeField] private ColaManager cola;

    //Variable para medir la satisfaccion del cliente
    public int clientSatis;
    private int maxSatis = 10;

    //Variable para el intervalo de tiempo en el que pierde cada punto
    private float intervalPerd = 2f;

    //Variable para indicar que el cliente est� en la caja
    //(De momento hardcodeado en true)
    private bool enCaja;

    //Booleano para indicar que el cliente se esta marchando;
    private bool irseDeTienda;

    //Elemento de Interfaz indicando satisfaccion
    //De momento se asigna mediante editor
    [SerializeField] private TextMeshProUGUI clientSatisText;

    //Punto de destino para cuando se marcha (Puerta)
    //Se asigna en editor
    [SerializeField] private Transform puertaTrigger;

    //Array de posiciones para las estanterias(gameObjects vacios)
    //Se asigna en editor
    [SerializeField] private Transform[] estantTrigger;


    //Booleano para saber si est� de camino a caja
    private bool caminoACaja;

    //NavMeshAgent del cliente
    private NavMeshAgent agent;

    //Awake se llama antes de que empiece el juego
    void Awake()
    {
        // Obtiene el componente NavMeshAgent GameObject cliente
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enCaja = false;
        irseDeTienda = false;

        //Asignamos el objeto GameManager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //Se asigna la cola con menos clientes
        cola = AsignarCola();
        //El cliente comienza con 10 puntos de satisfaccion
        clientSatis = maxSatis;
        

        // Cuando el NPC aparece, autom�ticamente va a la caja (temporal)
        // Se a�ade el cliente al sistema de cola del ColaManager
        cola.ACola(this);


    }

    // Update is called once per frame
    void Update()
    {
        // si ya esta en caja no seguir comprobando
        if (enCaja) return;

        // Si a�n est� calculando el path no hacemos nada
        if (agent.pathPending) return;

        // Si la distancia restante es menor que la distancia de parada
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Si ya no tiene path o est� parado completamente y est� de camino a caja, no yendose
            if (!agent.hasPath && caminoACaja && !irseDeTienda && cola.EsPrimero(this) || agent.velocity.sqrMagnitude == 0f && caminoACaja && !irseDeTienda)
            {
                // El cliente lleg� a la caja
                LLegadaACaja();
                
            }

            /*Si no est� de camino a caja y esta de camino a marcharse
             * el cliente se destruye al marcharse de la tienda
             */
            if (!agent.hasPath && irseDeTienda && !caminoACaja || agent.velocity.sqrMagnitude == 0f && irseDeTienda && !caminoACaja)
            {
                Destroy(gameObject);
            }
        }        
      

    }

    //Cuenta atras, el cliente pierde satisfaccion segun pasa el tiempo
    IEnumerator PerdidaSatis()
    {
        //Asignamos interfaz
        clientSatisText.text = "" + clientSatis;
        //Activamos elemento de interfaz
        clientSatisText.gameObject.SetActive(true);

        while (clientSatis > 0)
        {

            yield return new WaitForSeconds(intervalPerd);

            clientSatis--;
            // evitar negativos
            clientSatis = Mathf.Clamp(clientSatis, 0, maxSatis);
            //Actualiza la interfaz
            clientSatisText.text = ""+ clientSatis;

        }
        Debug.Log("Lleg� a 0");
        Marcharse();

    }

    /*
     * On Click (Placeholder) se a�ade la satisfacci�n del cliente a la 
     * general
     */
    private void OnMouseDown()
    {
        gameManager.AnadirSatis(clientSatis);
        //Se desactiva el elemento de interfaz
        clientSatisText.gameObject.SetActive(false);
        clientSatis = 10;
        Destroy(gameObject);
        //Iniciamos la cuenta atras de nuevo
        //TO DO: eliminar esto cuando se instancie cada cliente
        StartCoroutine(PerdidaSatis());
    }

    

    // Funci�n p�blica para enviar el cliente a la caja
    public void MoverseACaja(Transform posicion)
    {
        agent.isStopped = false;
        caminoACaja = true;
        // Setea el destino del pathfinding
        // El NavMeshAgent calcula autom�ticamente la ruta
        
            agent.SetDestination(posicion.position);
        Debug.Log("Cliente se mueven a caja");

    }

    // Funci�n cuando el cliente llega a la caja
    void LLegadaACaja()
    {
        // Evita que se siga llamando cada frame
        agent.isStopped = true;
        enCaja = true;
        mirar.enabled=true;
        
        Debug.Log("Cliente lleg� a la caja");

        //Iniciamos la perdida de satisfaccion
        StartCoroutine(PerdidaSatis());
    }

    // Funcion publica para que el cliente se marche de la tienda
    public void Marcharse()
    {
        caminoACaja = false;
        agent.isStopped = false;
        enCaja = false;
        agent.SetDestination(puertaTrigger.position);
        irseDeTienda = true;
        Debug.Log("El cliente se macrha");
        cola.SalirCola(this);
    }

    //Metodo para asignar cola al cliente
    //Para referenciar metodos del ColaManager
    ColaManager AsignarCola()
    {
        ColaManager[] colas = FindObjectsOfType<ColaManager>();

        ColaManager best = colas[0];
        int min = best.ConteoCola();

        foreach (var c in colas)
        {
            if( c.ConteoCola() < min)
            {
                best = c;
                min = c.ConteoCola();
            }
        }
        return best;
    }
}
