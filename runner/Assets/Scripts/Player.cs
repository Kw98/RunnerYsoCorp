using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Material white;
    [SerializeField] private Material[] colors;
    [SerializeField] private GameObject restart;
    public int index = 0;
    private Material color;
    public int points = 0;

    private void Start()
    {
        index = Random.Range(0, colors.Length);
        color = GetComponent<MeshRenderer>().material;
        color.CopyPropertiesFromMaterial(colors[index]);
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material.color == colors[i].color)
                index = i;
        }
        color.CopyPropertiesFromMaterial(colors[index]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Material other = collision.gameObject.GetComponent<MeshRenderer>().material;
        if (colors[index].name + " (Instance)" == other.name)
        {
            color.CopyPropertiesFromMaterial(white);
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine("Reset", 0.15f);
            points += 1;
        } else
        {
            Time.timeScale = 0;
            restart.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Reset(float Count)
    {
        yield return new WaitForSeconds(Count);
        color.CopyPropertiesFromMaterial(colors[index]);
        yield return null;
    }

}
