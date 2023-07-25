using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMommy : MonoBehaviour
{
    [SerializeField] GameObject _childPrefab;
    [SerializeField] int _hp;
    [SerializeField] int _childCount;

    [SerializeField] float _bonusChanse;
    Player _player;

    private void Start()
    {
        _hp = (int)(_hp * (1 + GameManager.Single.Score / 100));
    }

    public void TakeDamage()
    {
        _hp--;
        if (_hp <= 0)
        {
            GameManager.Single.Score++;
            Destroy(gameObject);
            if (_childCount> 0)
            {
                float angle = 360 / _childCount;
                for (int i = 0; i < _childCount; i++)
                {
                    var curAngle = Quaternion.Euler(0, 0, (angle * i) + 90);
                    Instantiate(_childPrefab, transform.position, curAngle);
                }
            }

            if (Random.value < _bonusChanse)
            {
                if (!_player)
                {
                    _player = FindObjectOfType<Player>();
                }

                _player.ReloadingDecrease();
            }
        }
    }
}
