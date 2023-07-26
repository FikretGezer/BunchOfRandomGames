using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarCastle : MonoBehaviour
{
    [SerializeField] private Image actualHealthBar;
    [SerializeField] private TMP_Text _healthPercentage;
    public static float maxHalthAmountOfCastle;
    public static float castleHealthFillAmount;
    private float castleHealth;
    [HideInInspector] public bool doesCastleGotHit;
    private void Awake() {
        castleHealth = maxHalthAmountOfCastle;
        castleHealthFillAmount = castleHealth / maxHalthAmountOfCastle;
        actualHealthBar.fillAmount = castleHealthFillAmount;
        _healthPercentage.text = $"%{castleHealthFillAmount * 100f:00}";
    }
    private void Update() {
        if(doesCastleGotHit)
        {
            castleHealth -= 5f;
            castleHealthFillAmount = castleHealth / maxHalthAmountOfCastle;
            //_healthPercentage.text =  "%" + (castleHealthFillAmount * 100f).ToString();
            _healthPercentage.text =  $"%{castleHealthFillAmount * 100f:00}";

            doesCastleGotHit = false;
        }
        if(actualHealthBar.fillAmount > 0.01f)
        {
            actualHealthBar.fillAmount = Mathf.MoveTowards(actualHealthBar.fillAmount, castleHealthFillAmount, 0.1f * Time.deltaTime);
        }
        else
        {
            CameraScript.isCamShaking = true;
            
        }
               
    }
}
