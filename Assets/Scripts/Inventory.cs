using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;

    private int _coin = 0;

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
