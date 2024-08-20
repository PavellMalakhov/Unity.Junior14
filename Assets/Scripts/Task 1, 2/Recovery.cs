using UnityEngine;

public class Recovery : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _collisionDetector.FirstKitPicked += AddHealth;
    }

    private void OnDisable()
    {
        _collisionDetector.FirstKitPicked -= AddHealth;
    }

    public void AddHealth(float amountHealth)
    {
        _health.AddHealth(amountHealth);
    }
}
