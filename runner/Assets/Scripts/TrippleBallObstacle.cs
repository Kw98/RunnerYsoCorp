using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrippleBallObstacle : MonoBehaviour
{
    [SerializeField] private List<GameObject> objs;
    public void Setup()
    {
        List<GameObject> colorBalls = new List<GameObject>();
        colorBalls.AddRange(objs);
        Vector3[] positions = { new Vector3(0, 0.1f, 0), new Vector3(0.25f, 0.1f, 0), new Vector3(-0.25f, 0.1f, 0) };

        for (int i = 0; i < positions.Length; i++)
        {
            int index = Random.Range(0, colorBalls.Count);
            colorBalls[index].SetActive(true);
            colorBalls[index].GetComponent<MeshRenderer>().enabled = true;
            colorBalls[index].GetComponent<ParticleSystem>().Stop();
            colorBalls[index].transform.position = transform.position + positions[i];
            colorBalls.RemoveAt(index);
        }
    }
}
