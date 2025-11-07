using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelect;

    public KeyCode press_Key1 = KeyCode.C;

    // Start is called before the first frame update
    void Start()
    {
        firstSelect.GetComponent<Button>().Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(press_Key1))
        {
            firstSelect.GetComponent<Button>().Select();
        }
    }
}
