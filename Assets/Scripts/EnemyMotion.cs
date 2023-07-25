using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    [SerializeField] float _rotSpeed;
    [SerializeField] float _speedMult;
    Transform _playerTr;
    private void Start()
    {
        _playerTr = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        if (GameManager.Single.GameActive)
        {
            Vector3 diference = _playerTr.position - transform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotateZ), Time.deltaTime * _rotSpeed);
            transform.Translate(GameManager.Single.Speed * _speedMult * Time.deltaTime * Vector2.down);
        }
    }
}
