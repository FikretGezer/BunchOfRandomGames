using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Image actualHealthBar;
    private Camera cam;
    private float healthAmount;
    private void OnEnable() {
        actualHealthBar = transform.GetChild(0).GetComponent<Image>();
        healthAmount = 1f;        
        actualHealthBar.fillAmount = healthAmount;
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
                actualHealthBar.fillAmount = Mathf.MoveTowards(actualHealthBar.fillAmount, health.healthAmount, 1f * Time.deltaTime);
            }
            else
            {
                CameraScript.isCamShaking = true;
                health.gameObject.SetActive(false);
            }
        }

        //Decrease Health When Something Happened
        
    }
    public void SetHealth(Health _health)
    {
        this.health = _health;
    }    
}
