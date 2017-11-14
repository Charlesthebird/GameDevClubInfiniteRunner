using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour {
    [HideInInspector]
    public SidescrollerGSC gsc;

    private void Awake()
    {
        gsc = FindObjectOfType<SidescrollerGSC>();
    }
}
