using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class AssetsGetter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] AssetsObjects;
    void Start(){
        InvokeRepeating(nameof(GetMyAssets),1f, 1f); // 1秒後に最初に実行し、その後1秒ごとに繰り返す
    }

    void StopUpdating(){
        CancelInvoke(nameof(GetMyAssets));
    }

    public bool GetMyAssets(){
        //URLが確定したらコレでGET通信ができる
        //ユーザーIDはUserData.UserIDで取得できる
        using (UnityWebRequest www = UnityWebRequest.Get($"{ConnectionData.URL}GetUserData?userId={UserData.UserID}")){
            www.SendWebRequest();
            while (!www.isDone) { }
            if (www.result != UnityWebRequest.Result.Success){
                Debug.LogError(www.error);
            }
            // JSONをデシリアライズするためのオブジェクトを作成
            var coinA = www.downloadHandler.text.Split(",")[0];
            var coinB = www.downloadHandler.text.Split(",")[1];

            // CoinAとCoinBの値をAssetsObjectsに設定
            AssetsObjects[0].text = "CoinA:"+coinA;
            AssetsObjects[1].text = "CoinB:"+coinB;

            return true;
        }
    }
}

[System.Serializable]
public class Message
{
    public string Name;
    public string id;
    public int coinA;
    public int coinB;
}
