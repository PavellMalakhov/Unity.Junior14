using UnityEngine;

public class Recovery : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _collisionDetector.FirstKitPicked += AddHealth;
    }

    private void OnDisable()
    {
        _collisionDetector.FirstKitPicked -= AddHealth;
    }

    public void AddHealth()
    {
        _player.AddHealth();
    }
}
