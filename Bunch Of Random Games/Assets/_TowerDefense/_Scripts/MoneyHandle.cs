using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyHandle : MonoBehaviour
{
    public static MoneyHandle Instance;
    [SerializeField] private TMP_Text _moneyTextHolder;
    [SerializeField] public int _towerMoney = 1000;
    [Header("Towers' Prizes")]
    [SerializeField] private int firstTowerPrize, secondTowerPrize;
    public bool canSpawnIt;

    private void Awake() {
        _moneyTextHolder.text = _towerMoney.ToString();
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Update() {
        // if(Input.GetKeyDown(KeyCode.C)){
        //     ChangeMoney(0);
        // }        
    }
    public void ChangeMoney(int i)
    {
        int decreasedMoney = _towerMoney;
        canSpawnIt = true;
        switch(i)
        {
            case 0: 
                decreasedMoney -= firstTowerPrize;
                break;
            case 1: 
                decreasedMoney -= secondTowerPrize;
                break;
            
        }
        if(decreasedMoney < 0)
        {
            canSpawnIt = false;
            return;
        }
        _towerMoney = decreasedMoney;
        if(_moneyTextHolder != null)
            _moneyTextHolder.text = _towerMoney.ToString();
    }
}
