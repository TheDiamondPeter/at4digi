using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    // Assign these in the Inspector with your TextMeshPro Input Fields
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    
    // TextMeshPro Text component to show feedback
    public TMP_Text feedbackText;

    // Set your correct username and password here
    public string correctUsername = "admin";
    public string correctPassword = "password123";

    // Call this method from a UI button's OnClick event
    public void CheckLogin()
    {
        if (usernameField.text == correctUsername && passwordField.text == correctPassword)
        {
            // Load the main menu scene with build index 2
            SceneManager.LoadScene(1);
        }
        else
        {
            // Display error message and clear input fields
            feedbackText.text = "try again.";
            usernameField.text = "";
            passwordField.text = "";
            // Start a coroutine to clear the error message after 3 seconds
            StartCoroutine(ClearErrorMessage());
        }
    }

    private IEnumerator ClearErrorMessage()
    {
        yield return new WaitForSeconds(3f);
        feedbackText.text = "";
    }
}