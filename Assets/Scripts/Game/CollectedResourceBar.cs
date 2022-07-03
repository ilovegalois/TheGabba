using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedResourceBar : MonoBehaviour
{
    public TextMeshProUGUI cash;
    public TextMeshProUGUI meth;
    

    int cashOnPlayer;
    int methOnPlayer;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cash.text = RoundManager.roundCash.ToString();
        meth.text = RoundManager.roundMeth.ToString();
    }
}
