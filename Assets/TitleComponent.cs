using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        using (UnityWebRequest www = UnityWebRequest.Post("URL入れておこう", "{ \"UserName\": "+UserData.UserName+"}", "application/json"))
        {
            string data = www.SendWebRequest().ToString();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            return data;
        }
    }
}
