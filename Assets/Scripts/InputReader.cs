using UnityEngine;
using System;


public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    private bool _isJump;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Convert.ToBoolean(Input.GetAxis(Jump)))
        {
            _isJump = true;
        }
    }

    //public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsJump()
    {
        return GetBoolAsTrigger(ref _isJump);
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
