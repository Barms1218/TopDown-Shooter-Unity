using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment _instance { get; private set; }
    private static GameObject[] wayPoints;
    public static GameObject[] WayPoints => wayPoints;
    public static int WayPointCount => wayPoints.Length;


    private void Awake()
    {
       if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        
    }
    private void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //Debug.Log(WayPointCount);
    }
    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new();
                
            }
            return _instance;
        }
    }
}
