using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingEnemies : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private Towers _tower;
    [SerializeField] private Transform _bulletInstantiateTransform;
    [SerializeField] private float _force = 1f;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private float _moveAmount = 2f;
    
    private ObjectPool<Bullet> _bulletPool;
    private Bullet _bulletPrefab;
    private float _bulletShootingRate;//Reversed higher means lower shooting rate
    private float _bulletDamageAmount;
    private float _elapsedTime;
    private bool enemyLocked;
    public bool isPlaced;
    private GameObject parentOfBullets;
    private Vector3 baseRotation;
    private float previousDistance = 100f;

    private void Awake() {

        _bulletPool = new ObjectPool<Bullet>(() => {
            return Instantiate(_bulletPrefab);
        }, bullet => {
            bullet.gameObject.SetActive(true);            
        }, bullet => {
            bullet.gameObject.SetActive(false);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }, bullet => {
            Destroy(bullet.gameObject);
        }, false, 5, 40);

        _bulletPrefab = _tower.ammoType;
        _bulletShootingRate = _tower.towerShootingRate;
        _bulletDamageAmount = _tower.damageAmount;

        parentOfBullets = GameObject.FindGameObjectWithTag("ParentOfBullets");

        parentOfBullets.name = "Parent Of Bullets";

        baseRotation = transform.localRotation.eulerAngles;
    }


    private void Update() {
        if(_enemy == null)
        {
            if(Enemies._enemyList.Count > 0)
            {
                _enemy = SelectCurrentEnemy(Enemies._enemyList);
            }
        }
        else
        {   
            if(isPlaced)
            {
                MoveTurret();
                if(CalculateTime(_bulletShootingRate) && enemyLocked)
                {
                    Shoot();
                }
            }
            
        }
        
    }
    private void MoveTurret()
    {
        var dir = CalculateDirOfTurret();
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        var canMove = RotationLocked(lookRotation);
        if(!_enemy.gameObject.activeSelf)
        {
            _enemy = null;        
            enemyLocked = false;
            canMove = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.parent.rotation.eulerAngles), _lerpSpeed * Time.deltaTime);          
        }
        if(canMove)
        {
            enemyLocked = true;
            lookRotation.x = transform.rotation.x;
            lookRotation.z = transform.rotation.z;
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _lerpSpeed * Time.deltaTime);
        }
        else 
        {
            _enemy = null;        
            enemyLocked = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.parent.rotation.eulerAngles), _lerpSpeed * Time.deltaTime);
        }

        if(_enemy != null && CalculateDistance(_enemy.GetComponent<Enemy>()) > 10f)
        {
            _enemy = null;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.parent.rotation.eulerAngles), _lerpSpeed * Time.deltaTime);
        }
        
    }
    private void Shoot()
    {
        Vector3 shootingDirection = CalculateDirOfTurret();
        Bullet bullet = _bulletPool.Get();
        bullet.transform.parent = parentOfBullets.transform;
        bullet.KillBullet(ReleaseBullet);
        bullet.GiveDamage(_bulletDamageAmount);
            
        bullet.transform.position = _bulletInstantiateTransform.position;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * _force); 
            
    }
    private float CalculateDistance(Enemy enemy)
    {
        float distance = Vector3.Distance(transform.parent.position, enemy.transform.position);
        return distance;
    }
    private Transform SelectCurrentEnemy(List<Enemy> enemies)
    {

        Enemy currentSelectedEnemy = enemies[0];   

        for (int i = 1; i < enemies.Count; i++)
        {
            if(CalculateDistance(enemies[i]) < CalculateDistance(enemies[i-1]))
            {
                currentSelectedEnemy = enemies[i];
            }
        }
        enemyLocked = true;
        return currentSelectedEnemy.transform;
    }
    public float multiplier = 1f;
    private Vector3 CalculateDirOfTurret()
    {
        Vector3 dir = default;
        var enemyChild = _enemy.transform.GetChild(0);
        if(enemyChild != null)
        {
            dir = (enemyChild.position + enemyChild.forward * _moveAmount) - transform.position;
            dir = dir.normalized;
        }
        else
        {
            dir = (_enemy.position + _enemy.forward * _moveAmount) - transform.position;
            dir = dir.normalized;
        }
        return dir;
    }
    private bool RotationLocked(Quaternion lookRot)
    {
        float yRotation = lookRot.eulerAngles.y;
        float yRotationOfParent = transform.parent.transform.rotation.eulerAngles.y;

        if(yRotation > 180f)
            yRotation -= 360f;
        if(yRotationOfParent > 180f)
            yRotationOfParent -= 360f;
        
        float maxRotation = yRotationOfParent + 60f;
        float minRotation = yRotationOfParent - 60f;
        if (yRotation > maxRotation || yRotation < minRotation)
            return false; // Can move
        else
            return true; // Can't move 
            
    }
    private bool CalculateTime(float maxTime)
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > maxTime)
        {
            _elapsedTime = 0f;
            return true;
        }             
        else return false;   
    }
    private void ReleaseBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
}