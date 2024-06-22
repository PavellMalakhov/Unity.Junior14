using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;

    private int _coin = 0;

    private void OnEnable()
    {
        _collisionDetector.CoinPicked += AddCoin;
    }

    private void OnDisable()
    {
        _collisionDetector.CoinPicked -= AddCoin;
    }

    private void AddCoin()
    {
        _coin++;
    }
}
