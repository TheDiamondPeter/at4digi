using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Wolfram : MonoBehaviour
{
    public TMP_Text resultText; // UI Text to display the result
    private string apiKey = "62TK42-53H8Y7AGQA";

    public async void ProcessEquation(string equation)
    {
        if (string.IsNullOrEmpty(equation))
        {
            Debug.LogError("Equation is empty. Cannot process.");
            return;
        }

        string baseUrl = "https://api.wolframalpha.com/v1/result?appid=" + apiKey + "&i=" + UnityWebRequest.EscapeURL(equation);
        string response = await getRequest(baseUrl);

        if (resultText != null)
        {
            resultText.text = "Solution: " + response; // Display result
        }
        Debug.Log("Wolfram Response: " + response);
    }

    private async Task<string> getRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
                return request.downloadHandler.text;
            else
                return $"Error: {request.error}";
        }
    }
}
