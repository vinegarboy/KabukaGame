using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TitleComponent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    public void StartGame(){
        if(inputField.text == ""){
            UserData.UserName = $"Guest{UnityEngine.Random.Range(1000, 10000)}";
        }else{
            UserData.UserName = inputField.text;
        }
        // POSTについて https://beyondjapan.com/blog/2020/05/unitywebrequest/
        //ユーザーIDはPOSTした内容を代入する
        UserData.UserID = PostNameData();
        // ゲームを開始する
        SceneManager.LoadScene("PlayingScene");
    }

    // ユーザー名をPOSTする
    string PostNameData()
    {
        Debug.Log($"{ConnectionData.URL}Register");
        using (UnityWebRequest www = UnityWebRequest.Post($"{ConnectionData.URL}Register", "{ \"UserName\": "+UserData.UserName+"}", "application/json"))
        {
            www.SendWebRequest();
            while (!www.isDone) { }
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{www.error}\n{www.downloadHandler.text}");
                return null;
            }
            RegisterResponse response = JsonUtility.FromJson<RegisterResponse>(www.downloadHandler.text);

            // idフィールドの値を返す
            return response.id;
        }
    }
}

[System.Serializable]
public class RegisterResponse{
    public int code;
    public string id;
    public string message;
}
