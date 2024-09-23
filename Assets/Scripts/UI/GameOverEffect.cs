using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverEffect : MonoBehaviour
{
    public GameObject enemy;  // 敵キャラクターのオブジェクト
    public Transform entryPoint;  // 敵キャラクターの侵入開始地点
    public Transform playerPosition;  // プレイヤーの位置
    public GameObject gameOverPanel;
    public string sceneName = "Title1week";

    public float moveSpeed = 2.0f;

    Vector3 targetPosition;

    public void PlayGameOverEffect()
    {
        // 敵キャラクターを侵入開始地点に移動
        enemy.transform.position = entryPoint.position;

        // 敵キャラクターをアクティブにして行動開始
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
            // プレイヤーとの現在の位置を取得し、Y軸を固定
            targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

            // XZ平面で移動
            enemy.transform.position = Vector3.MoveTowards(
                enemy.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // SEを再生
        SoundManager.Instance.PlaySE(SESoundData.SE.Dead, targetPosition);

        // プレイヤーに到達したのでゲームオーバー処理
        Debug.Log("Game Over! The enemy has reached you.");

        // ゲームオーバーパネルを表示
        gameOverPanel.SetActive(true);

        // 3秒待機してシーンを遷移
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName);
    }
}
