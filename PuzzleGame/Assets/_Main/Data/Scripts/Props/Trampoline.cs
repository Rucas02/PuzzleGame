using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Prop
{
    private float _currentForce;
    [SerializeField] private float force = 200f;

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        Player player = other.GetComponent<Player>();
        
        if (player)
        {
            if (player.Data.ID == "PSR")
                _currentForce = force * 2.5f;
            else if (player.Data.ID == "PPM")
                _currentForce = force / 2;
            else
                _currentForce = force;
        }
        else
        {
            _currentForce = force;
        }
        
        other.GetComponent<Rigidbody>().AddForce(transform.up * _currentForce * Time.deltaTime, ForceMode.Impulse);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.SetIsInteracting(true);
        }
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.SetIsInteracting(false);
        }
    }
}