using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public RewardAds rewardAds;

    public static AdManager instance { get; private set; }

    [SerializeField] GameObject adPanel;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        adPanel.SetActive(false);
    }

    public void OpenAdPanel()
    {
        if (!adPanel.activeInHierarchy)
        {
            rewardAds.LoadAd();
            adPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Ad panel already active");
        }
    }

    public void CloseAdPanel()
    {
        if (adPanel.activeInHierarchy)
        {
            adPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Ad panel already deactive");
        }
    }

    public void PlayAd()
    {
        instance.PlayAd();
        CloseAdPanel();
    }
    
}
