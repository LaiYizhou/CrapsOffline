using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAchievement : MonoBehaviour
{

    
    [SerializeField] private bool isRoundStart;
    public bool IsRoundStart
    {
        get
        {
            return isRoundStart;
        }

        set
        {
            isRoundStart = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Init();
    }

    public void Init()
    {
        IsRoundStart = false;
    }

}
