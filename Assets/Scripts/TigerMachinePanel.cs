using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrizeCondition 
{
    bigPrize,
    midPrize,
    litPrize    
}

public class TigerMachinePanel : MonoBehaviour
{
    public static TigerMachinePanel S;

    public GameObject point;
    public Prize[] PrizePrefabs;
    


    
    [Header("Set In Inspector")]
    //public bool isMove;    
    public bool pStartMove;
    
    public bool PickPrize;
    public bool prizeMoveComplete;

    
    public float pStartSpeed;
    public float PrizeMoveDuration;
    public float pInsDuration;
    public float machineDurationTime;

    public float xOffset;
    public float yOffset;

    public Transform PrizeAnchor;
    public Transform NormalPrizeAnchor;


    public Transform[] tigerMachinePos;
    public Transform[] resInitPos;
    public Transform[] InstiatePos;
    public GameObject[] resPrizes;
    public List<GameObject> movingPirzes = new List<GameObject>();



    


    SpriteRenderer sr;

    Bounds bounds;
    bool pMove;
    bool prizeLoad;
    float Width;
    float Height;


    public float u;
    public float u2;
    public float pStartTime;
    public float movePrizeStartTime;
    //public float machineDurationTime;

    
    

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bounds = sr.bounds;

        Width = bounds.max.x - bounds.min.x;
        Height = bounds.max.y - bounds.min.y;
        
        InitPosition();       
    }

    void Update()
    {
        u2 = (Time.time - pStartTime) / machineDurationTime;
        if (u2 <= 1)
        {
            //完成一个速度递减的功能
            if (pMove)
            {
                InstiateNormalPrize();
                //u2 = (Time.time - pStartTime) / pDuration;
                //if (u2 >= 1)
                //{
                //    InstiateNormalPrize();
                //    pStartTime = Time.time;
                //}
            }
        }
        else 
        {
            pStartMove = false;
        }
              
        
        if (PickPrize) 
        {
            StartPickPrize();
        }
    }    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (prizeLoad) 
        {
            MovePrize(movePrizeStartTime);
        }
    }

    void InitPosition()
    {
        tigerMachinePos = new Transform[9];
        resInitPos = new Transform[9];
        InstiatePos = new Transform[3];
        for (int i = 0; i < tigerMachinePos.Length; i++)
        {
            GameObject go = Instantiate(point, Vector3.zero, Quaternion.identity);
            tigerMachinePos[i] = go.transform;
            go.transform.SetParent(this.transform);
            go.name = "MachinePoint" + i.ToString();
        }

        for (int i = 0; i < resInitPos.Length; i++)
        {
            GameObject go = Instantiate(point, Vector3.zero, Quaternion.identity);
            resInitPos[i] = go.transform;
            go.transform.SetParent(this.transform);
            go.name = "ResInitPoint" + i.ToString();
        }

        for (int i = 0; i < InstiatePos.Length; i++)
        {
            GameObject go = Instantiate(point, Vector3.zero, Quaternion.identity);
            InstiatePos[i] = go.transform;
            go.transform.SetParent(this.transform);
            go.name = "Instiate" + i.ToString();
        }

        tigerMachinePos[0].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y + (Height / 2) - yOffset, bounds.center.z);
        tigerMachinePos[3].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y, bounds.center.z);
        tigerMachinePos[6].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y - (Height / 2) + yOffset, bounds.center.z);
        tigerMachinePos[1].position = new Vector3(bounds.center.x, bounds.center.y + (Height / 2) - yOffset, bounds.center.z);
        tigerMachinePos[4].position = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z);
        tigerMachinePos[7].position = new Vector3(bounds.center.x, bounds.center.y - (Height / 2) + yOffset, bounds.center.z);
        tigerMachinePos[2].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y + (Height / 2) - yOffset, bounds.center.z);
        tigerMachinePos[5].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y, bounds.center.z);
        tigerMachinePos[8].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y - (Height / 2) + yOffset, bounds.center.z);


        InstiatePos[0].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y + (Height / 2), bounds.center.z);
        InstiatePos[1].position = new Vector3(bounds.center.x, bounds.center.y + (Height / 2), bounds.center.z);
        InstiatePos[2].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y + (Height / 2), bounds.center.z);

        resInitPos[0].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y + (Height / 2) - yOffset + Height, bounds.center.z);
        resInitPos[3].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y + Height, bounds.center.z);
        resInitPos[6].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y - (Height / 2) + yOffset + Height, bounds.center.z);
        resInitPos[1].position = new Vector3(bounds.center.x, bounds.center.y + (Height / 2) - yOffset + Height, bounds.center.z);
        resInitPos[4].position = new Vector3(bounds.center.x, bounds.center.y + Height, bounds.center.z);
        resInitPos[7].position = new Vector3(bounds.center.x, bounds.center.y - (Height / 2) + yOffset + Height, bounds.center.z);
        resInitPos[2].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y + (Height / 2) - yOffset + Height, bounds.center.z);
        resInitPos[5].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y + Height, bounds.center.z);
        resInitPos[8].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y - (Height / 2) + yOffset + Height, bounds.center.z);
    }

    void InstiateNormalPrize()
    {
        StartCoroutine("pInstiate");
    }

    IEnumerator pInstiate()
    {
        //Debug.Log("Start Coroutine");

        for (int i = 0; i < InstiatePos.Length; i++)
        {
            //Debug.Log("Ins go" + i.ToString());
            int pIndex = Random.Range(0, PrizePrefabs.Length);
            GameObject go = Instantiate(PrizePrefabs[pIndex].gameObject, InstiatePos[i].position, Quaternion.identity);
            go.transform.SetParent(NormalPrizeAnchor);
            go.GetComponent<Prize>().SetSpeed((1-u2+0.4f) * pStartSpeed);

        }
        pMove = false;
        yield return new WaitForSeconds(pInsDuration);
        pMove = true;


        //Debug.Log("2 Seconds Operation");        
    }

    public void StartPickPrize() 
    {
        if (resPrizes[0].gameObject !=null) 
        {
            foreach (GameObject go in resPrizes) 
            {
                go.GetComponent<Prize>().enabled = true;
            }
        }

        int index = Random.Range(0, 3);
        PrizeCondition condi = (PrizeCondition)index;
        Debug.Log(condi);
        resPrizes = new GameObject[9];
        InitPrize(condi);
        movePrizeStartTime = Time.time;
        prizeLoad = true;
        PickPrize = false;
    }    

    void InitPrize(PrizeCondition condition) 
    {
        int pIndex;
        GameObject go;

        //设置奖项
        switch (condition) 
        {
            case PrizeCondition.bigPrize:
                pIndex = 0;
                for (int i = 0; i < resInitPos.Length; i++)
                {
                    go = Instantiate(PrizePrefabs[pIndex], resInitPos[i].position, Quaternion.identity).gameObject;
                    go.transform.SetParent(PrizeAnchor);
                    resPrizes[i] = go;                    
                }                                                                   
                break;
            case PrizeCondition.midPrize:                
                for (int i = 0; i < resInitPos.Length; i++)
                {
                    pIndex = Random.Range(0, 3);
                    go = Instantiate(PrizePrefabs[pIndex], resInitPos[i].position, Quaternion.identity).gameObject;
                    go.transform.SetParent(PrizeAnchor);
                    resPrizes[i] = go;
                }
                break;
            case PrizeCondition.litPrize:                
                for (int i = 0; i < resInitPos.Length; i++)
                {
                    pIndex = Random.Range(0, PrizePrefabs.Length);
                    go = Instantiate(PrizePrefabs[pIndex], resInitPos[i].position, Quaternion.identity).gameObject;
                    go.transform.SetParent(PrizeAnchor);
                    resPrizes[i] = go;
                }
                break;
        }
        foreach (GameObject res in resPrizes) 
        {
            res.GetComponent<Prize>().enabled = false;
        }
    }



    void MovePrize(float startTime) 
    {
        for (int i = 0; i < resPrizes.Length; i++)
        {
            u = (Time.time - startTime)/PrizeMoveDuration;
            
            GameObject go = resPrizes[i];
            Vector3 pos = go.transform.position;
            if (u < 1)
            {
                pos.y = u * tigerMachinePos[i].position.y + (1 - u) * resInitPos[i].position.y;
                go.transform.position = pos;
            }
            else 
            {
                prizeMoveComplete = true;
                return;
            }                       
        }
    }

    void CheckPrizeRes()
    {


    }


    //绑定于StartButton上的方法;
    public void StartBtnClick()
    {
        pMove = true;
        pStartTime = Time.time;
        pStartMove = true;

        
        prizeMoveComplete = false;
    }
    
}
