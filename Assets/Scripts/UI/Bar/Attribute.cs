using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Attribute : MonoBehaviour
{
    protected void RenderBar(float currentValue, float maxValue, Coroutine render, float lerpSpeed, Slider slider)
    {
        if (render != null)
        {
            StopCoroutine(render);
        }

        float targetMana = currentValue / maxValue;

        render = StartCoroutine(SmoothRender(targetMana, slider.value, lerpSpeed, slider));
    }

    private IEnumerator SmoothRender(float targetValue, float startValue, float lerpSpeed, Slider slider)
    {
        var wait = new WaitForEndOfFrame();

        float lerpTime = 0f;

        while (slider.value != targetValue)
        {
            slider.value = Mathf.Lerp(startValue, targetValue, lerpTime += lerpSpeed * Time.deltaTime);

            yield return wait;
        }
    }
}
