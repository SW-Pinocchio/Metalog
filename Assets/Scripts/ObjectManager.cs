using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> objectsList = new List<GameObject>();

    public void AddObject(GameObject obj)
    {
        objectsList.Add(obj);
        if(objectsList.Count > 1)
        {
            Debug.Log(objectsList[objectsList.Count - 2].gameObject.transform.position);
        }
        
        // ������Ʈ �߰� �� ���ϴ� �۾� ����
    }

    // �ٸ� ���� ��ɵ��� �߰��� �� �ֽ��ϴ�.
}