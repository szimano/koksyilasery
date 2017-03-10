using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {

    public static float health = 200f;

    public void Health(float points)
    {
        health = points;
    }

    public static void Reset()
    {
        health = 200f;
    }

    void Update()
    {
        gameObject.GetComponent<Text>().text = health.ToString();
    }
}
