using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CreateFurniture : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ �������� ���⿡ �������ּ���.
    private GameObject spawnedPrefab;
    private ObjectManager objectManager;
    private Collider myCollider; // �ݶ��̴� ������Ʈ�� ������ ����

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
            // ���콺�� ���� ä�� �����̸� �������� ���콺 ��ġ�� �̵���ŵ�ϴ�.
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
        // ���콺 �������� ȭ�� ��ġ�� ���� ��ǥ�� ��ȯ�մϴ�.
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
