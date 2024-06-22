using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour
{
    public event Action CoinPick;
    public event Action FirstKitPick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);

            CoinPick?.Invoke();
        }

        if (collision.TryGetComponent(out FirstKit firstKit))
        {
            Destroy(firstKit.gameObject);

            FirstKitPick?.Invoke();
        }
    }
}
