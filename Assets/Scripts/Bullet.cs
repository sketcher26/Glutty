﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb.velocity = transform.right * speed; 
    }

    void Update()
    {
        
    }
}