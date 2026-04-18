using UnityEngine;

[RequireComponent(typeof(RankingAPI))]
[RequireComponent(typeof(ProgresoApi))]
public class GameManager : MonoBehaviour
{
    [Header("Configuracion")]
    public float tiempototal = 120f;
    public int nivelInicial = 1;

    public float dinero;
    public float tiempoRestante;
    public int nivel;
    public float satisfaccion;
    public bool jugando;
    private ProgresoApi _progresoApi;

    void Start()
    {
        jugando = false;

        _progresoApi = GetComponent<ProgresoApi>();
        GameEvents.OnIniciarJuego += IniciarJuego;
        GameEvents.OnRecoger      += OnRecoger;
        GameEvents.OnCobrar       += OnCobrar;

        // Solo inicializa valores para el HUD, no arranca el timer
        tiempoRestante = tiempototal;
        dinero         = 0f;
        satisfaccion   = 1f;
        nivel          = nivelInicial;
        BroadcastEstado();
        IniciarJuego();
    }

    void OnDestroy()
    {
        GameEvents.OnIniciarJuego -= IniciarJuego;
        GameEvents.OnRecoger      -= OnRecoger;
        GameEvents.OnCobrar       -= OnCobrar;
    }

    void Update()
    {
        if (!jugando) return;

        tiempoRestante = Mathf.Max(0, tiempoRestante - Time.deltaTime);
        GameEvents.CambiarTiempo(tiempoRestante);

        if (tiempoRestante == 0) { jugando = false; FindeTurno(); }

        if (Input.GetKeyDown(KeyCode.R))
            foreach (var e in RankingAPI.GetTop10Falso())
                Debug.Log($"{e.rank}. {e.userName} → {e.score}");
    }

    // Emite todos los valores al HUD de una vez
    void BroadcastEstado()
    {
        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarTiempo(tiempoRestante);
        GameEvents.CambiarNivel(nivel);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    // Suma satisfacción, la clampea y notifica
    void AgregarSatisfaccion(float delta)
    {
        satisfaccion = Mathf.Clamp01(satisfaccion + delta);
        GameEvents.CambiarDinero(dinero);
        GameEvents.CambiarSatisfaccion(satisfaccion);
    }

    void IniciarJuego()
    {
        jugando        = true;
        tiempoRestante = tiempototal;
        dinero         = 0f;
        satisfaccion   = 1f;
        nivel          = nivelInicial;
        BroadcastEstado();
    }

    public void OnRecoger() => AgregarSatisfaccion(0.1f);

    public void OnCobrar()
    {
        dinero += 150f;
        AgregarSatisfaccion(0.05f);
    }

    void FindeTurno()
    {
        GameEvents.FinTurno(dinero);
        StartCoroutine(_progresoApi.GuardarProgreso(dinero,
            onSuccess: () => Debug.Log("Progreso guardado"),
            onError:   (e) => Debug.LogWarning("Error al guardar: " + e)
        ));
    }

    void GuardarRanking() =>
        StartCoroutine(_progresoApi.GuardarRanking(nivel,
            onSuccess: () => Debug.Log("Ranking guardado"),
            onError:   (e) => Debug.LogWarning("Error ranking: " + e)
        ));

    public void SubirNivel()
    {
        nivel++;
        GameEvents.CambiarNivel(nivel);
    }
}
