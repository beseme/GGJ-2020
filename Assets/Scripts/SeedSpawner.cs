using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner _confiner = null;
    [SerializeField] private GameObject _seed = null;
    [SerializeField] private float _waveCooldown = 0;
    [SerializeField] private float _spawnAmount = 0;

    private float _waveCD = 0;
    private float _spawn = 0;

    private void Awake()
    {
        _spawnAmount /= 60;
        _waveCD = _waveCooldown;
        _spawn = _spawnAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnAmount >= 0)
        {
            int spawnDirection = Random.Range(1, 2);
            float positiveOffest =
                Random.Range(_confiner.m_BoundingVolume.bounds.min.y, _confiner.m_BoundingVolume.bounds.max.y);
            float negativeOffest =
                -Random.Range(_confiner.m_BoundingVolume.bounds.min.y, _confiner.m_BoundingVolume.bounds.max.y);
            if (spawnDirection == 1)
                Instantiate(_seed, (Vector2) _confiner.m_BoundingVolume.bounds.min + Vector2.up * positiveOffest,
                    Quaternion.identity);
            else if (spawnDirection == 1)
                Instantiate(_seed, (Vector2) _confiner.m_BoundingVolume.bounds.max + Vector2.up * negativeOffest,
                    Quaternion.identity);

            _waveCD = _waveCooldown;
        }
        else if (_waveCD <= 0)
        {
            _spawn = _spawnAmount;
        }

        _spawn -= Time.deltaTime;
        _waveCD -= Time.deltaTime;

    }
}
