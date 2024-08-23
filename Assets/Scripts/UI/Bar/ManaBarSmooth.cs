using UnityEngine;
using UnityEngine.UI;

public class ManaBarSmooth : Attribute
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private Vampirism _vampirism;

    private Coroutine _renderMana;

    protected void OnEnable()
    {
        _vampirism.ManaChanged += RenderMana;
    }

    protected void OnDisable()
    {
        _vampirism.ManaChanged -= RenderMana;
    }

    private void RenderMana(float absorbMana, float absorbManaMax)
    {
        RenderBar(absorbMana, absorbManaMax, _renderMana, _lerpSpeed, _slider);
    }
}
