using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class BuyComponent : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    AudioSource audioSource;

    public void Buy(){
        int money = int.Parse(inputField.text);
        string requestBody = $"{{\"id\":\"{money}\"}}"; // リクエストボディの例

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("{ConnectionDataURL}BuyOrder", requestBody))
        {
            www.SendWebRequest();
            
            while (!www.isDone) { }
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
                audioSource.Play(); // エラーサウンドを再生
            }
            else
            {
                var jsonResponse = JsonUtility.FromJson<dynamic>(www.downloadHandler.text);
                int receivedCode = jsonResponse.code;

                if (receivedCode == 200)
                {
                    Debug.Log("Purchase successful.");
                    // 購入成功時の処理
                }
                else
                {
                    Debug.LogError("Purchase failed.");
                    audioSource.Play(); // エラーサウンドを再生
                }
            }
        }
    }
}
