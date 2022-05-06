using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapMvm : MonoBehaviour
{
    [SerializeField] private Transform camTr;
    [SerializeField] private Vector2 speed;
    private Touch touch;

    private Vector2 beginTouchPos, endTouchPos;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    beginTouchPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    endTouchPos = touch.position;
                    if (beginTouchPos.x > endTouchPos.x )
                    {
                        camTr.Translate(speed);
                    }

                    if (beginTouchPos.x < endTouchPos.x)
                    {
                        camTr.Translate(-speed);
                    }
                    break;
            }
            beginTouchPos = touch.position;

        }
    }
}
