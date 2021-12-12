using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eowyn : MonoBehaviour
{
    private Animator _animator;
    private bool _defending = false;
    private bool _hasShield = true;

    private Entity _entity;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _entity = GetComponent<Entity>();
        _entity.onEnemyAttack += OnEnemyAttack;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_defending && (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Z)))
            Shield();
        else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.X))
            Attack();
        else if (!_defending && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z)))
            Shield();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            if (_rb.velocity.y == 0)
                _rb.AddForce(Vector2.up * 1000f);

        _entity.SetMovement(Input.GetAxisRaw("Horizontal"));
    }

    void Attack()
    {
        _defending = false;
        _animator.SetBool("Shield", _defending);
        _animator.SetTrigger("Attack");
        _entity.Attack();
    }

    void Shield()
    {
        _defending = !_defending;
        _animator.SetBool("Shield", _defending);
    }

    void OnEnemyAttack(float strength)
    {
        bool dies = false;
        if (!_defending || !_hasShield)
            dies = _entity.TakeDamage(strength);
        else
            dies = _entity.TakeDamage(Random.Range(0, strength / 3));
        if (dies)
        {
            Destroy(gameObject);
        }
    }
}
