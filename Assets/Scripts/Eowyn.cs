using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eowyn : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _defending = false;

    [SerializeField]
    private float _offset = 2f;
    [SerializeField]
    private float _attackRange = 0.5f;
    [SerializeField]
    private LayerMask _layerMask;

    private float _movement = 0f;
    private bool _facingRight = true;

    [SerializeField]
    private float _speed = 2f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        _movement = Input.GetAxisRaw("Horizontal") * _speed;
        if ((_movement < 0 && _facingRight) || (_movement > 0 && !_facingRight))
        {
            _facingRight = !_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        if (_movement != 0)
            _rb.MovePosition(transform.position + _movement * _speed * transform.right * Time.fixedDeltaTime);
    }

    void Attack()
    {
        _defending = false;
        _animator.SetBool("Shield", _defending);
        _animator.SetTrigger("Attack");

        Vector2 attackPoint = transform.position + transform.localScale.x * transform.right * _offset;
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint, _attackRange, _layerMask);
        if (enemy)
            Debug.Log("HIT");
    }

    void Shield()
    {
        _defending = !_defending;
        _animator.SetBool("Shield", _defending);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.localScale.x * transform.right * _offset, _attackRange);
    }
}
