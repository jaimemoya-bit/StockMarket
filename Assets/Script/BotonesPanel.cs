using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonoesPanel : MonoBehaviour
{ public Button btnRecoger;
    public Button btnCobrar;

    void Start()
    {
        btnRecoger.onClick.AddListener(OnRecoger);
        btnCobrar.onClick.AddListener(OnCobrar);
    }

    void OnDestroy()
    {
        btnRecoger.onClick.RemoveListener(OnRecoger);
        btnCobrar.onClick.RemoveListener(OnCobrar);
    }

    void OnRecoger()
    {
        Debug.Log("Boton Recoger pulsado");
        GameEvents.PulsarRecoger();
    }

    void OnCobrar()
    {
        Debug.Log("Boton Cobrar pulsado");
        GameEvents.PulsarCobrar();
    }
}
