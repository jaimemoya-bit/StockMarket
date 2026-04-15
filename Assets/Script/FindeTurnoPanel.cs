using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;


public class FindeTurnoPanel : MonoBehaviour
{

    //Declaracion de variables
    public TextMeshProUGUI Txtdinero;
    public TextMeshProUGUI Txtnivel;
    public TextMeshProUGUI TxtSatisfaccion;
    public TextMeshProUGUI TxtEstado;
    public GameObject PanelFinTurno;
    public RankingScreen PanelRanking;

    void Start()
    {
        PanelFinTurno.SetActive(false);
        GameEvents.OnFinTurno += UpdatePanel;
    }

    void OnDisable()
    {
        GameEvents.OnFinTurno -= UpdatePanel;
    }

    void UpdatePanel(float dinero)
    {
        PanelFinTurno.SetActive(true);
        //Actualizar el panel con la información del juego
        Txtdinero.text = "Dinero: $" + dinero.ToString("N0");
        TxtSatisfaccion.text = "Satisfacción: " +
                               Mathf.RoundToInt(dinero / 50f) + "%";
        Txtnivel.text = "Nivel: " + PlayerPrefs.GetInt("Nivel", 1);
        TxtEstado.text = "Guardando progreso...";
        //Cambiar estado cuando guarde
        StartCoroutine(Esperandoguardado());

    }
    System.Collections.IEnumerator Esperandoguardado()
    {
        yield return new WaitForSeconds(2f);
        TxtEstado.text = "Estado: Guardado";
    }
    public void OnClickVerRanking()
    {
        //Mostrar el ranking
        PanelRanking.AbrirRanking();
            
    }
    public void OnClickJugarDeNuevo()
    {
        //Reiniciar el juego
        SceneManager.LoadScene("GameScene");
    }
}
