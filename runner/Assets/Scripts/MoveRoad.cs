using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float objDist = 0f;
    [SerializeField] private float despawnDist = -60f;
    [SerializeField] private float spawnDist = 60f;
    private bool spawn = true;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
        if (transform.position.z <= objDist && spawn)
        {
            ObjectPool.Instance.SpawnFromPool("road", new Vector3(0, 0, spawnDist), Quaternion.identity);
            spawn = false;
        }
        else if (transform.position.z <= despawnDist)
        {
            gameObject.SetActive(false);
            spawn = true;
        }
    }
}
