using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;

    private string adUnityId;

    private void Awake()
    {
#if UNITY_IOS
adUnityId = iosAdUnitID
#elif UNITY_ANDROID
        adUnityId = androidAdUnitID;
#endif

    }

    public void ShowRewardAd()
    {
        Advertisement.Show(adUnityId, this);
        
    }

    public void LoadRewardAd()
    {
        Advertisement.Load(adUnityId, this);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == adUnityId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            //Grant Player rewards here!!!!!!!!!
            Debug.Log("Player Reward");
        }
    }
}
