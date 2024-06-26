using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour
{
    public event Action CoinPicked;
    public event Action<float> FirstKitPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);

            CoinPicked?.Invoke();
        }

        if (collision.TryGetComponent(out FirstKit firstKit))
        {
            FirstKitPicked?.Invoke(firstKit.EatFirstKit());
            
            Destroy(firstKit.gameObject);
        }
    }
}
