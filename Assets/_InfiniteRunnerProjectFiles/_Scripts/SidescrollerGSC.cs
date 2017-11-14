using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SidescrollerGSC : MonoBehaviour {
    [HideInInspector]
    public Player player;

    public UnityEvent doOnStart;


    public static T TryToGet<T>() where T : MonoBehaviour
    { 
        var o = FindObjectOfType<T>();
        if (o == null)
        {
            Debug.LogError("Could not find: " + typeof(T));
            return null;
        }
        else return o;
    }


	void Awake () {
        doOnStart.Invoke();

        player = TryToGet<Player>();
	}
	
}
