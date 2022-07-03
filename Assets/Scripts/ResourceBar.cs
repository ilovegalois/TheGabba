using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    public TextMeshProUGUI cash;
    public TextMeshProUGUI meth;
    // Start is called before the first frame update
    void Start()
    {
        cash.text = DataController.cashAmount.ToString();
        meth.text = DataController.methAmount.ToString();
    }

    // Update is called once per frame
}
