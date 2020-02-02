using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class WhiteGoo : Actor
{
    private float _speedX = 0;
    private float _speedY = 0;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _speedX = Random.Range(1, 10);
        _speedY = Random.Range(1, 10);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        this.MoveX(_speedX * Time.fixedDeltaTime);
        this.MoveY(_speedY * Time.fixedDeltaTime);
    }
}
