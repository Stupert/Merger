using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelStateMachine : MonoBehaviour
{
    public enum UIState
    {
        Gameplay,
        Ad
    }

    public UIState state;
    [SerializeField] private GameObject adUIElement;
    public RewardAds rewardAds;

    public void ChangeState(UIState _state)
    {
        LeaveState(state);
        switch (_state)
        {
            case UIState.Gameplay:
                StateStart(_state);
                return;
            case UIState.Ad:
                StateStart(_state);
                return;

        }
    }

    public void ChangeState(int _state)
    {
        ChangeState((UIState)_state);
    }

    private void StateStart(UIState _state)
    {
        switch (_state)
        {
            case UIState.Gameplay:
                state = UIState.Gameplay;
                return;
            case UIState.Ad:
                state = UIState.Ad;
                rewardAds.LoadAd();
                adUIElement.active = true;
                return;

        }
    }

    public void LeaveState(UIState _state)
    {
        switch (_state)
        {
            case UIState.Gameplay:
                return;
            case UIState.Ad:
                adUIElement.active = false;
                return;
        }
    }
}
