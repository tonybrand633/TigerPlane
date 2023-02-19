using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    /// <summary>
    ///  将飞机限制在相机的内部
    ///  只对位于0，0，0的mainCamera并且为正交相机有用（Orthographic)
    /// </summary>
    [Header("Set In Inspector")]
    public float radius;
    public float camHeight;
    public float camWidth;

    [Header("相机Bound信息")]
    private Bounds _camBounds;

    public Bounds camBounds 
    {
        get 
        {
            if (_camBounds==null) 
            {
                return new Bounds(Vector3.zero, new Vector3(camWidth * 2 + camHeight * 2, 2f));
            }
            return _camBounds;
        }
    }



    private void Awake()
    {
        //相机高度（半径）
        camHeight = Camera.main.orthographicSize;

        //相机高度乘以变量Camera.main.aspect可以得到相机的长度，半径
        camWidth = camHeight * Camera.main.aspect;
    }

    /// <summary>
    /// 放在LateUpdate中避免和hero产生竞态条件
    /// </summary>
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if (pos.x > camWidth + radius)
        {
            pos.x = camWidth + radius;
        }
        if (pos.x < -camWidth - radius)
        {
            pos.x = -camWidth - radius;
        }
        if (pos.y > camHeight + radius)
        {
            pos.y = camHeight + radius;
        }
        if (pos.y < -camHeight - radius)
        {
            pos.y = -camHeight - radius;
        }
        transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boundSize = new Vector3(camWidth*2,camHeight*2,2f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
