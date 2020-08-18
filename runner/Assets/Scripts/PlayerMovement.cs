using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject roadPath;
    [SerializeField] private float speed = 3f;
    private Vector3 touchPos;
    private float deltaX;
    private float actualX = 0;
    private Rigidbody rb;
    private float distDone = 0;
    private float move = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        distDone += 3f * Time.deltaTime;
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Road");
        if (paths.Length > 1)
        {
            if (distDone - 60f >= 0)
            {
                foreach (GameObject path in paths)
                {
                    if (path != roadPath)
                    {
                        roadPath = path;
                        distDone = 0;
                        break;
                    }
                }
            }
        }
        Vector3 rpath = roadPath.GetComponent<PathCreator>().path.GetPointAtDistance(distDone, EndOfPathInstruction.Stop);
        transform.rotation = roadPath.GetComponent<PathCreator>().path.GetRotationAtDistance(distDone, EndOfPathInstruction.Stop);
        transform.Rotate(new Vector3(0, 0, 1), 90);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x;
                    rb.velocity = Vector3.zero;
                    break;
                case TouchPhase.Moved:
                    Debug.Log("actualx:" + actualX);
                    float newX;
                    if (touchPos.x > deltaX)
                        newX = speed * Time.deltaTime;
                    else
                        newX = -speed * Time.deltaTime;
                    if (move + newX >= -0.35 && move + newX <= 0.35)
                        move += newX;
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector3.zero;
                    break;
            }
        }
        transform.position = new Vector3(rpath.x + move, 0.075f, rpath.z);
    }
}
