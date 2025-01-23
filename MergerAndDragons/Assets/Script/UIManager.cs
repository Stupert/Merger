using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text descriptorText;

    public void UpdateText(string descrition)
    {
        descriptorText.text = descrition;
    }

    public void ClearText()
    {
        descriptorText.text = null;
    }
}
