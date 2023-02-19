using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float Speed;


    [Header("动态设置")]
    public float shiledType;
    public Shiled shiled;
    public static Player S;

    private void Awake()
    {
        if (S==null) 
        {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(h, v);

        Vector3 pos = transform.position;
        pos.x += dir.x * Speed*Time.deltaTime;
        pos.y += dir.y * Speed*Time.deltaTime;
        transform.position = pos;
    }

    public void GetDamage() 
    {
        if (shiled.hasShiled && shiled.shiledHp > 0)
        {
            //盾获得伤害
            shiled.shiledInfo.GetDamange();            
        }
        else 
        {
            //玩家获得伤害
        }
    }
}

