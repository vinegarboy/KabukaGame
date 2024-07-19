using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static Cysharp.Threading.Tasks.UniTask;
using TMPro;
using System.Linq;

public class RankingContainer : MonoBehaviour
{
    public GameObject[] ViewObjects;
    void Start(){
        for (int i = 0; i < ViewObjects.Length; i++){
            ViewObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Name: None";
            ViewObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Score: -1";
        }
        //InvokeRepeating(nameof(UpdateRanking),1f, 1f); // 1秒後に最初に実行し、その後1秒ごとに繰り返す
    }

    void StopUpdating(){
        //CancelInvoke(nameof(UpdateRanking));
    }


    void Update(){
        var r = UnityWebRequest.Get($"{ConnectionData.URL}Ranking");
        var result = r.SendWebRequest();
        while (!r.isDone) { }
        for (int i = 0; i < ViewObjects.Length; i++){
            ViewObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Name: None";
            ViewObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Score: -1";
        }
        if(r.result != UnityWebRequest.Result.Success){
            Debug.Log(r.error);
            return;
        }
        string[] rankingData = r.downloadHandler.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        List<PlayerInfo> players = new List<PlayerInfo>();

        foreach (var data in rankingData){
            var player = JsonUtility.FromJson<PlayerInfo>(data);
            players.Add(player);
        }

        // 上位6人のデータを抽出
        var topSixPlayers = players.OrderByDescending(p => p.coinA + p.coinB).Take(6);

        // 上位6人のデータを表示する処理
        var topSixPlayersList = topSixPlayers.ToList();
        for (int i = 0; i < topSixPlayersList.Count(); i++){
            ViewObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Name: {topSixPlayersList[i].Name}";
            ViewObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Coins: {topSixPlayersList[i].coinA + topSixPlayersList[i].coinB}";
        }
    }
}

[System.Serializable]
public class PlayerInfo
{
    public string Name;
    public string id;
    public int coinA;
    public int coinB;
}
