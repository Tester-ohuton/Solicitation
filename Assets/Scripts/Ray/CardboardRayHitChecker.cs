using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardboardRayHitChecker : MonoBehaviour
{
    public UnityEvent onCardboardHit; // ヒット時に実行するイベント
    public GameObject playerObject;

    private GameObject transformObject;
    private string targetTag = "Cardboard"; // 段ボールのタグ名
    private string doorTag = "Door"; // ドアのタグ名
    private string entranceDoorTag = "EntranceDoor"; // 入口のドアのタグ名
    private float rayLength = 3f; // レイの長さ

    /// <summary>
    /// Rayが段ボール（cardboard）にヒットしたかどうかだけ判定する
    /// </summary>
    /// <returns>true=ヒット, false=ヒットなし</returns>
    public bool IsRayHitCardboard(Ray ray, float distance)
    {
        return Physics.Raycast(ray, distance);
    }

    public void Intaract(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    // アイテムをプレイヤーの子オブジェクトに移動させる
                    obj = hit.collider.gameObject;
                    // アイテムをインタラクトする処理
                    if (obj == null)
                    {
                        obj.transform.SetParent(playerObject.transform);
                        obj.transform.position = new Vector3(0.5f, -0.5f, 1f); // 適切な位置に調整
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag(doorTag))
                {
                    // アイテムをプレイヤーの子オブジェクトに移動させる
                    obj = hit.collider.gameObject;
                    // アイテムをインタラクトする処理
                    if (obj == null)
                    {
                        obj.transform.SetParent(playerObject.transform);
                        obj.transform.position = new Vector3(0.5f, -0.5f, 1f); // 適切な位置に調整
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag(entranceDoorTag))
                {
                    // アイテムをプレイヤーの子オブジェクトに移動させる
                    obj = hit.collider.gameObject;
                    // アイテムをインタラクトする処理
                    if (obj == null)
                    {
                        obj.transform.SetParent(playerObject.transform);
                        obj.transform.position = new Vector3(0.5f, -0.5f, 1f); // 適切な位置に調整
                    }
                }
            }
        }
    }

    void Start()
    {
        onCardboardHit.RemoveAllListeners(); // 念のため、登録されているリスナーを全て削除しておく
        onCardboardHit.AddListener(() =>
        {
            Debug.Log("段ボールにヒットしました！");
            Intaract(transformObject);
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 極限まで単純なbool判定、タグも見ない
            if (IsRayHitCardboard(ray, 100f))
            {
                onCardboardHit.Invoke(); // 登録したイベント群を実行
            }
        }
    }
}

