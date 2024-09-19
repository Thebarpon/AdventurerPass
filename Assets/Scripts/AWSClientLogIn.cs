using TMPro;
using UnityEngine;

public class AWSClientLogIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;

    public void OnLogInClick()
    {
        AWSManager.instance.AuthenticateUser(_username.text, _password.text);
    }
}
