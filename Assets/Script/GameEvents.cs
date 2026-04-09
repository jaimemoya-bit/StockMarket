using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class GameEvents
{
    public static event Action<float>    OnDineroChanged;
    public static event Action<float>    OnTiempoChanged;
    public static event Action<int>      OnNivelChanged;
    public static event Action<float>    OnSatisfaccionChanged;
    public static event Action<string[]> OnPedidoActualizado;
    public static event Action           OnRecoger;   
    public static event Action           OnCobrar;    
    public static event Action<float>    OnFinTurno;

    public static void CambiarDinero(float v)
    {
        if (OnDineroChanged != null) OnDineroChanged(v);
    }

    public static void CambiarTiempo(float v)
    {
        if (OnTiempoChanged != null) OnTiempoChanged(v);
    }

    public static void CambiarNivel(int v)
    {
        if (OnNivelChanged != null) OnNivelChanged(v);
    }

    public static void CambiarSatisfaccion(float v)
    {
        if (OnSatisfaccionChanged != null) OnSatisfaccionChanged(v);
    }

    public static void NuevoPedido(string[] prods)
    {
        if (OnPedidoActualizado != null) OnPedidoActualizado(prods);
    }

    public static void PulsarRecoger()
    {
        if (OnRecoger != null) OnRecoger();
    }

    public static void PulsarCobrar()
    {
        if (OnCobrar != null) OnCobrar();
    }
    public static void FinTurno(float dineroFinal)
    {
        if (OnFinTurno != null) OnFinTurno(dineroFinal);
    }
}