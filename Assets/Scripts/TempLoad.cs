using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLoad : MonoBehaviour
{
    ObjectManager objectManager;

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }
    public void Onclick()
    {
        objectManager.LoadLegacyFurniture();
    }
}
