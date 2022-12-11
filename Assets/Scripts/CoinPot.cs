using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPot : MonoBehaviour
{
    public float Speed;
    public Transform Left;
    public Transform Right;
    public BoxCollider2D col;
    

    public bool FaceRight;
    public bool FaceLeft;

    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<BoxCollider2D>();

        float rf = Random.Range(0, 1);
        if (rf >= 0.5) 
        {
            FaceRight = true;
        }
        else
        {
            FaceLeft = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLoop();
    }

    void MoveLoop() 
    {
        if (FaceLeft)
        {
            Vector3 pos = this.transform.position;
            pos.x -= Speed * Time.deltaTime;
            this.transform.position = pos;
            if (this.transform.position.x < Left.transform.position.x)
            {
                FaceRight = true;
                FaceLeft = false;
            }
        }
        else if (FaceRight) 
        {
            Vector3 pos = this.transform.position;
            pos.x += Speed * Time.deltaTime;
            this.transform.position = pos;
            if (this.transform.position.x > Right.transform.position.x)
            {
                FaceRight = false;
                FaceLeft = true;
            }
        }        
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin") 
        {
            TigerMachinePanel.S.MachineMoveCount++;
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }                
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Coin In" + collision.gameObject.name);
    //}
}
