using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : GameElement {
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var p = other.GetComponent<Player>();
        if(p != null)
        {
            p.TakeDamage(damage);
        }
    }
}
