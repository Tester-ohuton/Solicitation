using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    public float interactionDistance = 3f; // レイの最大距離
    public KeyCode interactKey = KeyCode.E; // インタラクトに使うキー
    public List<Slider> interactionSlider=new List<Slider>(); // インタラクトゲージを表示するSlider
    public List<GameObject> sliderPanel=new List<GameObject>(); // SliderPanelを表示するためのパネル
    public GameObject cardboardOpen;
    public float currentValue;

    private bool isInteracting = false; // インタラクト中かどうか
    private float interactionProgress = 0f; // インタラクトの進捗度
    private int i = 0;

    PlayerLife playerLife;
    Outline outline;
    Item item;

    private void Start()
    {
        playerLife = GetComponent<PlayerLife>();

        // 初期状態ではゲージパネルを非表示にする
        foreach (var panel in sliderPanel)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (!SceneFlagManager.Instance.isPlayerMoving || 
            SceneFlagManager.Instance.isSetting ||
            playerLife.isGameOver)
            return;

        // レイを飛ばしてインタラクト可能なオブジェクトを検出
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Card"))
            {
                // Outline取得
                outline = hit.collider.GetComponent<Outline>();

                // Item取得
                item = hit.collider.GetComponent<Item>();

                // Eキーが押され、かつインタラクト中でない場合
                if (Input.GetKeyDown(interactKey) && !isInteracting)
                {
                    // パネル表示
                    sliderPanel[i].SetActive(true);

                    isInteracting = true;

                    // アウトライン表示
                    outline.enabled = true;
                }

                // Eキーを押している間はSliderを増やす
                if (isInteracting)
                {
                    interactionProgress += Time.deltaTime * currentValue;
                    interactionSlider[i].value = interactionProgress;

                    // アウトライン表示
                    outline.enabled = true;

                    if (100 <= interactionProgress)
                    {

                        ItemText.OnItemText.Invoke();

                        sliderPanel[i].SetActive(false);

                        // インデックスを次のゲージに進める
                        i++;

                        // インデックスがゲージ数以上の場合はリセット
                        if (interactionSlider.Count <= i)
                        {
                            i = 0;
                        }

                        // プレハブを生成
                        Instantiate(cardboardOpen, hit.collider.transform.position, Quaternion.identity);

                        // Item取得
                        item.GetCardBoard();

                        // アイテムを消すなどの処理を実行
                        Destroy(hit.collider.gameObject);

                        // ゲージをリセット
                        interactionProgress = 0;

                        // インタラクトを終了
                        isInteracting = false;
                    }
                }

                // Eキーが離されたらSliderPanelを非表示にする
                if (Input.GetKeyUp(interactKey))
                {
                    isInteracting = false;

                    // アウトライン非表示
                    outline.enabled = false;
                }
            }
            else if(hit.collider.CompareTag("Door"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("DoorOpen");
                    Animator animator = hit.collider.GetComponentInParent<Animator>();
                    animator.SetBool("openAnim", true);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("DoorClose");
                    Animator animator = hit.collider.GetComponentInParent<Animator>();
                    animator.SetBool("openAnim", false);
                }
            }
            else
            {
                // Eキーを離してもゲージをリセットしない
                if (!Input.GetKey(interactKey))
                {
                    isInteracting = false;
                }
            }
        }
        else
        {
            // Eキーを離してもゲージをリセットしない
            if (!Input.GetKey(interactKey))
            {
                isInteracting = false;
                sliderPanel[i].SetActive(false);
            }
        }
    }
}