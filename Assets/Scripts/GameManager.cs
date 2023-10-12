using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool decoMode = false;

    [SerializeField]
    Camera camDeco;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject uiCanvas;


    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }

    private void Start()
    {
        decoMode = false;
        DecoModeSetting();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(decoMode)
            {
                decoMode = false;
            }
            else
            {
                decoMode= true;
            }
            DecoModeSetting();
        }
    }

    public void DecoModeSetting()
    {
        Player.SetActive(!decoMode);
        camDeco.gameObject.SetActive(decoMode);
        uiCanvas.SetActive(decoMode);
        if(decoMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

}
