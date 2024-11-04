using System.Collections;
using ServerCommunicationUtilities;
using UnityEngine;
using UnityEngine.Networking;

public class ServerEndpointsEnum
{
    public static string
        BASE_URL = "http://localhost:5005",
        HAND_DATA = "/hand-data",
        END_TRAINING = "/save-model",
        HELLO_WORLD = "/hello-world";
};

public class ServerGateway : MonoBehaviour
{
    [SerializeField]
    private string serverUrl;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HelloWorld(System.Action<string> callback)
    {
        StartCoroutine(ExecRequest(serverUrl + ServerEndpointsEnum.HELLO_WORLD, "GET", "{}", callback));
    }

    public void SendHandData(RequestWrapper requestWrapper, System.Action<string> callback)
    {
        StartCoroutine(ExecRequest(serverUrl + ServerEndpointsEnum.HAND_DATA, "POST", JsonUtility.ToJson(requestWrapper), callback));
    }

    public void EndTraining(System.Action<string> callback)
    {
        StartCoroutine(ExecRequest(serverUrl + ServerEndpointsEnum.END_TRAINING, "GET", "{}", callback));
    }

    IEnumerator ExecRequest(string uri, string method, string jsonData, System.Action<string> callback)
    {
        UnityWebRequest webRequest = new UnityWebRequest(uri, method);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.DataProcessingError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.downloadHandler.text);
            callback?.Invoke(webRequest.downloadHandler.text);
        }
        else
        {
            callback?.Invoke(webRequest.downloadHandler.text);
        }
    }

}
