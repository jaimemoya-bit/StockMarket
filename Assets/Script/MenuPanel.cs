using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    public TextMeshProUGUI TxtTitulo;
    public GameObject PanelMenu;
    public GameObject PanelInstrucciones;
    public GameObject CreditosPanel;
    
    void Start()
    { //mostrar nombre dl Jugador
        if(!string.IsNullOrEmpty(AuthManager.UserName))
        TxtTitulo.text = AuthManager.UserName;
        else
        {
            TxtTitulo.text = "Jugador";
        } 
    }
    //Boton para iniciar el juego
    public void OnClickJugar()
    {
        SceneManager.LoadScene("GameScene");
    }
    //panel de intrucciones
    public void OnClickInstrucciones()
    {
        PanelInstrucciones.SetActive(true);
        PanelMenu.SetActive(false);
       
    }
    public void OnClickRanking()
    {
        RankingScreen.escenaRetorno = "MenuScene";
        SceneManager.LoadScene("RankingScene");
    }
    public void OnClickcerrarSession()
    {
        AuthManager.CerrarSesion();
        SceneManager.LoadScene("LoginScene");
    }
    void OnEnable()
    {
        GameEvents.OnAbrirMenu += MostrarMenu;
    }
    void OnDisable()
    {
        GameEvents.OnAbrirMenu -= MostrarMenu;
    }
    public void MostrarMenu()

    {
        PanelMenu.SetActive(true);
        PanelInstrucciones.SetActive(false);
            CreditosPanel.SetActive(false);

    }
    public void OnclickEmpezar()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickCreditos()
    {
        CreditosPanel.SetActive(true);
        PanelMenu.SetActive(false);
    }
}
