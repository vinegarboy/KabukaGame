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

    public void Buy(){
        int money = int.Parse(inputField.text);
        
    }
}
