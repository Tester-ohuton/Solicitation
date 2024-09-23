using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject   //ScriptableObjectを継承する
{
    public string id;          //登録ID

    public string charName;    //キャラクターの名前

    // 追加していく
    public float moveSpeed;

    
}