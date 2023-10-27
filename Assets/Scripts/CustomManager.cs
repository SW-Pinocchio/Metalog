using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] beard;
    [SerializeField]
    private GameObject[] hair;
    [SerializeField]
    private GameObject[] cap;
    [SerializeField]
    private GameObject[] cloth;

    [SerializeField]
    private GameObject UI;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("beard") != beard.Length)
            beard[PlayerPrefs.GetInt("beard")].SetActive(true);
        if (PlayerPrefs.GetInt("hair") != hair.Length)
            hair[PlayerPrefs.GetInt("hair")].SetActive(true);
        if (PlayerPrefs.GetInt("cap") != cap.Length)
            cap[PlayerPrefs.GetInt("cap")].SetActive(true);

        cloth[PlayerPrefs.GetInt("cloth")].SetActive(true);
    }


    public void ChangeBeard()
    {
        PlayerPrefs.SetInt("beard", (PlayerPrefs.GetInt("beard", 0) + 1) % (beard.Length+1));
        foreach (GameObject go in beard)
        {
            go.SetActive(false);
        }
        if(PlayerPrefs.GetInt("beard") != beard.Length)
        {
            beard[PlayerPrefs.GetInt("beard")].SetActive(true);
        }
        
    }

    public void ChangeHair()
    {
        PlayerPrefs.SetInt("hair", (PlayerPrefs.GetInt("hair",0) + 1) % (hair.Length+1));
        foreach (GameObject go in hair)
        {
            go.SetActive(false);
        }
        if (PlayerPrefs.GetInt("hair") != hair.Length)
        {
            hair[PlayerPrefs.GetInt("hair")].SetActive(true);
        }
    }

    public void ChangeCap()
    {
        PlayerPrefs.SetInt("cap", (PlayerPrefs.GetInt("cap", 0) + 1) % (cap.Length + 1));
        foreach (GameObject go in cap)
        {
            go.SetActive(false);
        }
        if (PlayerPrefs.GetInt("cap") != cap.Length)
        {
            cap[PlayerPrefs.GetInt("cap")].SetActive(true);
        }
    }

    public void ChangeCloth()
    {
        PlayerPrefs.SetInt("cloth", (PlayerPrefs.GetInt("cloth", 0) + 1) % cloth.Length);
        foreach (GameObject go in cloth)
        {
            go.SetActive(false);
        }
        cloth[PlayerPrefs.GetInt("cloth")].SetActive(true);
    }

    public void closeUI()
    {
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
