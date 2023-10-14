using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartSetting : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
