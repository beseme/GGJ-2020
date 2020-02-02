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

    private bool _spawning = true;
    private void Awake()
    {
        _spawnAmount /= 60;
        _waveCD = _waveCooldown;
        _spawn = _spawnAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawning)
        {
            int spawnDirection = Random.Range(1, 2);
            float positiveOffest =
                Random.Range(1, _confiner.m_BoundingVolume.bounds.max.y - _confiner.m_BoundingVolume.bounds.min.y);
            float negativeOffest =
                -Random.Range(1, _confiner.m_BoundingVolume.bounds.max.y - _confiner.m_BoundingVolume.bounds.min.y);
            if (spawnDirection == 1)
                Instantiate(_seed, (Vector2) _confiner.m_BoundingVolume.bounds.min + Vector2.up * positiveOffest,
                    Quaternion.identity);
            else if (spawnDirection == 1)
                Instantiate(_seed, (Vector2) _confiner.m_BoundingVolume.bounds.max + Vector2.up * negativeOffest,
                    Quaternion.identity);

            _spawn -= Time.deltaTime;
            if (_spawn <= 0)
            {
                _spawning = false;
                _spawn = _spawnAmount;
            }
        }
        else if (!_spawning)
        {
            _waveCD -= Time.deltaTime;
            if (_waveCD <= 0)
            {
                _spawning = true;
                _waveCD = _waveCooldown;
            }
        }
    }
}
