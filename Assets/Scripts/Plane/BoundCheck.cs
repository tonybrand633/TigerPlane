using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    /// <summary>
    ///  ���ɻ�������������ڲ�
    ///  ֻ��λ��0��0��0��mainCamera����Ϊ����������ã�Orthographic)
    /// </summary>
    [Header("Set In Inspector")]
    public float radius;
    public float camHeight;
    public float camWidth;

    [Header("���Bound��Ϣ")]
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
        //����߶ȣ��뾶��
        camHeight = Camera.main.orthographicSize;

        //����߶ȳ��Ա���Camera.main.aspect���Եõ�����ĳ��ȣ��뾶
        camWidth = camHeight * Camera.main.aspect;
    }

    /// <summary>
    /// ����LateUpdate�б����hero������̬����
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
