using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Material[] colors;
    [SerializeField] private float speed = 0.75f;
    [SerializeField] private bool moveLeft = true;
    private float move = 0;
    
    public void Setup()
    {
        float[] starting = { -0.25f, 0, 0.25f };
        int index = Random.Range(0, starting.Length);

        ball.SetActive(true);
        ball.transform.position = transform.position + new Vector3(starting[index], 0.1f, 0);
        if (Random.Range(0, 2) == 0)
            moveLeft = !moveLeft;

        Material color = colors[Random.Range(0, colors.Length)];
        ball.GetComponent<MeshRenderer>().enabled = true;
        ball.GetComponent<ParticleSystem>().Stop();
        ball.GetComponent<MeshRenderer>().material = color;
        ball.GetComponent<ParticleSystemRenderer>().material = color;
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        if (moveLeft)
            x = speed * Time.deltaTime;
        else
            x = -speed * Time.deltaTime;

        if (move + x >= -0.35 && move + x <= 0.35)
            move += x;
        else
            moveLeft = !moveLeft;
        ball.transform.position = transform.position + new Vector3(move, 0.1f, 0);
    }
}
