using UnityEngine;

public class FirstKit : MonoBehaviour 
{
    [SerializeField, Min(1)] private float _amountHealth = 100f;

    public float EatFirstKit() => _amountHealth;
}
