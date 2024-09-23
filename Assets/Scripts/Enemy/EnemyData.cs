using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject   //ScriptableObject���p������
{
    public string id;          //�o�^ID

    public string charName;    //�L�����N�^�[�̖��O

    // �ǉ����Ă���
    public float moveSpeed;

    
}