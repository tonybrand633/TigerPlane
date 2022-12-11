using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachineInfoShow : MonoBehaviour
{
    public TextMeshProUGUI CoinCount;
    public TextMeshProUGUI MachineMoveCount;
    public TextMeshProUGUI PrizeCondition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MachineMoveCount.text = TigerMachinePanel.S.MachineMoveCount.ToString();
        CoinCount.text = TigerMachineController.S.coinCount.ToString();
        PrizeCondition.text = TigerMachinePanel.S.condi.ToString();
    }
}
