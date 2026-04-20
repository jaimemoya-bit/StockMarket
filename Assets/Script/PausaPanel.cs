using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausaPanel : MonoBehaviour
{
    public GameObject panelPausa;
    public Button btnPausa;

    void Start()
    {
        panelPausa.SetActive(false);
        GameEvents.OnFinTurno += OnFinTurno;
    }

    void OnDestroy() => GameEvents.OnFinTurno -= OnFinTurno;

    void OnFinTurno(float _) { if (btnPausa != null) btnPausa.interactable = false; }

    public void Pausar()
    {
        Debug.Log("Pausar llamado");
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Continuar()
    {
        Debug.Log("Continuar llamado");
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    public void CerrarSesion()
    {
        Time.timeScale = 1f;
        AuthManager.CerrarSesion();
        SceneManager.LoadScene("LoginScene");
    }
}