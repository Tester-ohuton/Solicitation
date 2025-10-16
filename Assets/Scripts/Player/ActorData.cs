using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ActorData")]
public class ActorData : ScriptableObject   //ScriptableObjectを継承する
{
    public string id;          //登録ID

    public string charName;    //キャラクターの名前

    // 追加していく

}