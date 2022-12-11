using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMachineController : MonoBehaviour
{
    float cCount;

    public static TigerMachineController S;
    public GameObject Coin;
    public float coinInsTime;
    


    public float coinCount 
    {
        get { return cCount; }
        set 
        {
            cCount = value;
        }
    }

    float u;
    float lastCoinInsTime;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;    
    }

    // Update is called once per frame
    void Update()
    {
        u = (Time.time - lastCoinInsTime);
        if (u <= coinInsTime)
        {
            return;
        }
        else 
        {
            InsCoins();
        }        
    }

    public void InsCoins() 
    {
        if (coinCount<=0) 
        {
            return;
        }
        GameObject go = Instantiate(Coin, transform.position, Quaternion.identity);
        cCount--;
        lastCoinInsTime = Time.time;
    }

    public void AddCoin() 
    {
        cCount++;
    }
}
