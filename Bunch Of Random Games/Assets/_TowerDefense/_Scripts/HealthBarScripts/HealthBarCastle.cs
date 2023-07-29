using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        actualHealthBar.fillAmount = 100f;
        _healthPercentage.text = $"%{castleHealthFillAmount * 100f:00}";
    }
    private void Update() {
        if(doesCastleGotHit)
        {
            castleHealth -= 10f;
            castleHealthFillAmount = castleHealth / maxHalthAmountOfCastle;
            //_healthPercentage.text =  "%" + (castleHealthFillAmount * 100f).ToString();
            _healthPercentage.text =  $"%{castleHealthFillAmount * 100f:00}";

            doesCastleGotHit = false;
        }
        if(actualHealthBar.fillAmount > 0.01f)
        {
            actualHealthBar.fillAmount = Mathf.MoveTowards(actualHealthBar.fillAmount, castleHealthFillAmount, 0.1f * Time.deltaTime);

            var isThereEnemy = FindObjectOfType<Enemy>() ? true : false;
            if(Enemies.Instance.currentEnemyCount >= Enemies.Instance.enemySpawnAmount && !isThereEnemy)
            {
                //All enemies killed, pass level
                var loadingScene = SceneManager.GetActiveScene().buildIndex + 1;
                if(loadingScene < SceneManager.sceneCountInBuildSettings)
                {
                    SaveScript.Instance.SaveLevel(SaveScript.Instance.GetLevel() + 1);
                    SceneManager.LoadScene(loadingScene);
                }
            }
        }
        else
        {
            CameraScript.isCamShaking = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }       
    }
}
