using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;  // Required for AWSCredentials
using System;
using System.Collections.Generic;
using UnityEngine;

public class AWSManager : MonoBehaviour
{
    [SerializeField] string _cognitoUserPoolID;   // Your Cognito User Pool ID
    [SerializeField] string _cognitoUserAppClient; // Your Cognito App Client ID
    [SerializeField] string _identityPoolID;      // Your Cognito Identity Pool ID

    static public AWSManager instance;

    private AmazonCognitoIdentityProviderClient _providerClient;
    private CognitoUserPool _userPool;
    private CognitoAWSCredentials _cognitoCredentials;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CognitoStart();
        }
        else
        {
            Destroy(this);
        }
    }

    #region Cognito
    private void CognitoStart()
    {
        // Initialize the Amazon Cognito Identity Provider client using Anonymous credentials for registration
        _providerClient = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.CACentral1);

        // Initialize the Cognito User Pool
        _userPool = new CognitoUserPool(_cognitoUserPoolID, _cognitoUserAppClient, _providerClient);

        // Initialize CognitoAWSCredentials (only needed after user is authenticated)
        _cognitoCredentials = new CognitoAWSCredentials(_identityPoolID, RegionEndpoint.CACentral1);
    }

    public async void RegisterUser(string username, string password, string email)
    {
        try
        {
            // Create sign-up request
            var signUpRequest = new SignUpRequest
            {
                ClientId = _cognitoUserAppClient,
                Username = username,
                Password = password,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = email }
                }
            };

            // Send request to Cognito User Pool for registration
            var signUpResponse = await _providerClient.SignUpAsync(signUpRequest);

            if (signUpResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.Log("User registered successfully! Please check your email for verification.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during registration: {ex.Message}");
        }
    }

    public void AuthenticateUser(string username, string password)
    {
        var user = new CognitoUser(username, _cognitoUserAppClient, _userPool, _providerClient);
        var authRequest = new InitiateSrpAuthRequest
        {
            Password = password
        };

        user.StartWithSrpAuthAsync(authRequest).ContinueWith(task =>
        {
            if (task.Exception == null)
            {
                Debug.Log("User authenticated successfully!");

                // Add login to CognitoAWSCredentials
                _cognitoCredentials.AddLogin(
                    $"cognito-idp.{RegionEndpoint.CACentral1.SystemName}.amazonaws.com/{_cognitoUserPoolID}",
                    user.SessionTokens.IdToken
                );

                // Now, the _cognitoCredentials object can be used to access AWS services.
                Debug.Log("AWS Credentials are now available for authenticated access.");
            }
            else
            {
                Debug.LogError($"Error during authentication: {task.Exception.InnerException.Message}");
            }
        });
    }
    #endregion
}
