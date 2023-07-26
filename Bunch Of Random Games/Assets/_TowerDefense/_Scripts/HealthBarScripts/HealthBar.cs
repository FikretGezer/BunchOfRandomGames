using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Image actualHealthBar;
    private Camera cam;
    private void OnEnable() {
        actualHealthBar = transform.GetChild(0).GetComponent<Image>();      
        actualHealthBar.fillAmount = 1f;
    }
    private void Awake() {
        cam = Camera.main;
    }
    private void Update() {
        if(health != null)
        {
            transform.position = cam.WorldToScreenPoint(health.transform.position);//Add UI Elements on World

            if(actualHealthBar.fillAmount > 0.01f)
            {
                var hAmount = health._enemyScript.healthAmount;
                var maxHealth = health._enemyScript.maxHealthAmount;
                var actualHAmount = hAmount / maxHealth;
                actualHealthBar.fillAmount = Mathf.MoveTowards(actualHealthBar.fillAmount, actualHAmount, 1f * Time.deltaTime);
            }
            else
            {
                CameraScript.isCamShaking = true;
                health.gameObject.SetActive(false);
            }
        }        
    }
    public void SetHealth(Health _health)
    {
        this.health = _health;
    }    
}
