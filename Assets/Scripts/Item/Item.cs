using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Type1,
    Type2,
    Type3
}

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public ItemType itemType;

    public bool isOK;

    public static UnityEvent onItemCollected = new UnityEvent();

    [SerializeField] string ID;

    void Start()
    {
        onItemCollected.RemoveAllListeners();
        onItemCollected.AddListener(GetCardBoard);

        if (ItemLogger.Contains(ID))
        {
            Debug.Log("This item has already been collected: " + ID);

            //Destroy(gameObject);
        }
        else
        {
            //Debug.Log("This item is new: " + ID);
        }
    }

    public void GetCardBoard()
    {
        //Debug.Log($"ID + {ID}");
        ItemLogger.Add(ID);
    }
}