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


    // Bandera para reabrir el panel al volver del Ranking
    public static bool volverDeRanking = false;
    private static float ultimoDinero = 0f;

    void Start()
    {
        GameEvents.OnFinTurno += UpdatePanel;

        if (volverDeRanking)
        {
            volverDeRanking = false;
            UpdatePanel(ultimoDinero);
        }
        else
        {
            PanelFinTurno.SetActive(false);
        }
    }

    void OnDisable()
    {
        GameEvents.OnFinTurno -= UpdatePanel;
    }

    void UpdatePanel(float dinero)
    {
        ultimoDinero = dinero;
        PanelFinTurno.SetActive(true);
        //Actualizar el panel con la información del juego
        Txtdinero.text = "Dinero: $" + dinero.ToString("N0");
        TxtSatisfaccion.text = "Satisfaccion: " +
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
        volverDeRanking = true;
        RankingScreen.escenaRetorno = "GameScene";
        SceneManager.LoadScene("RankingScene");
    }
    public void OnClickJugarDeNuevo()
    {
        //Reiniciar el juego
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickMenuPrincipal()
    {
        //Volver al menu principal
        SceneManager.LoadScene("MenuScene");
    }
}
