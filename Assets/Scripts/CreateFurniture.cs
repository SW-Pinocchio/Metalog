using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CreateFurniture : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹을 여기에 연결해주세요.
    private GameObject spawnedPrefab;
    private ObjectManager objectManager;
    private Collider myCollider; // 콜라이더 컴포넌트를 참조할 변수

    [SerializeField]
    private Image deleteZone;
    [SerializeField]
    private GameObject DecoUI;
    [SerializeField]
    private int objType;
    [SerializeField]
    private GameObject Parent;

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }
    public void Onclick()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        spawnedPrefab = Instantiate(prefabToSpawn, mousePosition, Quaternion.identity);
        myCollider = spawnedPrefab.transform.GetComponent<Collider>();
        myCollider.enabled = false;
        spawnedPrefab.transform.SetParent(Parent.transform);
        deleteZone.gameObject.SetActive(true);
        DecoUI.gameObject.SetActive(true);
        Cursor.visible = false;
    }
    void Update()
    {
        if (spawnedPrefab != null)
        {
            // 마우스를 누른 채로 움직이면 프리팹을 마우스 위치로 이동시킵니다.
            Vector3 mousePosition = GetMouseWorldPosition();
            spawnedPrefab.transform.position = mousePosition;
        }
        
        if (Input.GetMouseButtonDown(0) && spawnedPrefab != null)
        {
            if(GetMouseWorldPosition() != Vector3.zero)
            {
                objectManager.AddObject(spawnedPrefab, objType);
                deleteZone.gameObject.SetActive(false);
                DecoUI.gameObject.SetActive(false);
                myCollider.enabled = true;
                spawnedPrefab = null;
                Cursor.visible = true;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Q) && spawnedPrefab != null) {
            spawnedPrefab.transform.Rotate(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.E) && spawnedPrefab != null)
        {
            spawnedPrefab.transform.Rotate(0, 90, 0);
        }

        if(GetMouseWorldPosition().z > 8)
        {
            DecoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
        }
        else
        {
            DecoUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // 마우스 포인터의 화면 위치를 월드 좌표로 변환합니다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            string hitObjectTag = hit.collider.gameObject.tag;
            if (hitObjectTag == "Floor")
            {
                return hit.point;
            }
        }
        return Vector3.zero;
        
    }
}
