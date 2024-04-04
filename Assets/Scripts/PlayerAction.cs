using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    public float interactionDistance = 2f; // レイの最大距離
    public KeyCode interactKey = KeyCode.E; // インタラクトに使うキー
    public List<Slider> interactionSlider=new List<Slider>(); // インタラクトゲージを表示するSlider
    public List<GameObject> sliderPanel=new List<GameObject>(); // SliderPanelを表示するためのパネル
    public GameObject cardboardOpen;
    public float currentValue;

    private bool isInteracting = false; // インタラクト中かどうか
    private float interactionProgress = 0f; // インタラクトの進捗度
    private int i = 0;

    PlayerLife playerLife;

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
        if (!SceneFlagManager.Instance.isPlayerMoving || playerLife.isGameOver)
            return;

        // レイを飛ばしてインタラクト可能なオブジェクトを検出
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Card"))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                outline.enabled = true;

                // Eキーが押され、かつインタラクト中でない場合
                if (Input.GetKeyDown(interactKey) && !isInteracting)
                {
                    // パネル表示
                    sliderPanel[i].SetActive(true);

                    isInteracting = true;
                }

                // Eキーを押している間はSliderを増やす
                if (isInteracting)
                {
                    interactionProgress += Time.deltaTime * currentValue;
                    interactionSlider[i].value = interactionProgress;

                    if(100 <= interactionProgress)
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
                }
            }
            else if(hit.collider.CompareTag("Item"))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                outline.enabled = true;

                PickupObj pickupObj = hit.collider.GetComponent<PickupObj>();
                pickupObj.OnPointerClick();
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