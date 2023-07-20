using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCastle : MonoBehaviour
{
    [SerializeField] private float maxHalthAmountOfCastle;
    [SerializeField] private Image actualHealthBar;
    private float castleHealthFillAmount;
    private float castleHealth;
    [HideInInspector] public bool doesCastleGotHit;
    private void Awake() {
        castleHealth = maxHalthAmountOfCastle;
        castleHealthFillAmount = castleHealth / maxHalthAmountOfCastle;
        actualHealthBar.fillAmount = castleHealthFillAmount;
    }
    private void Update() {
        if(doesCastleGotHit)
        {
            castleHealth -= 5f;
            castleHealthFillAmount = castleHealth / maxHalthAmountOfCastle;

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
