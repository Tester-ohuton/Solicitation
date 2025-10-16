using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClearEffect : MonoBehaviour
{
    public GameObject outsideClearCollider;  // ClearCollider�̃I�u�W�F�N�g
    public Transform entryPoint;  // ClearCollider�̐N���J�n�n�_
    public Transform playerPosition;  // �v���C���[�̈ʒu

    [Space]
    public Image whiteScreen;  // �S��ʂɔz�u���ꂽ�����C���[�W
    public float fadeDuration = 1.0f;
    public GameObject gameClearPanel;
    public string sceneName = "Title1week";

    Vector3 targetPosition;

    public void PlayClearEffect()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        gameClearPanel.SetActive(true);
        GameManager.instance.isCleared = true;

        float elapsedTime = 0f;
        Color color = whiteScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            whiteScreen.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        // �����ŃN���A��̏�����ǉ��i�V�[���J�ڂ⃁�b�Z�[�W�\���Ȃǁj
        Debug.Log("Game Cleared!");
        SceneManager.LoadScene(sceneName);
    }


    public void PlayGameClearEffect()
    {
        // ClearCollider��N���J�n�n�_�Ɉړ�
        outsideClearCollider.transform.position = entryPoint.position;

        // ClearCollider���A�N�e�B�u�ɂ��čs���J�n
        StartCoroutine(MovePlayerToCollider());
    }

    private IEnumerator MovePlayerToCollider()
    {
        outsideClearCollider.SetActive(true);

        if(Vector3.Distance(outsideClearCollider.transform.position, playerPosition.position) > 0.1f)
        {
            // �v���C���[�Ƃ̌��݂̈ʒu���擾���AY�����Œ�
            targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

            yield return null;
        }

        GameManager.instance.isCleared = true;

        // SE���Đ�
        SoundManager.Instance.PlaySE3D(SESoundData.SE.Dead, targetPosition);

        // ClearCollider�ɓ��B�����̂ŃQ�[���N���A����
        Debug.Log("Game Cleared!");

        // �����ŃN���A��̏�����ǉ��i�V�[���J�ڂ⃁�b�Z�[�W�\���Ȃǁj
        gameClearPanel.SetActive(true);

        // 3�b�ҋ@���ăV�[����J��
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName);
    }
}
