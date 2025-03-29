using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image energyUIBar;
    public int energy = 1000;
    public int maxEnergy = 1000;
    public int energyGenRate = 1;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) //DEBUG
        {
            DepleteEnergy(100);
        }
        if (Input.GetKeyDown(KeyCode.D)) //DEBUG
        {
            IncreaseEnergy(90);
        }
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(GenerateEnergy());
    }

    public void UpdateUI()
    {
        energyUIBar.fillAmount = (float)energy/maxEnergy;
    }

    public void DepleteEnergy(int val)
    {
        if (val > energy) return; //if the incoming depletion is less than the amount we have left

        energy = energy - val;
        UpdateUI();
    }

    public void IncreaseEnergy(int val)
    {
        if ((val + energy) > maxEnergy)
        {
            energy = maxEnergy;
        }
        else
        {
            energy = energy + val;
        }

        UpdateUI();
    }

    public bool EnergyCheck(int val)
    {
        if (val > energy)
        {
            return false;
        } 
        else 
        { 
            return true;
        }
    }

    IEnumerator GenerateEnergy()
    {
        while (true)
        {
            if (energy != maxEnergy)
            {
                IncreaseEnergy(energyGenRate);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
