using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class KeysText : MonoBehaviour
{
    private int keyInt = 1;
    private float textCd = 3f;
    private float timer = 0f;
    private bool textShown = false;
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().SetText(new StringBuilder(""));
    }
    void Update()
    {
        if (textShown)
        {
            timer += Time.deltaTime;
            if (timer >= textCd)
            {
                timer = 0f;
                GetComponent<TextMeshProUGUI>().SetText(new StringBuilder(""));
                textShown = false;
            }
        }
            
    }
    public void ShowText()
    {
        
        GetComponent<TextMeshProUGUI>().SetText(new StringBuilder("You need " + keyInt.ToString() + " key(s) to open this door!"));
        textShown = true;
    }

    public void SetAmount(int keys)
    {
        keyInt = keys;
    }


}
