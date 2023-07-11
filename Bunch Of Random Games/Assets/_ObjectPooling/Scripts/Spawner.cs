using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool _poolSwitch;
    [SerializeField] private Shape _shapePrefab;
    [SerializeField] private int _spawnAmount = 20;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxCapacity = 20;
    private ObjectPool<Shape> _pool;

    private void Start() {
        _pool = new ObjectPool<Shape>(() => {
            return Instantiate(_shapePrefab);
        }, shape => {
            shape.gameObject.SetActive(true);
        }, shape => {
            shape.gameObject.SetActive(false);
        }, shape => {
            Destroy(shape.gameObject);
        }, false, defaultCapacity, maxCapacity);

        InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);
    }
    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var shape = _poolSwitch ? _pool.Get() : Instantiate(_shapePrefab);
            shape.transform.position = transform.position + Random.insideUnitSphere * 10f;
            shape.Init(KillShape);
        }
    }
    private void KillShape(Shape shape)
    {
        if(_poolSwitch) _pool.Release(shape);
        else Destroy(shape.gameObject);
        
    }
}
