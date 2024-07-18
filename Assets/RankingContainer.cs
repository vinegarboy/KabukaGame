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
        InvokeRepeating(nameof(UpdateRanking),1f, 1f); // 1秒後に最初に実行し、その後1秒ごとに繰り返す
    }

    void StopUpdating(){
        CancelInvoke(nameof(UpdateRanking));
    }


    async UniTask<bool> UpdateRanking()
    {
        var r = UnityWebRequest.Get($"{ConnectionData.URL}Ranking");
        var result = await r.SendWebRequest();
        while (!result.isDone) { }
        if (result.result != UnityWebRequest.Result.Success){
            Debug.Log(r.error);
            return false;
        }
        string[] rankingData = result.downloadHandler.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
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
        return true;
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
