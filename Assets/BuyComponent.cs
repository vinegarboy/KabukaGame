using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyComponent : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    AudioSource audioSource;

    public void Buy(){
        int money = int.Parse(inputField.text);
        //購入のポストをして帰って来た値を元にifで分岐
        /*
        if(true){
            //通ったら表示を更新する。
        }else{
            //通らなかったらエラーとして音を鳴らす
            audioSource.Play();
        }
        */
    }
}
