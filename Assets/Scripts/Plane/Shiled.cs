using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiled : MonoBehaviour
{
    [Header("护盾信息")]
    public int shiledType;
    public int shiledLevel;
    public float shiledHp;
    public bool hasShiled;
    public ShiledInfo shiledInfo;

    public GameObject[] shiledArry;
    public GameObject shiledCur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shiledType = Mathf.FloorToInt(Player.S.shiledType);
                
        if (hasShiled)
        {
            shiledInfo = shiledCur.GetComponent<ShiledInfo>();
            shiledHp = shiledInfo.Hp;

            //如果具有旋转属性则让护盾旋转
            if (shiledInfo.isRotate)
            {
                float rZ = shiledInfo.rotatePerSecond;
                this.transform.rotation = Quaternion.Euler(0f, 0f, (rZ * Time.time * 360f) % 360f);
            }                
        }
    }

    public void ChangeShiled(int shiledIndex) 
    {
        if (hasShiled)
        {            
            //如果有盾牌的话，增加盾牌等级,或者更换盾牌类型
            if (shiledIndex != shiledType && shiledType != 0)
            {                
                Destroy(shiledCur);
                shiledCur = Instantiate(shiledArry[shiledIndex-1], transform.position, Quaternion.identity);
                shiledCur.transform.SetParent(this.transform);
                Player.S.shiledType = shiledIndex;
            }
            else
            {
                shiledLevel++;
                shiledInfo.LevelUp(shiledLevel);
            }
        }
        else 
        {
            shiledCur = Instantiate(shiledArry[shiledIndex-1], transform.position, Quaternion.identity);
            shiledCur.transform.SetParent(this.transform);
            hasShiled = true;
            Player.S.shiledType = shiledIndex;
        }
    }
}
