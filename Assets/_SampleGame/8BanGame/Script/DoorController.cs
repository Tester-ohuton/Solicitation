using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public bool isOpened;
    public int ID;
    public GameObject doorObj;

    private Animator animator;

    public static UnityEvent OnDoorCloseAnimation = new UnityEvent();

    private void Awake()
    {
        OnDoorCloseAnimation.RemoveAllListeners();

        OnDoorCloseAnimation.AddListener(() =>
        {
            ResetDoorAnimation();
        });
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void OpenDoorAnimation()
    {
        animator.SetBool("openAnim", true);

        doorObj.SetActive(false);

        /*
        if (ID == 99)
        {
            ShowButtonUI();
        }
        */
    }

    public void ResetDoorAnimation()
    {
        animator.SetBool("openAnim", false);
        doorObj.SetActive(true);
    }

    private void ShowButtonUI()
    {
        // ボタンUIを表示する処理をここに記述
        // 例えば、UIマネージャーを呼び出してボタンを表示する
        UIManager.instance.ShowEventButton();
    }
}
