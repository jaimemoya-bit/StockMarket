using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegistroPanel : MonoBehaviour
{
    public TMPro.TMP_InputField inputUsuario;   
    public TMPro.TMP_InputField inputEmail;
    public TMPro.TMP_InputField inputPass;
    public TMPro.TMP_InputField inputPass2;
    public TextMeshProUGUI TxtError;

    private AuthManager authManager;
    private NavegacionAuth navegacionAuth;

    
    
    // Start is called before the first frame update
    void Start()
    {
        authManager = FindObjectOfType<AuthManager>();
        navegacionAuth = FindObjectOfType<NavegacionAuth>();

        if (authManager == null)
            Debug.LogError("RegistroPanel: AuthManager no encontrado en la escena.");
        if (navegacionAuth == null)
            Debug.LogError("RegistroPanel: NavegacionAuth no encontrado en la escena.");
        if (TxtError == null)
        {
            Debug.LogError("RegistroPanel: TxtError no está asignado en el Inspector.");
        }
        else
        {
            TxtError.text = "";
            TxtError.gameObject.SetActive(false);
        }
    }
    public void OnClickRegister()
    {
        Debug.Log("RegistroPanel: OnClickRegister ejecutado.");

        if (inputUsuario == null || inputEmail == null || inputPass == null || inputPass2 == null)
        {
            Debug.LogError("RegistroPanel: uno o más campos de input no están asignados en el Inspector.");
            return;
        }

        string name = inputUsuario.text.Trim();
        string email = inputEmail.text.Trim();
        string password = inputPass.text.Trim();
        string password2 = inputPass2.text.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2))
        {
            if (TxtError != null)
            {
                TxtError.text = "Por favor, completa todos los campos.";
                TxtError.gameObject.SetActive(true);
            }
            else
                Debug.LogWarning("RegistroPanel: no se puede mostrar error porque TxtError es null.");
            return;
        }
        if (!Validador.UsuarioValido(name))
        {
            if (TxtError != null)
            {
                TxtError.text = "El nombre de usuario debe tener al menos 3 caracteres.";
                TxtError.gameObject.SetActive(true);
            }
            else
                Debug.LogWarning("RegistroPanel: no se puede mostrar error porque TxtError es null.");
            return;
        }
        if (!Validador.EmailValido(email))
        {
            if (TxtError != null)
            {
                TxtError.text = "El email no es válido.";
                TxtError.gameObject.SetActive(true);
            }
            else
                Debug.LogWarning("RegistroPanel: no se puede mostrar error porque TxtError es null.");
            return;
        }
        if (!Validador.PassValida(password))
        {
            if (TxtError != null)
            {
                TxtError.text = "La contraseña debe tener al menos 6 caracteres.";
                TxtError.gameObject.SetActive(true);
            }
            else
                Debug.LogWarning("RegistroPanel: no se puede mostrar error porque TxtError es null.");
            return;
        }

        if (password != password2)
        {
            if (TxtError != null)
            {
                TxtError.text = "Las contraseñas no coinciden.";
                TxtError.gameObject.SetActive(true);
            }
            else
                Debug.LogWarning("RegistroPanel: no se puede mostrar error porque TxtError es null.");
            return;
        }

        if (authManager == null)
        {
            Debug.LogError("RegistroPanel: authManager es null, no se puede iniciar el registro.");
            if (TxtError != null)
            {
                TxtError.text = "Error interno: authManager no encontrado.";
                TxtError.gameObject.SetActive(true);
            }
            return;
        }

        StartCoroutine(authManager.Register(name, email, password,
            onSucess: (message) => {
                Debug.Log("Registro exitoso");
                if (TxtError != null)
                {
                    TxtError.text = "";
                    TxtError.gameObject.SetActive(false);
                }
                navegacionAuth.MostrarLogin(); // Volver al panel de login después del registro exitoso
            },
            onError: (errorMessage) => {
                Debug.LogError("RegistroPanel error: " + errorMessage);
                if (TxtError != null)
                {
                    TxtError.text = errorMessage;
                    TxtError.gameObject.SetActive(true);
                }
                else
                    Debug.LogWarning("RegistroPanel: error recibido pero TxtError es null.");
            }
        ));
     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
