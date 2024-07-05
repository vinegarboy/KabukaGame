using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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

    async UniTask<bool> GetMyAssets(){
        var r = UnityWebRequest.Get("");
        var result = await r.SendWebRequest();
        if (!result.isDone){
            Debug.Log(r.error);
            return false;
        }
        string[] assets_data = result.downloadHandler.text.Split(',');
        for(int i = 0; i < assets_data.Length; i++){
            AssetsObjects[i].text = assets_data[i];
        }
        return true;
    }
}
