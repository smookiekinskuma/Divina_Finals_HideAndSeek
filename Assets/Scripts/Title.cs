using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Title : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject title;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private string Text_Update;
    [SerializeField] private int count;

    void Update()
    {
        Text.text = Text_Update;

        switch (count)
        {
            case 0:
                tutorial.SetActive(false);
                title.SetActive(true);
                Text_Update = "Start";
                break;
            case 1:
                tutorial.SetActive(true);
                title.SetActive(false);
                Text_Update = "I'm Ready";
                break;
            case 2:
                SceneManager.LoadScene("Game");
                break;
        }
    }

    public void Count()
    {
        count++;
    }
}
