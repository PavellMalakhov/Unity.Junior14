using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _coin = 0;

    private void OnEnable()
    {
        CollisionPlayer.CoinPick += AddCoin;
    }

    private void OnDisable()
    {
        CollisionPlayer.CoinPick -= AddCoin;
    }

    private void AddCoin()
    {
        _coin++;
    }
}
