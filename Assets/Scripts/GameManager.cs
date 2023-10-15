using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 0 = 일반, 1 = 데코, 2 = 글 작성
    public static int decoMode = 0;

    [SerializeField]
    Camera camDeco;
    [SerializeField]
    Camera camChar;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject uiCanvas;
    [SerializeField]
    GameObject uiAim;
    [SerializeField]
    LayerMask layerMask;

    PostManager m_post;


    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
        m_post = FindObjectOfType<PostManager>();
    }

    private void Start()
    {
        decoMode = 0;
        DecoModeSetting();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            switch (decoMode)
            {
                case 0:
                    decoMode = 1;
                    break;
                case 1:
                    decoMode = 0;
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            DecoModeSetting();
        }

        if(decoMode == 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject obj = GetMouseWorldPosition();
                if(obj != null)
                {
                    Debug.Log(obj.GetComponent<ObjectPost>().GetID());
                    m_post.OpenPost(obj.GetComponent<ObjectPost>().GetID(), obj.GetComponent<ObjectPost>().GetNum());
                    decoMode = 2;
                    DecoModeSetting();
                }
            }
        }
    }

    public void DecoModeSetting()
    {
        switch(decoMode)
        {
            case 0:
                Player.SetActive(true);
                camDeco.gameObject.SetActive(false);
                uiCanvas.SetActive(false);
                uiAim.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case 1:
                Player.SetActive(false);
                camDeco.gameObject.SetActive(true);
                uiCanvas.SetActive(true);
                uiAim.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case 2:
                Player.SetActive(true);
                camDeco.gameObject.SetActive(false);
                uiCanvas.SetActive(false);
                uiAim.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            default:
                break;
        }
        
    }


    GameObject GetMouseWorldPosition()
    {
        float maxRayDistance = 100f;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = camChar.ScreenPointToRay(screenCenter);

        // 레이어 마스크 설정하여 특정 레이어의 콜라이더를 무시하도록 함
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxRayDistance, layerMask))
        {
            string hitObjectTag = hitInfo.collider.gameObject.tag;
            if (hitObjectTag == "Furniture")
            {
                return hitInfo.collider.gameObject;
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    public void closeUI()
    {
        m_post.closeUI();
        decoMode = 0;
        DecoModeSetting();
    }

}
