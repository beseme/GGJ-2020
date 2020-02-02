using System;
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
    
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _speedX = Random.Range(-10f, 10);
        _speedY = Random.Range(-10f, 10);
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
        _rig.velocity = new Vector2(_speedX, _speedY);
    }
}
