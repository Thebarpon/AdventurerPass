using TMPro;
using UnityEngine;

public class AWSClientRegister : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _emailConfirmation;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _passwordConfirmation;

    public void OnRegisterClick()
    {
        if (EmailConfirmation() && PasswordConfirmation())
        {
            AWSManager.instance.RegisterUser(_username.text, _password.text, _email.text);
        }
    }

    private bool EmailConfirmation()
    {
        if (_email.text == _emailConfirmation.text)
        {
            return true;
        }
        else
        {
            Debug.Log("Email confirmation doesn't match with email");
            return false;
        }
    }
    private bool PasswordConfirmation()
    {
        if (_password.text == _passwordConfirmation.text)
        {
            return true;
        }
        else
        {
            Debug.Log("Password confirmation doesn't match with email");
            return false;
        }
    }
}
