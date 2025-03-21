using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Wolfram : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public async Task<string> UseWolframAsync() {
        string apiKey = "62TK42-53H8Y7AGQA"; 
        string baseUrl = "http://api.wolframalpha.com/v1/result?appid=" + apiKey + "&i=";

        //return getRequest("http:///www.yoururl.com");
        string response = await getRequest(baseUrl);
        Debug.Log(response);
        return response;
    }
    private async Task<string> getRequest(string uri) {
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