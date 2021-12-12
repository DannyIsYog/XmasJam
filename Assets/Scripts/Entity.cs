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
    private int _strength = 2;
    private int _health = 100;
    public Action<int> onEnemyAttack;

    public GameObject floatingPoints;

    public HealthBar healthBar;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(_health);
    }

    void Update()
    {
        if ((_movement < 0 && _facingRight) || (_movement > 0 && !_facingRight))
        {
            _facingRight = !_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            healthBar.transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        if (_movement != 0)
            _rb.velocity = new Vector2(_speed * _movement, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);
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

    public bool TakeDamage(int damage)
    {
        _health -= damage;
        healthBar.SetHealth(_health);
        Debug.Log(_health);
        if (floatingPoints != null)
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
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

    public AttackHitbox getAttackHitbox()
    {
        return _attackHitbox;
    }
}
