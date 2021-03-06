﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class WhiteGoo : MonoBehaviour
{
    private float _speedX = 0;
    private float _speedY = 0;
    private Rigidbody2D _rig = null;
    private float _lifetime = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _speedX = Random.Range(-5f, 5);
        _speedY = Random.Range(-1f, 1);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if(_lifetime < 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rig.velocity = new Vector2(_speedX, _speedY);
    }
}
