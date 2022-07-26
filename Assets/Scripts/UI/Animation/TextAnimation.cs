using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation
{

    public IEnumerator WoopAnimation(RectTransform rectTransform, float maxScale)
    {
        Vector3 initialScale = rectTransform.localScale;
        Vector3 _enlargeScale = new Vector3(maxScale, maxScale, maxScale);

        float changeSpeed = Mathf.Abs((initialScale.x - maxScale) / 0.1f);

        while (rectTransform.localScale.x < maxScale)
        {
            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, _enlargeScale, changeSpeed * Time.deltaTime);
            yield return null;
        }

        while (rectTransform.localScale.x > initialScale.x)
        {
            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, initialScale, changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
