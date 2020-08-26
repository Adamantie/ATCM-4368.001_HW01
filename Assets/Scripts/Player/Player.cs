﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    //TODO offload health into a Health.cs script
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    int _currentTreasure;

    public bool _invincibility = false;

    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        ProcessMovement();  
    }

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if (_invincibility == false)
        {
            _currentHealth -= amount;
            Debug.Log("Player's health: " + _currentHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
        else
        {
            Debug.Log("Invincible");
        }
        
    }

    public void IncreaseTreasure(int amount)
    {
        _currentTreasure += amount;
        Debug.Log("Player's treasure: " + _currentTreasure);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        //play particles
        //play sounds
    }
}
