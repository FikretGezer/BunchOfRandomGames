using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TowerShooting : MonoBehaviour
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
    private float _elapsedTime;
    private bool enemyLocked;
    public bool isPlaced;
    private GameObject parentOfBullets;
    private Vector3 baseRotation;

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

        parentOfBullets = new GameObject();
        parentOfBullets.name = "Parent Of Bullets";

        baseRotation = transform.localRotation.eulerAngles;
        //_enemy = FindObjectOfType<Enemy>().transform;
    }

    private void Update() {
        if(_enemy == null)
        {
            if(Enemies._enemyList.Count > 0)
                _enemy = SelectCurrentEnemy(Enemies._enemyList);
        }
        else if(_enemy != null && isPlaced)
        {
            MoveTurret();
            if(CalculateTime(_bulletShootingRate) && enemyLocked) 
            {
                Shoot();
            }
        }
    }
    private void MoveTurret()
    {
        var shootingDirection = CalculateDirOfTurret();
        Quaternion lookRotation = Quaternion.LookRotation(shootingDirection);

        if(RotationLock(lookRotation))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.parent.rotation.eulerAngles), _lerpSpeed * Time.deltaTime);

            _enemy = null;
            _enemy = SelectCurrentEnemy(Enemies._enemyList);
            enemyLocked = false;
        } 
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _lerpSpeed * Time.deltaTime);     
            enemyLocked = true;
        }
    }
    private Vector3 CalculateDirOfTurret()
    {
        Vector3 dir = (_enemy.position + _enemy.forward * _moveAmount) - transform.position;
        dir = dir.normalized;
        return dir;
    }
    private bool RotationLock(Quaternion lookRot)
    {
        float yRotation = lookRot.eulerAngles.y;
        float yRotationOfParent = transform.parent.transform.rotation.eulerAngles.y;

        if(yRotation > 180f)
            yRotation -= 360f;
        if(yRotationOfParent > 180f)
            yRotationOfParent -= 360f;

        if(yRotation > 60f + yRotationOfParent || yRotation < -60f + yRotationOfParent) return true; //Cant move 
        else return false; //can move           
    }
    private void Shoot()
    {
        Vector3 shootingDirection = CalculateDirOfTurret();
        Bullet bullet = _bulletPool.Get();
        bullet.transform.parent = parentOfBullets.transform;
        bullet.KillBullet(ReleaseBullet);
        
        bullet.transform.position = _bulletInstantiateTransform.position;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * _force);      
    }
    private void ReleaseBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
   
    private Transform SelectCurrentEnemy(List<Enemy> enemies)
    {
        float CalculateDistance(Enemy enemy)
        {
            float distance = Vector3.Dot(transform.forward, enemy.transform.position);
            return distance;
        }

        Enemy current = enemies[0];       

        for (int i = 1; i < enemies.Count; i++)
        {
            if(CalculateDistance(enemies[i]) < CalculateDistance(current))
            {
                current = enemies[i];
            }
        }
        return current.transform;
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
}

