using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverEffect : MonoBehaviour
{
    public GameObject enemy;  // �G�L�����N�^�[�̃I�u�W�F�N�g
    public Transform entryPoint;  // �G�L�����N�^�[�̐N���J�n�n�_
    public Transform playerPosition;  // �v���C���[�̈ʒu
    public GameObject gameOverPanel;
    public string sceneName = "Title1week";

    public float moveSpeed = 2.0f;

    Vector3 targetPosition;

    public void PlayGameOverEffect()
    {
        // �G�L�����N�^�[��N���J�n�n�_�Ɉړ�
        enemy.transform.position = entryPoint.position;

        // �G�L�����N�^�[���A�N�e�B�u�ɂ��čs���J�n
        StartCoroutine(MoveEnemyToPlayer());
    }

    public void EnemyActive(bool isActive)
    {
        enemy.SetActive(isActive);
    }

    private IEnumerator MoveEnemyToPlayer()
    {
        while (Vector3.Distance(enemy.transform.position, playerPosition.position) > 0.1f)
        {
            // �v���C���[�Ƃ̌��݂̈ʒu���擾���AY�����Œ�
            targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

            // XZ���ʂňړ�
            enemy.transform.position = Vector3.MoveTowards(
                enemy.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // SE���Đ�
        SoundManager.Instance.PlaySE(SESoundData.SE.Dead, targetPosition);

        // �v���C���[�ɓ��B�����̂ŃQ�[���I�[�o�[����
        Debug.Log("Game Over! The enemy has reached you.");

        // �Q�[���I�[�o�[�p�l����\��
        gameOverPanel.SetActive(true);

        // 3�b�ҋ@���ăV�[����J��
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName);
    }
}
