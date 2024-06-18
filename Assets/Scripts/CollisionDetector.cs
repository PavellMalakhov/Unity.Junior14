using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour
{
    private bool _isCoinPick = false;

    public event Action CoinPick;

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
