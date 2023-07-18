using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] _towers;
    [SerializeField] private float gridPerAxis = 0.25f;
    [SerializeField] private LayerMask _ground;

    private GameObject _spawnedTower;
    private GameObject _selectedPrefab;

    private Camera _cam;
    private void Awake() {
        _cam = Camera.main;        
    }
    private void Update() {
        
        if(_spawnedTower != null)
        {
           // Debug.Log(_spawnedTower.transform.GetChild(0).localRotation.eulerAngles);
            Raycasting();
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateSpawnedTower();
            }
            if(Input.GetMouseButtonDown(0)){
                _spawnedTower.transform.GetChild(0).GetComponent<ShootingEnemies>().isPlaced = true;
                _spawnedTower = null;
            }
        }
        else
        {
            WhichTower();
            SpawnTower();
        }
    }
    private void Raycasting()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ground))
        {
            var point = hit.point;
            _spawnedTower.transform.position = new Vector3(RecalculatePosition(point.x), point.y + 1f, RecalculatePosition(point.z));            
        }
    }
    private void RotateSpawnedTower()
    {
        var rot = _spawnedTower.transform.rotation.eulerAngles;
        rot.y += 90f;
        _spawnedTower.transform.rotation = Quaternion.Euler(rot);
    }
    private void SpawnTower()
    {
        if(_spawnedTower == null && _selectedPrefab != null)
        {
            _spawnedTower = Instantiate(_selectedPrefab, transform.position, Quaternion.identity);
            _selectedPrefab = null;
        }
    }
    private void WhichTower()
    {
        if(_spawnedTower == null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                _selectedPrefab = _towers[0];
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                _selectedPrefab = _towers[1];
            }
        }
    }
    private float RecalculatePosition(float axis)
    {
        float kalan = axis % gridPerAxis;
        float result = axis - kalan; 
        if(axis > gridPerAxis / 2)
        {
            result += gridPerAxis;
        }
        return result;
    }
}
