using UnityEngine;
using Firebase.Auth;
using TMPro;

public class LoginManager : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("Login correcto");
            }
            else
            {
                Debug.Log("Error en login");
            }
        });
    }
}