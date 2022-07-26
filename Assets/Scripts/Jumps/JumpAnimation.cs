using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimation
{
    public IEnumerator Play(AnimationCurve animationCurve, Transform transform)
    {
        Vector3 nextPosition = transform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < animationCurve.length)
        {
            elapsedTime += Time.deltaTime;

            nextPosition.y = animationCurve.Evaluate(elapsedTime);

            transform.localPosition = nextPosition;

            yield return null;
        }
    }
}
