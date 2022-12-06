using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{    
    public bool outOfBottom;
    public CheckType checkType;
    public float Speed;

    Transform panelTrans;
    SpriteRenderer sr;

    Bounds panelBounds;
    Bounds prizeBounds;

    public SpriteRenderer SR 
    {
        get { return sr; }

        set 
        {
            sr = value;
            sr.sprite = value.sprite;
        }
    }
    public SpriteRenderer panelSr;

    void Awake()
    {
        
        
    }


    // Start is called before the first frame update
    void Start()
    {
        panelTrans = tracerParent(this.transform);         
        panelSr =panelTrans.GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
        panelBounds = panelSr.bounds;

        checkType = CheckType.outBottom;               
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y -= Speed * Time.fixedDeltaTime;
        transform.position = pos;
        prizeBounds = this.GetComponent<SpriteRenderer>().bounds;
        outOfBottom = Utils.CheckInBounds(panelBounds, prizeBounds, checkType);        
    }

    void LateUpdate()
    {
        if (outOfBottom)
        {
            //TigerMachinePanel.S.InstiatePrize(this);
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed) 
    {
        this.Speed = speed;
    }

    Transform tracerParent(Transform t) 
    {
        if (t.transform.name != "tigermachinePlane")
        {
            t = t.parent;
            return tracerParent(t);
        }
        else 
        {
            return t;
        }
    }
}
