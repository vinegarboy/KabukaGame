using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChatView : MonoBehaviour
{
    int[] chart1 = new int[9];
    int[] chart2 = new int[9];

    [SerializeField]
    LineRenderer chartRender1;

    [SerializeField]
    LineRenderer chartRender2;

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
        //上のコードを元に現在の値段をchartの入れて一つづずつらしていく

        //入れ終わったらchartRenderの各座標を更新する
        //y座標は+1する上に比率を利用しないと上に伸び続けるので注意
    }
}
