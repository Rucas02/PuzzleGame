using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsGrounded => _isGrounded;
    [SerializeField] private bool _isGrounded;

    public bool IsInteracting => _isInteracting;
    [SerializeField] private bool _isInteracting;

    private void Update()
    {
        var transform1 = transform;
        _isGrounded = Physics.Raycast(transform1.position, -transform1.up, out RaycastHit hit, 0.25f);
        
    }

    public void SetIsInteracting(bool isInteracting) => _isInteracting = isInteracting;
}
