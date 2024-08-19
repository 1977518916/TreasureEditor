using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public Vector2 point;

    public Vector2 size;

    public float angle;

    private void Update()
    {
        var hit = Physics2D.OverlapBox(point, size, angle);
        if (hit)
        {
            Debug.Log($"触碰到了物体，名字是：{hit.gameObject.name}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(point, size);
    }
}
