using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiledInfo : MonoBehaviour
{
    public float Hp;
    public float rotatePerSecond;
    public bool isRotate;

    //¸ø»¤¶ÜÉý¼¶
    public void LevelUp(float shiledLevel) 
    {
        Hp += shiledLevel;
    }


    public void GetDamange() 
    {
        Hp--;
    }
}
