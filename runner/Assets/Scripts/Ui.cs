using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject restart;
    // Start is called before the first frame update
    void Start()
    {
        restart.SetActive(false);
        start.SetActive(true);
        Time.timeScale = 0;
    }


    public void OnClick_Start()
    {
        Time.timeScale = 1;
        start.SetActive(false);
    }

    public void OnClick_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
