using UnityEngine;

public enum ItemType
{
    Type1,
    Type2,
    Type3
}

public class Item : MonoBehaviour
{
    [Header("�A�C�e���^�C�v")]
    public ItemType itemType;

    public bool isOK;

    [SerializeField] string ID;

    void Start()
    {
        //������ �擾��Ԃ̊m�F
        if (ItemLogger.Contains(ID))
        {
            Debug.Log("�A�C�e�����l���ς�");

            //Destroy(gameObject);
        }
        else
        {
            //Debug.Log("�i�{�[�����J���ĂȂ���");
        }
    }

    public void GetCardBoard()
    {
        //Debug.Log($"ID + {ID}");
        ItemLogger.Add(ID);
    }
}