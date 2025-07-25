using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public RewardAds rewardAds;
    public TimeController timeController;
    public static AdManager instance { get; private set; }

    float refreshAdsOn;

    [SerializeField] GameObject adPanel;
    private void Awake()
    {
        if (instance != null && instance != this)
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
        CloseAdPanel();
        instance.PlayAd();
    }
    
    public int GetRemainingAds()
    {
        int value;
        value = rewardAds.adBoostsLeft;
        return value;
    }

    public void LoadAdsRemaining(int remainingAdValue, double adRefreshDate)
    {
        double currentDate = Mathf.Floor((float)timeController.GetTime() / 86400) ;

        Debug.Log($"{currentDate} || {adRefreshDate}");

        if (currentDate >= adRefreshDate) //if you have passed the date 1 day after watching your first ad
        {
            Debug.Log("loading all ads");
            rewardAds.adBoostsLeft = rewardAds.totalAdBoost; //then refresh all remaining ads
        }
        else
        {
            Debug.Log("loading remaining ads");
            rewardAds.adBoostsLeft = remainingAdValue; //if you are stil on that day, then loaded the saved amount of ads left
        }
        rewardAds.UpdateUI();    
    }

    public double RefreshDate()
    {
        double time;
        time = timeController.GetTime();
        refreshAdsOn = Mathf.Floor((float)time / 86400) + 1;
        return (double)refreshAdsOn;
    }
}
