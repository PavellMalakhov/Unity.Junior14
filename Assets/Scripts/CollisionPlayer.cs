using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionPlayer : MonoBehaviour
{
    public static event Action CoinPick;

    private bool _isCoinPick = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);

            _isCoinPick = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isCoinPick)
        {
            _isCoinPick = false;
            CoinPick.Invoke();
        }
    }
}
