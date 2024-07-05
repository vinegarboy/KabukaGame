using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static Cysharp.Threading.Tasks.UniTask;
using TMPro;

public class RankingContainer : MonoBehaviour
{
    public GameObject[] ViewObjects;
    void Update(){
        var task = UpdateRanking();
        task.Forget();
    }

    async UniTask<bool> UpdateRanking(){
        var r = UnityWebRequest.Get("");
        var result = await r.SendWebRequest();
        if (!result.isDone){
            Debug.Log(r.error);
            return false;
        }
        string[] ranking_data = result.downloadHandler.text.Split(',');
        for(int i = 0; i < ranking_data.Length; i+=2){
            ViewObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ranking_data[i];
            ViewObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ranking_data[i+1];
        }
        return true;
    }
}
