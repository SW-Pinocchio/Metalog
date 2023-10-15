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

    private void Awake()
    {
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
                    m_post.OpenPost(obj.GetComponent<ObjectPost>().GetID());
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
