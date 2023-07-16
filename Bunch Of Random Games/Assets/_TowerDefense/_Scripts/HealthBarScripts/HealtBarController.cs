using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBarController : MonoBehaviour
{
    #region  Singleton
    private static HealtBarController instance;
    public static HealtBarController Instance{
        get{
            if(instance == null)
                instance = new HealtBarController();
            return instance;
        }
    }
    #endregion
    [SerializeField] private HealthBar healthBarPrefab;
    
    public Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();
    private void OnEnable() {
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }
    private void OnDisable() {
        Health.OnHealthAdded -= AddHealthBar;
        Health.OnHealthRemoved -= RemoveHealthBar;
    }
    private void AddHealthBar(Health _health)
    {
        if(!healthBars.ContainsKey(_health))
        {
            var _healthBar = Instantiate(healthBarPrefab, transform);
            healthBars.Add(_health, _healthBar);
            _healthBar.SetHealth(_health);
        }
        if(healthBars.ContainsKey(_health))
        {
            if(healthBars[_health] != null)
                healthBars[_health].gameObject.SetActive(true);
        }
    }
    private void RemoveHealthBar(Health _health)
    {
        if(healthBars.ContainsKey(_health))
        {
            if(healthBars[_health] != null)
                healthBars[_health].gameObject.SetActive(false);
        }        
    }
}
