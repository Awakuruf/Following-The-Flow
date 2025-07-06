using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class RAGBotManager : MonoBehaviour
{
    public OnResponseEvent OnResponse;

    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string>
    {

    }
    private List<ChatMessage> messages = new List<ChatMessage>();
    public TMP_Text responseText;
    private void Start()
    {
        OnResponse.AddListener(UpdateUI);
    }

    private void UpdateUI(string response)
    {
        if (responseText != null)
        {
            responseText.text = response;
        }
    }

    public async void AskRAGAi(string newText)
    {
        ChatMessage newMessage = new ChatMessage
        {
            Content = newText,
            Role = "user"
        };
        messages.Add(newMessage);

        string url = "http://127.0.0.1:8000/chat";
        ChatInput input = new ChatInput { message = newText };
        string jsonData = JsonUtility.ToJson(input);

        Debug.Log($"[RAGBot] JSON Payload: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            Debug.Log($"[RAGBot] Sending POST request to {url}");

            while (!operation.isDone) await Task.Yield();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
            if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError("Chat request failed: " + request.error);
                OnResponse.Invoke("Sorry, I couldn’t reach the agent.");
            }
            else
            {
                // Debug.Log("[RAGBot] Request successful");
                // Debug.Log($"[RAGBot] Status Code: {request.responseCode}");
                // Debug.Log($"[RAGBot] Raw Response: {request.downloadHandler.text}");

                string responseJson = request.downloadHandler.text;
                AIResponse response = JsonUtility.FromJson<AIResponse>(responseJson);

                // Debug.Log($"[RAGBot] Parsed AI response: {response.response}");

                ChatMessage chatResponse = new ChatMessage
                {
                    Content = response.response,
                    Role = "assistant"
                };
                messages.Add(chatResponse);

                Debug.Log(chatResponse.Content);
                OnResponse.Invoke(chatResponse.Content);
            }
        }
    }

    //     public async void AskRAGAi(string newText)
    //     {
    //         Debug.Log($"[RAGBot] Preparing request for input: {newText}");

    //         ChatMessage newMessage = new ChatMessage
    //         {
    //             Content = newText,
    //             Role = "user"
    //         };
    //         messages.Add(newMessage);

    //         string url = "http://127.0.0.1:8000/chat";
    //         ChatInput input = new ChatInput { message = newText };
    //         string jsonData = JsonUtility.ToJson(input);

    //         Debug.Log($"[RAGBot] JSON Payload: {jsonData}");

    //         using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
    //         {
    //             byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
    //             request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //             request.downloadHandler = new DownloadHandlerBuffer();
    //             request.SetRequestHeader("Content-Type", "application/json");

    //             Debug.Log($"[RAGBot] Sending POST request to {url}");

    //             var operation = request.SendWebRequest();

    //             while (!operation.isDone) await Task.Yield();

    // #if UNITY_2020_1_OR_NEWER
    //             if (request.result != UnityWebRequest.Result.Success)
    // #else
    //         if (request.isNetworkError || request.isHttpError)
    // #endif
    //             {
    //                 Debug.LogError($"[RAGBot] Request failed! Error: {request.error}");
    //                 OnResponse.Invoke("Sorry, I couldn’t reach the agent.");
    //             }
    //             else
    //             {
    //                 Debug.Log("[RAGBot] Request successful");
    //                 Debug.Log($"[RAGBot] Status Code: {request.responseCode}");
    //                 Debug.Log($"[RAGBot] Raw Response: {request.downloadHandler.text}");

    //                 string responseJson = request.downloadHandler.text;
    //                 AIResponse response;

    //                 try
    //                 {
    //                     response = JsonUtility.FromJson<AIResponse>(responseJson);
    //                     Debug.Log($"[RAGBot] Parsed AI response: {response.response}");
    //                 }
    //                 catch (System.Exception e)
    //                 {
    //                     Debug.LogError($"[RAGBot] Failed to parse response JSON. Error: {e.Message}");
    //                     OnResponse.Invoke("Sorry, I couldn’t understand the agent’s reply.");
    //                     return;
    //                 }

    //                 ChatMessage chatResponse = new ChatMessage
    //                 {
    //                     Content = response.response,
    //                     Role = "assistant"
    //                 };
    //                 messages.Add(chatResponse);

    //                 OnResponse.Invoke(chatResponse.Content);
    //             }
    //         }
    //     }

    [System.Serializable]
    private class ChatInput
    {
        public string message;
    }

    [System.Serializable]
    private class AIResponse
    {
        public string response;
    }

}
