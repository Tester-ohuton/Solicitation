using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string ID;

    void Start()
    {
        //初期化 取得状態の確認
        if (ItemLogger.Contains(ID))
        {
            Debug.Log("アイテムを獲得済み");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("段ボールを開けてないよ");
        }
    }

    public void GetCardBoard()
    {
        Debug.Log($"ID + {ID}");
        ItemLogger.Add(ID);
        Destroy(gameObject);
    }
}