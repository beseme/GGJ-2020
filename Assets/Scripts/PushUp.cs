﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PushUp : Actor
{
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _jumpHeight = 0;
    
    private bool[] _side = {false, false, false, false}; //left 0; right 1; up 2; down 3
    
    private PlayerController _control = null;

    new void Awake()
    {
        base.Awake();
        _control = GetComponent<PlayerController>();
    }
    
    private void FixedUpdate()
    {
        if(_side[0])
            this.MoveX(-_speed * Time.fixedDeltaTime);
        else if(_side[1])
            this.MoveX(_speed * Time.fixedDeltaTime);
        else if(_side[2])
            this.MoveY(_jumpHeight * Time.fixedDeltaTime);
        else if(_side[3])
            this.MoveY(32 * Physics2D.gravity.y * Time.fixedDeltaTime);
    }

    public void Push(int side)
    {
        Debug.Log("works");
        //_control.enabled = false;
        _side[side] = true;
    }

    public void Finish()
    {
        for (int i = 0; i < _side.Length; i++)
        {
            _side[i] = false;
        }
       // _control.enabled = true;
    }
}
