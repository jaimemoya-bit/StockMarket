using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavegacionAuth : MonoBehaviour
{
    public GameObject panelLogin;
    public GameObject panelRegistro;
    void Start()
    {
     MostrarLogin();   
    }
    // Función para mostrar el panel de login y ocultar el de registro
    public void MostrarLogin()
    {
        panelLogin.SetActive(true);
        panelRegistro.SetActive(false);
    }
    public void MostrarRegistro()
    {
        panelLogin.SetActive(false);
        panelRegistro.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
