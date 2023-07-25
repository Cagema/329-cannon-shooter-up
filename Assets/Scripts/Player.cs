using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _gunTr;

    [SerializeField] float _reloadingTime;

    [SerializeField] int _bulletsPoolCount;
    [SerializeField] Transform _poolParent;
    Bullet[] _poolBullets;
    int _bulletIndex = 0;

    float _timeAfterShot;
    private void Start()
    {
        _timeAfterShot = _reloadingTime;

        _poolBullets = new Bullet[_bulletsPoolCount];
        for (int i = 0; i < _bulletsPoolCount; i++)
        {
            _poolBullets[i] = Instantiate(_bulletPrefab, Vector3.down * 15, Quaternion.identity, _poolParent).GetComponent<Bullet>();
        }
    }
    private void Update()
    {
        if (GameManager.Single.GameActive)
        {
            if (Input.GetMouseButton(0))
            {
                _timeAfterShot += Time.deltaTime;
                if (_timeAfterShot > _reloadingTime)
                {
                    _timeAfterShot = 0;
                    Shot();
                }
            }
        }
    }

    public void ReloadingDecrease()
    {
        if (_reloadingTime > 0.15f)
        {
            _reloadingTime -= 0.02f;
        }
    }

    private void Shot()
    {
        _poolBullets[_bulletIndex].transform.position = transform.position;
        _poolBullets[_bulletIndex].StartMove();
        _bulletIndex = ++_bulletIndex % _bulletsPoolCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Single.LostLive();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
