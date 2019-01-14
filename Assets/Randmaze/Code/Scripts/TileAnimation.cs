using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TileAnimation : MonoBehaviour
{
    public float MinimumTime;
    public float MaximumTime;

    public AnimationCurve AnimationX;
    public AnimationCurve AnimationY;

    public Vector2 Multipler = Vector2.one;

    private const float GRANUALITY = 0.01f;

    private IEnumerator Start()
    {
        //Decide the time the animatio will take
        float realTime = UnityEngine.Random.Range(MinimumTime, MaximumTime);

        //Store target positions
        float originalX = transform.position.x;
        float originalY = transform.position.y;

        //Run animations
        for (float i = 0; i < 1; i += GRANUALITY)
        {
            transform.position = new Vector3(
                x: originalX + AnimationX.Evaluate(i) * Multipler.x, 
                y: originalY + AnimationY.Evaluate(i) * Multipler.y);
            yield return new WaitForSeconds(GRANUALITY / realTime);
        }

        //Fix iregularities
        transform.position = new Vector3(originalX, originalY);
    }
}
