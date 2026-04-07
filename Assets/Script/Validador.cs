using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Validador
{
    public static bool EmailValido(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    public static bool PassValida(string pass)
    {
        return pass.Length >= 6;
    }

    public static bool UsuarioValido(string usuario)
    {
        return usuario.Length >= 3;
    }
}

