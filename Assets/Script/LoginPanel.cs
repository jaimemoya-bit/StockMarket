using UnityEngine;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField  inputEmail;
    public TMP_InputField  inputPassword;
    public TextMeshProUGUI txtError;

    private AuthManager _auth;

    void Start()
    {
        _auth = FindObjectOfType<AuthManager>();
        if (txtError != null)
        {
            txtError.text = "";
            txtError.gameObject.SetActive(false);
        }
    }

    public void OnClickLogin()
    {
        if (txtError != null)
        {
            txtError.text = "";
            txtError.gameObject.SetActive(false);
        }

        string email = inputEmail.text.Trim();
        string pass  = inputPassword.text;

        // Validar campos vacíos
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
        {
            MostrarError("Rellena todos los campos");
            return;
        }

        // Validar formato email
        if (!Validador.EmailValido(email))
        {
            MostrarError("El email no es válido");
            return;
        }

        // Validar longitud contraseña
        if (!Validador.PassValida(pass))
        {
            MostrarError("La contraseña debe tener mínimo 6 caracteres");
            return;
        }


        _ = StartCoroutine(_auth.Login(
            email,
            pass,
            onSucess: (message) =>
            {
                Debug.Log("Login OK. Usuario: " + AuthManager.UserName);
                UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            },
            onError: (msg) =>
            {
                MostrarError(msg);
            }
        ));
    }

    void MostrarError(string msg)
    {
        if (txtError != null)
        {
            txtError.text = msg;
            txtError.gameObject.SetActive(true);
        }
    }
}