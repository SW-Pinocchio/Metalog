using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class CreateFurniture : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ �������� ���⿡ �������ּ���.
    private GameObject spawnedPrefab;
    private ObjectManager objectManager;

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }
    public void Onclick()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        spawnedPrefab = Instantiate(prefabToSpawn, mousePosition, Quaternion.identity);
        objectManager.AddObject(spawnedPrefab);
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
            // ���콺���� ���� ���� �� �������� �� ��ġ�� ����ϴ�.
            spawnedPrefab = null;
            Cursor.visible = true;
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
                hit.point = hit.point - new Vector3(0, 4, 0);

                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
