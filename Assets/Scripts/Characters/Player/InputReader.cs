using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    private bool _isJump;
    private bool _isAttack;
    private bool _isUseSkill;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Convert.ToBoolean(Input.GetAxis(Jump)))
        {
            _isJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isUseSkill = true;
        }
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    public bool GetAttack() => GetBoolAsTrigger(ref _isAttack);
    public bool GetUseSkillk() => GetBoolAsTrigger(ref _isUseSkill);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
