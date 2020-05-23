using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public bool isYourTurn;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartYourTurn()
    {
        isYourTurn = true;
    }

    public void EndYourTurn()
    {
        isYourTurn = false;
    }

}
