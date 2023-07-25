using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    bool _move;
    Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        if (_move)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * GameManager.Single.Speed * _bulletSpeed, transform.position.z);
        }
    }

    void Death()
    {
        _move = false;
        transform.position = _startPos;
    }

    public void StartMove()
    {
        _move = true;
        Invoke(nameof(Death), 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyMommy>().TakeDamage();
            Death();
        }
    }
}
