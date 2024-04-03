using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemType : MonoBehaviour
{
    public string itemType;
    public bool isOK;

    [SerializeField] private GameObject itemObj;
    [SerializeField] SphereCollider sphereCollider;

    private void Awake()
    {
        itemObj.SetActive(true);
    }

    public IEnumerator ColliderEnabled()
    {
        yield return new WaitForSeconds(1f);
        sphereCollider.enabled = false;
    }
}
