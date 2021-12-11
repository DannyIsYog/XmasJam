using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [System.Serializable]
    public struct AttackHitbox
    {
        public float offset;
        public float range;
    }

    [SerializeField]
    private AttackHitbox _attackHitbox;

    [SerializeField]
    private LayerMask _enemyMask;

    private float _movement = 0f;
    private bool _facingRight = true;

    [SerializeField]
    private float _speed = 2f;

    private Rigidbody2D _rb;

    [SerializeField]
    private float _strength = 2f;
    private float _health = 100f;
    public Action<float> onEnemyAttack;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

    public void Attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(GetHitboxPos(), _attackHitbox.range, _enemyMask);
        if (enemy != null)
        {
            Entity entity = enemy.GetComponent<Entity>();
            if (entity.onEnemyAttack != null)
                entity.onEnemyAttack(_strength);
        }
    }

    public bool TakeDamage(float damage)
    {
        _health -= damage;
        return _health <= 0;
    }

    public Vector2 GetHitboxPos()
    {
        return transform.position + transform.localScale.x * transform.right * _attackHitbox.offset;
    }

    public void SetMovement(float value)
    {
        _movement = value * _speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetHitboxPos(), _attackHitbox.range);
    }
}
