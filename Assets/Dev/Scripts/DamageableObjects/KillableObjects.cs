using Scripts.Interfaces;
using UnityEngine;

public class KillableObjects : MonoBehaviour, IKillable
{
    public float health;
    public void Execute()
    {
        health -= 5;

        if (!(health <= 0)) return;
        print("I killed");
        Destroy(gameObject);
    }
}
