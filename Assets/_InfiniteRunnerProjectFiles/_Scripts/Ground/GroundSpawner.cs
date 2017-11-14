using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

[Serializable]
public class GroundObject
{
    public GameObject prefab;
    [Range(1,100)]
    public float frequency;
}
[Serializable]
public class GroundSpawner : MonoBehaviour
{
    public float groundSpeed = -10f;
    public GroundObject[] groundObjects;
    GameObject lastSpawnedGround;

    static GroundObject selectGroundObject(GroundObject[] G, int low, int high)
    {
        if (low == high) return G[low]; // base case
        else
        {
            // recursive case (split left and right halves until they are 1 element each)
            var mid = (high-low) / 2;
            var l = selectGroundObject(G, low, mid);
            var r = selectGroundObject(G, mid + 1, high);
            // calculate the probablility that l should be picked
            var p = l.frequency / (l.frequency + r.frequency);
            // roll to figure out the outcome
            var proll = UnityEngine.Random.Range(0.0f, 1.0f);
            // return the groundObject result
            if (proll <= p) return l;
            else return r;
        }
    }
    static GroundObject selectGroundObjectAsync(GroundObject[] G, int low, int high)
    {
        if (low == high) return G[low]; // base case
        else
        {
            // recursive case (split left and right halves until they are 1 element each)
            // left half is calculated in a separate thread
            GroundObject l = null;
            var mid = (high-low) / 2;
            var tL = new Thread(new ThreadStart(() => {
                l = selectGroundObjectAsync(G, low, mid);
            }));
            tL.Start();
            var r = selectGroundObjectAsync(G, mid + 1, high);
            // join the threads (l and r should have been assigned now)
            tL.Join();
            // calculate the probablility that l should be picked
            var p = l.frequency / (l.frequency + r.frequency);
            // roll to figure out the outcome
            var rand = new System.Random();
            var proll = (float)rand.NextDouble();
            // return the groundObject result
            if (proll <= p) return l;
            else return r;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        // if this is the first time the ground has been spawned, spawn a ground object
        if (lastSpawnedGround == null)
        {
            SpawnGround();
        }
        else
        {
            // get the length of ground's box collider
            var length = lastSpawnedGround.GetComponentInChildren<BoxCollider2D>().bounds.size.x;
            // get the difference between the last spawned ground's position and the spawner's psoition
            var diff = Mathf.Abs(lastSpawnedGround.transform.localPosition.x);
            // if the ground has moved far enough away, spawn another ground
            if(diff >= (length - Mathf.Abs(Time.deltaTime * 2.0f * groundSpeed)))
            {
                SpawnGround();
            }
        }
    }

    void SpawnGround()
    {
        // select the ground object
        var groundObject = selectGroundObjectAsync(groundObjects, 0, groundObjects.Length-1);
        // set the speed of the ground
        groundObject.prefab.GetComponent<Ground>().speed = groundSpeed;
        // instantiate it
        lastSpawnedGround = Instantiate(groundObject.prefab, this.transform.position,
            Quaternion.identity, this.transform) as GameObject;
    }
}
