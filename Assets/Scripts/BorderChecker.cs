using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderChecker : MonoBehaviour
{
    [SerializeField] Camera gameCamera;

    // screen border coors
    [SerializeField] float borderDown;
    [SerializeField] float borderUp;
    [SerializeField] float borderLeft;
    [SerializeField] float borderRigth;
    [SerializeField] float offset = 0f;

    private void Start()
    {
        borderUp = gameCamera.ScreenToWorldPoint(Vector3.up * Screen.height).y + offset;
        borderDown = gameCamera.ScreenToWorldPoint(Vector3.zero).y - offset;
        borderRigth = gameCamera.ScreenToWorldPoint(Vector3.right * Screen.width).x + offset;
        borderLeft = gameCamera.ScreenToWorldPoint(Vector3.zero).x - offset;
    }

    public bool isPositionOutOfBorder(Vector2 position)
    {
        return position.x > borderRigth ||
               position.x < borderLeft ||
               position.y > borderUp ||
               position.y < borderDown;
    }

    public Vector2 GetBorderMax()
    {
        return new Vector2(borderRigth, borderUp);
    }
}
