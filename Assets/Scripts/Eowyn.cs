using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eowyn : MonoBehaviour
{
    private Animator _animator;
    private bool _defending = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_defending && (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Z)))
            Shield();
        else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.X))
            Attack();
        else if (!_defending && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z)))
            Shield();
    }

    void Attack()
    {
        _defending = false;
        _animator.SetBool("Shield", _defending);
        _animator.SetTrigger("Attack");
    }

    void Shield()
    {
        _defending = !_defending;
        _animator.SetBool("Shield", _defending);
    }
}
