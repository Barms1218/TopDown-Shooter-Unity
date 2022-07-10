using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment Instance { get; private set; }
    private static List<GameObject> wayPoints = new List<GameObject>();
    public static List<GameObject> WayPoints => wayPoints; 


    private void Awake()
    {
       if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public static GameEnvironment SingleTon
    {
        get
        {
            if (Instance == null)
            {
                Instance = new GameEnvironment();
                GameEnvironment.WayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
            }
            return Instance;
        }
    }

    public static List<GameObject> WayPoints1 { get => wayPoints; set => wayPoints = value; }
}
