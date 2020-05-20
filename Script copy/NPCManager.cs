using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC interactions
/// </summary>

public class NPCManager : MonoBehaviour
{
    public GameObject TipImage;

    public GameObject DialogImage;

    public float showTime = 4;

    public float showTimer = -1;

    // Start is called before the first frame update
    void Start()
    {
        TipImage.SetActive(true);
        DialogImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;

        if (showTimer < 0)
        {
            TipImage.SetActive(true);
            DialogImage.SetActive(false);

        }
    }

    public void ShowDialog()
    {
        showTimer = showTime;
        DialogImage.SetActive(true);
        TipImage.SetActive(false);
    }
}
