using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClearEffect : MonoBehaviour
{
    public GameObject outsideClearCollider;  // ClearColliderのオブジェクト
    public Transform entryPoint;  // ClearColliderの侵入開始地点
    public Transform playerPosition;  // プレイヤーの位置

    [Space]
    public Image whiteScreen;  // 全画面に配置された白いイメージ
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

        // ここでクリア後の処理を追加（シーン遷移やメッセージ表示など）
        Debug.Log("Game Cleared!");
        SceneManager.LoadScene(sceneName);
    }


    public void PlayGameClearEffect()
    {
        // ClearColliderを侵入開始地点に移動
        outsideClearCollider.transform.position = entryPoint.position;

        // ClearColliderをアクティブにして行動開始
        StartCoroutine(MovePlayerToCollider());
    }

    private IEnumerator MovePlayerToCollider()
    {
        outsideClearCollider.SetActive(true);

        if(Vector3.Distance(outsideClearCollider.transform.position, playerPosition.position) > 0.1f)
        {
            // プレイヤーとの現在の位置を取得し、Y軸を固定
            targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

            yield return null;
        }

        GameManager.instance.isCleared = true;

        // SEを再生
        SoundManager.Instance.PlaySE(SESoundData.SE.Dead, targetPosition);

        // ClearColliderに到達したのでゲームクリア処理
        Debug.Log("Game Cleared!");

        // ここでクリア後の処理を追加（シーン遷移やメッセージ表示など）
        gameClearPanel.SetActive(true);

        // 3秒待機してシーンを遷移
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName);
    }
}
