using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _coin = 0;
    [SerializeField] private CollisionDetector _collisionDetector;

    private void OnEnable()
    {
        _collisionDetector.CoinPick += AddCoin;
    }

    private void OnDisable()
    {
        _collisionDetector.CoinPick -= AddCoin;
    }

    private void AddCoin()
    {
        _coin++;
    }
}
