using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChange : MonoBehaviour
{
    [SerializeField]
    private GameObject[] beard;
    [SerializeField]
    private GameObject[] hair;
    [SerializeField]
    private GameObject[] cap;
    [SerializeField]
    private GameObject[] cloth;
    private void Awake()
    {
        if(PlayerPrefs.GetInt("beard") != beard.Length)
            beard[PlayerPrefs.GetInt("beard")].SetActive(true);
        if (PlayerPrefs.GetInt("hair") != hair.Length)
            hair[PlayerPrefs.GetInt("hair")].SetActive(true);
        if (PlayerPrefs.GetInt("cap") != cap.Length)
            cap[PlayerPrefs.GetInt("cap")].SetActive(true);

        cloth[PlayerPrefs.GetInt("cloth")].SetActive(true);
    }
}
