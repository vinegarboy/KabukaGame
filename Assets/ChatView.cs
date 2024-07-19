using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ChatView : MonoBehaviour
{
    float[] chart1 = new float[9];
    float[] chart2 = new float[9];

    [SerializeField]
    LineRenderer chartRender1;

    [SerializeField]
    LineRenderer chartRender2;

    [SerializeField]
    TextMeshProUGUI price1;

    [SerializeField]
    TextMeshProUGUI price2;

    void Start(){
        for(int i = 0;i<9;i++){
            chart1[i] = 0;
            chart2[i] = 0;
        }
        // chart1の各要素をY軸の値としてLineRendererに設定
        // y軸のレンジは1~3なので割合で調整する
        for (int i = 0; i < chart1.Length; i++){
            chartRender1.SetPosition(i, new Vector3(-8+(float)(i*0.5), chart1[i]+1, 0));
            chartRender2.SetPosition(i, new Vector3(-3+(float)(i*0.5), chart2[i]+1, 0));
        }
    }
    void Update()
    {
        float atob_Rate;
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost:5280/AtoBRate")){
            www.SendWebRequest();
            while (!www.isDone) { }
            if (www.result != UnityWebRequest.Result.Success){
                Debug.LogError(www.error);
            }
            Debug.Log(www.downloadHandler.text);
            atob_Rate = float.Parse(www.downloadHandler.text);
        }
        price1.text = "AtoB:"+atob_Rate+"B";
        price2.text = "BtoA:"+1/atob_Rate+"A";
        chart1[0] = atob_Rate/100;
        chart2[0] = 1/atob_Rate * 100;
        for(int i = 0;i<chart1.Length-1;i++){
            chart1[i+1] = chart1[i];
            chart2[i+1] = chart2[i];
        }

        for (int i = 0; i < chart1.Length; i++){
            chartRender1.SetPosition(i, new Vector3(-8+(float)(i*0.5), chart1[i]+1, 0));
            chartRender2.SetPosition(i, new Vector3(-3+(float)(i*0.5), chart2[i]+1, 0));
        }
        //上のコードを元に現在の値段をchartの入れて一つづずつらしていく

        //入れ終わったらchartRenderの各座標を更新する
        //y座標は+1する上に比率を利用しないと上に伸び続けるので注意
    }
}
