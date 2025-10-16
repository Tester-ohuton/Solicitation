using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverEffect : MonoBehaviour
{
    public GameObject enemy;  //敵
    public Transform entryPoint;  //出現場所
    public Transform playerPosition;  //プレイヤー座標
    public GameObject gameOverPanel;    //ゲームオーバーUI
    public string sceneName = "TitleScene";

    public float moveSpeed = 2.0f;

    Vector3 targetPosition;

    public void PlayGameOverEffect()
    {
        //敵の場所を設定
        enemy.transform.position = entryPoint.position;

        if (enemy.activeInHierarchy)
        {
            //プレイヤーを見つけて追いかける
            StartCoroutine(MoveEnemyToPlayer());
        }
    }

    public void EnemyActive(bool isActive)
    {
        enemy.SetActive(isActive);
    }

    private IEnumerator MoveEnemyToPlayer()
    {
        while (Vector3.Distance(enemy.transform.position, playerPosition.position) > 0.1f)
        {
            //実際に距離を詰めるプログラム
            targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

            // 移動するプログラム
            enemy.transform.position = Vector3.MoveTowards(
                enemy.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        //SE再生
        SoundManager.Instance.PlaySE3D(SESoundData.SE.Dead, targetPosition);

        //Log
        Debug.Log("Game Over! The enemy has reached you.");

        GameManager.instance.isGameOver = true;

        //ゲームオーバーUIの表示
        gameOverPanel.SetActive(true);

        //待機
        yield return new WaitForSeconds(0.1f);

        //シーンチェンジ
        SceneManager.LoadScene(sceneName);
    }
}
