using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private PathCreator path;
    [SerializeField] private string[] obstacles;
    [SerializeField] private bool starter = false;

    private void OnEnable()
    {
        if (starter)
            return;
        SpawnObstacle();
    }

    private void Start()
    {
        if (starter)
            SpawnObstacle();
    }

    private delegate void del(float dist);

    private void SpawnObstacle()
    {
        string obstacleType;
        bool once = false;
        for (float dist = 10f; dist <= 60f;)
        {
            obstacleType = obstacles[Random.Range(0, obstacles.Length)];
            if (obstacleType == "TrippleBalls")
                dist = CreateMultipleTrap(dist, TrippleBall);
            else if (obstacleType == "MovingBall")
                dist = CreateMultipleTrap(dist, MovingBall);
            else if (obstacleType == "OneOpenning")
                dist = CreateMultipleTrap(dist, OneOpenningTrap);
            else if (obstacleType == "ColorModifier" && !once)
            {
                once = true;
                ColorModifier(dist);
                dist += Random.Range(5, 11);
            }
        }
    }

    private float CreateMultipleTrap(float dist, del callback)
    {
        int multiple = Random.Range(2, 5);
        for (int i = 0; i < multiple; i++)
        {
            callback(dist);
            dist += 2;
        }
        dist += Random.Range(5, 11);
        return dist;
    }

    private void TrippleBall(float dist)
    {
        Vector3 pos = path.path.GetPointAtDistance(dist, EndOfPathInstruction.Stop);
        Quaternion rot = path.path.GetRotationAtDistance(dist, EndOfPathInstruction.Stop);
        GameObject obstacle = ObjectPool.Instance.SpawnFromPool("TrippleBalls", pos, rot);
        if (obstacle != null)
        {
            obstacle.GetComponent<TrippleBallObstacle>().Setup();
            obstacle.transform.SetParent(transform);
            obstacle.transform.eulerAngles = new Vector3(0, obstacle.transform.eulerAngles.y * 2f, obstacle.transform.eulerAngles.z);
        }

    }

    private void MovingBall(float dist)
    {
        Vector3 pos = path.path.GetPointAtDistance(dist, EndOfPathInstruction.Stop);
        Quaternion rot = path.path.GetRotationAtDistance(dist, EndOfPathInstruction.Stop);
        GameObject obstacle = ObjectPool.Instance.SpawnFromPool("MovingBall", pos, rot);
        if (obstacle != null)
        {
            obstacle.GetComponent<MovingBall>().Setup();
            obstacle.transform.SetParent(transform);
            obstacle.transform.eulerAngles = new Vector3(0, obstacle.transform.eulerAngles.y * 2f, obstacle.transform.eulerAngles.z);
        }
    }

    private void OneOpenningTrap(float dist)
    {
        Vector3 pos = path.path.GetPointAtDistance(dist, EndOfPathInstruction.Stop);
        Quaternion rot = path.path.GetRotationAtDistance(dist, EndOfPathInstruction.Stop);
        GameObject obstacle = ObjectPool.Instance.SpawnFromPool("OneOpenning", pos, rot);
        if (obstacle != null)
        {
            obstacle.GetComponent<OneOpenningTrap>().Setup();
            obstacle.transform.SetParent(transform);
            obstacle.transform.eulerAngles = new Vector3(0, obstacle.transform.eulerAngles.y * 2f, obstacle.transform.eulerAngles.z);
        }
    }

    private void ColorModifier(float dist)
    {
        Vector3 pos = path.path.GetPointAtDistance(dist, EndOfPathInstruction.Stop);
        Quaternion rot = path.path.GetRotationAtDistance(dist, EndOfPathInstruction.Stop);
        GameObject obstacle = ObjectPool.Instance.SpawnFromPool("ColorModifier", pos, rot);
        if (obstacle != null)
        {
            obstacle.GetComponent<ColorModifier>().Setup();
            obstacle.transform.SetParent(transform);
            obstacle.transform.eulerAngles = new Vector3(0, obstacle.transform.eulerAngles.y * 2f, obstacle.transform.eulerAngles.z + 90f);
        }

    }

}
