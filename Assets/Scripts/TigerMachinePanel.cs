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
    public bool prizeLoad;
    public bool startCheck;

    public bool MachineDuration;


    public float pSpeed;
    //public float pSpeedRateOffset;
    public float PrizeMoveDuration;
    public float pInsInterval;
    public float machineDurationTime;
    public float prizeWaitTime;

    public float xOffset;
    public float yOffset;
    public float initYOffset;

    public Transform PrizeAnchor;
    public Transform NormalPrizeAnchor;


    public Transform[] tigerMachinePos;
    public Transform[] resInitPos;
    public Transform[] InstiatePos;
    public GameObject[] resPrizes;
    public List<GameObject> movingPirzes = new List<GameObject>();
    public PrizeCondition condi;



    


    SpriteRenderer sr;

    Bounds bounds;
    bool pMove;
    
    float Width;
    float Height;
    float machineMoveCount;


    public float u;
    public float u2;
    public float pStartTime;
    public float movePrizeStartTime;

    public float MachineMoveCount 
    {
        get { return machineMoveCount; }
        set 
        {
            machineMoveCount = value; 
        }
    }

    //public float machineDurationTime;
       
    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //prizeMoveComplete = true;
        sr = GetComponent<SpriteRenderer>();
        bounds = sr.bounds;

        Width = bounds.max.x - bounds.min.x;
        Height = bounds.max.y - bounds.min.y;
        
        InitPosition();       
    }

    void Update()
    {
        if (machineMoveCount>0) 
        {
            StartBtnClick();
        }

        if (!pStartMove && PickPrize)
        {
            StartPickPrize();
        }        
    }    

    // Update is called once per frame
    void FixedUpdate()
    {
        u2 = (Time.time - pStartTime) / machineDurationTime;
        if (u2 < 1)
        {
            //完成一个速度递减的功能
            if (pMove)
            {
                InstiateNormalPrize();
            }
        }
        else
        {
            pStartMove = false;
        }

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


        InstiatePos[0].position = new Vector3(bounds.center.x - (Width / 2) + xOffset, bounds.center.y + (Height / 2)+initYOffset, bounds.center.z);
        InstiatePos[1].position = new Vector3(bounds.center.x, bounds.center.y + (Height / 2)+initYOffset, bounds.center.z);
        InstiatePos[2].position = new Vector3(bounds.center.x + (Width / 2) - xOffset, bounds.center.y + (Height / 2)+initYOffset, bounds.center.z);

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

    //IEnumerator pInstiate()
    //{
    //    //Debug.Log("Start Coroutine");

    //    for (int i = 0; i < InstiatePos.Length; i++)
    //    {
    //        //Debug.Log("Ins go" + i.ToString());
    //        int pIndex = Random.Range(0, PrizePrefabs.Length);
    //        GameObject go = Instantiate(PrizePrefabs[pIndex].gameObject, InstiatePos[i].position, Quaternion.identity);
    //        go.transform.SetParent(NormalPrizeAnchor);
    //        go.GetComponent<Prize>().SetSpeed((1-u2+pSpeedRateOffset) * pSpeed);

    //    }
    //    pMove = false;
    //    yield return new WaitForSeconds(pInsInterval);
    //    pMove = true;


    //    //Debug.Log("2 Seconds Operation");        
    //}



    public void StartPickPrize() 
    {       
        int index = Random.Range(0, 3);
        condi = (PrizeCondition)index;
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
        u = (Time.time - startTime) / PrizeMoveDuration;
        for (int i = 0; i < resPrizes.Length; i++)
        {                       
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
                CheckPrizeRes();
                return;
            }                       
        }
    }

    void RefreshResPrizes()
    {
        if (resPrizes[0].gameObject != null)
        {
            foreach (GameObject go in resPrizes)
            {
                go.GetComponent<Prize>().enabled = true;
            }
        }
    }

    void CheckPrizeRes()
    {
        if (!startCheck) 
        {
            StartCoroutine("WaitForPrizeCheck");
            startCheck = true;
        }   
        

    }

    IEnumerator pInstiate()
    {
        //Debug.Log("Start Coroutine");

        for (int i = 0; i < resInitPos.Length; i++)
        {
            //Debug.Log("Ins go" + i.ToString());
            int pIndex = Random.Range(0, PrizePrefabs.Length);
            GameObject go = Instantiate(PrizePrefabs[pIndex].gameObject, resInitPos[i].position, Quaternion.identity);
            go.transform.SetParent(NormalPrizeAnchor);
            //go.GetComponent<Prize>().SetSpeed((1 - u2 + pSpeedRateOffset) * pSpeed);
            go.GetComponent<Prize>().SetSpeed(pSpeed);
        }
        pMove = false;
        yield return new WaitForSeconds(pInsInterval);
        pMove = true;


        //Debug.Log("2 Seconds Operation");        
    }

    IEnumerator WaitForPrizeCheck() 
    {
        yield return new WaitForSeconds(prizeWaitTime);
        MachineDuration = false;
    }

    //绑定于StartButton上的方法;
    public void StartBtnClick()
    {
        //if (pStartMove&&prizeLoad) 
        //{
        //    return;
        //}
        //if (pStartMove || prizeCheckOver == false)
        //{
        //    return;
        //}
        if (MachineDuration) 
        {
            return;
        }
        RefreshResPrizes();
        pMove = true;
        pStartMove = true;
        MachineDuration = true;
        PickPrize = true;
        prizeLoad = false;
        startCheck = false;
        prizeMoveComplete = false;
        pStartTime = Time.time;
        machineMoveCount--;
    }    
}
