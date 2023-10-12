using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFurniture : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ �������� ���⿡ �������ּ���.
    private GameObject spawnedPrefab;

    private bool clicked;


    private void Awake()
    {
        clicked = false;
    }
    public void Onclick()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        spawnedPrefab = Instantiate(prefabToSpawn, mousePosition, Quaternion.identity);
        Debug.Log("spawn");
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
            Debug.Log("Drop");
            
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
                Debug.Log(hit.point);

                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
