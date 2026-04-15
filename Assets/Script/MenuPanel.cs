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
    public GameObject Panelranking;
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
        PanelMenu.SetActive(false);
        GameEvents.IniciarJuego();
    }
    //panel de intrucciones
    public void OnClickInstrucciones()
    {
        PanelInstrucciones.SetActive(true);
        PanelMenu.SetActive(false);
        Panelranking.SetActive(false);
    }
    public void OnClickRanking()
    {
        Panelranking.SetActive(true);
        PanelMenu.SetActive(false);
        PanelInstrucciones.SetActive(false);

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
    void MostrarMenu()

    {
        PanelMenu.SetActive(true);
        PanelInstrucciones.SetActive(false);
        Panelranking.SetActive(false);
    }
    public void OnclickEmpezar()
    {
        PanelMenu.SetActive(false);
        GameEvents.IniciarJuego();
    }
}
