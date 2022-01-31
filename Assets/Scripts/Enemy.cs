using System;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AIPath aIPath;
    private Animator _animator;
    private AIDestinationSetter _aiDestinationSetter;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _aiDestinationSetter = FindObjectOfType<AIDestinationSetter>();
        
        _aiDestinationSetter.target = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        _animator.SetFloat("Horizontal", aIPath.desiredVelocity.x);
        _animator.SetFloat("Vertical", aIPath.desiredVelocity.y);
          
    }
}
