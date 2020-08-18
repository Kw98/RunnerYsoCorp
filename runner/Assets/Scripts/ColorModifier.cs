using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    [SerializeField] private Material[] colors;
    [SerializeField] private MeshRenderer mr;

    public void Setup()
    {
        Material playerColor = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material;
        int index = Random.Range(0, colors.Length);
        while (colors[index].name + " (Instance)" == playerColor.name)
            index = Random.Range(0, colors.Length);
        mr.material = colors[index];
    }
}
