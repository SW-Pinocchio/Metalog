using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFurniture : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹을 여기에 연결해주세요.
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
            // 마우스를 누른 채로 움직이면 프리팹을 마우스 위치로 이동시킵니다.
            Vector3 mousePosition = GetMouseWorldPosition();
            spawnedPrefab.transform.position = mousePosition;
        }
        
        if (Input.GetMouseButtonDown(0) && spawnedPrefab != null)
        {
            // 마우스에서 손을 뗐을 때 프리팹을 그 위치에 남깁니다.
            spawnedPrefab = null;
            Debug.Log("Drop");
            
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
                hit.point = hit.point - new Vector3(0, 4, 0);
                Debug.Log(hit.point);

                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
