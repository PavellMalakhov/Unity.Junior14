using UnityEngine;

public class ScaleHold : MonoBehaviour
{
    private Vector3 _scale;

    private void Start()
    {
        _scale = transform.lossyScale;
    }

    private void Update()
    {
        if (transform.lossyScale != _scale)
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
