using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetPlayer : MonoBehaviour
{
    private static CabinetPlayer instance;
    public static CabinetPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<CabinetPlayer>();
            return instance;
        }
    }
}
