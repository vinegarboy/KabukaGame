using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserDataContainer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nametext;
    void Update()
    {
        nametext.text = $"Name:{UserData.UserName}";
    }
}
