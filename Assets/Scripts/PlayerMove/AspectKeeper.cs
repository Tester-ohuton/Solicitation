using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ｶﾒﾗの表示領域を調整する
[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    [SerializeField] private Camera camera_;      // 対象のｶﾒﾗ
    [SerializeField] private Vector2 aspectVec_;  // 参照解像度
    [SerializeField] private float pixelPerUnit_; // 画像のPixelPerUnit
    private float currentAspect_ = 0.0f;          // 現在のｱｽﾍﾟｸﾄ比

    void Update()
    {
        // ｶﾒﾗの表示領域の調整
        SetCameraViewArea();
    }

    // ｶﾒﾗの表示領域の調整
    private void SetCameraViewArea()
    {
        // 現在のｱｽﾍﾟｸﾄ比を取得し、変化がなければ処理を抜ける
        float currentAspect = (float)Screen.height / (float)Screen.width;

        if (Mathf.Approximately(currentAspect_, currentAspect))
        {
            return;
        }

        currentAspect_ = currentAspect;

        // ｶﾒﾗｻｲｽﾞの調整
        camera_.orthographicSize = aspectVec_.y / pixelPerUnit_ / 2;

        // ViewportRectの調整
        SetViewportRect();
    }

    // ViewportRectの調整
    private void SetViewportRect()
    {
        float baseAspect = aspectVec_.y / aspectVec_.x;     // 基準のｱｽﾍﾟｸﾄ比

        if (baseAspect > currentAspect_)
        {
            // 画面が横に広い場合、基準のｱｽﾍﾟｸﾄ比に合わせて調整する
            float bgScale = aspectVec_.y / Screen.height;

            // viewportRectの幅
            float tmpWidth = aspectVec_.x / (Screen.width * bgScale);

            // viewportRectを設定
            camera_.rect = new Rect(
             (1.0f - tmpWidth) / 2, 0.0f, tmpWidth, 1.0f);
        }
        else
        {
            // 画面が縦に長い場合、画面の長さにｱｽﾍﾟｸﾄ比を追従させる
            float bgScale = currentAspect_ / baseAspect;

            // ｶﾒﾗのｻｲｽﾞを縦の長さに合わせて設定しなおす
            camera_.orthographicSize *= bgScale;

            // viewportRectを設定(範囲内全てを表示)
            camera_.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }
}