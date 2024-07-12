using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleComponent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    public void StartGame(){
        if(inputField.text == ""){
            UserData.UserName = $"Guest{Random.Range(1000, 10000)}";
        }else{
            UserData.UserName = inputField.text;
        }
        // POSTについて https://beyondjapan.com/blog/2020/05/unitywebrequest/
        //ユーザーIDはPOSTした内容を代入する
        UserData.UserID = "";
        // ゲームを開始する
        SceneManager.LoadScene("PlayingScene");
    }
}
