using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOpenningTrap : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    [SerializeField] private Material[] colors;
    public void Setup()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(new Vector3(0, 0.1f, 0));
        positions.Add(new Vector3(-0.25f, 0.1f, 0));
        positions.Add(new Vector3(0.25f, 0.1f, 0));
        Material color;

        foreach (GameObject ball in balls)
        {
            int index = Random.Range(0, positions.Count);
            ball.transform.position = transform.position + positions[index];
            ball.SetActive(true);
            positions.RemoveAt(index);
            color = colors[Random.Range(0, colors.Length)];
            ball.GetComponent<MeshRenderer>().enabled = true;
            ball.GetComponent<ParticleSystem>().Stop();
            ball.GetComponent<MeshRenderer>().material = color;
            ball.GetComponent<ParticleSystemRenderer>().material = color;
        }
    }
}
