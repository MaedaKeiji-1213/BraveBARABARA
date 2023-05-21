using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLimit : MonoBehaviour
{
    [SerializeField]
    [Tooltip("最小角度(-180～180")]
    private float MinAngle;

    [SerializeField]
    [Tooltip("最大角度(-180～180")]
    private float MaxAngle;

    [SerializeField]
    [Tooltip("回転するスピード")]
    private float rotationSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        // 左右キーの入力を取得
        float horizontal = Input.GetAxis("Horizontal");
        // 現在のGameObjectのY軸方向の角度を取得
        float currentYAngle = transform.eulerAngles.y;
        // 現在の角度が180より大きい場合
		if (currentYAngle > 180)
        {
            // デフォルトでは角度は0～360なので-180～180となるように補正
            currentYAngle = currentYAngle - 360;
        }
        // (現在の角度が最小角度以上かつキー入力が0未満(左キー押下)) または (現在の角度が最大角度以下かつキー入力が0より大きい(右キー押下))の時
        if ((currentYAngle >= MinAngle && horizontal < 0) || (currentYAngle <= MaxAngle && horizontal > 0))
        {
            // Y軸を基準に回転させる
            transform.Rotate(new Vector3(0, horizontal * rotationSpeed, 0));
        }
    }
}