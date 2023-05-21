using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//同じ名前のクラスの分割
//partial class を使うと、同じ名前のクラスが別のファイル
//で定義される

partial class KyojinController
{
    //enumを使ったアニメーション番号の定義
    //stay=0,Run=1...といった連番が設定される
    enum AnimNo
    {
        giant_walk, germanSuplex,giant_attack, giant_drop, giant_throw
    };
    //AnimNoに対応した文字列配列を作成
    //Unityで設定したアニメーションの
    //名前と一致していないといけない
    string[] NameTbl =
    {
        "giant_walk","germanSuplex","giant_attack","giant_drop","giant_throw"
    };
    //コンポーネント用
    Animator animator;
    //変数
    AnimNo oldAnimNo = 0;   //現在再生中のアニメーション番号
    AnimNo newAnimNo = 0;   //新しく設定するアニメーション番号


    //初期化メソッド
    void InitAnim()
    {
        animator = GetComponent<Animator>();
    }
    //アップデート一番最初に呼ばれるメソッド
    void PreAnimUpdate()
    {
        oldAnimNo = newAnimNo;//現在再生中のアニメーション番号を保存
    }

    //アニメーションを変更
    //アニメーターの再生命令を実行
    //変更したい番号（数字）から
    //配列の文字列を取得して設定
    void changeAnim(AnimNo changeNo)
    {
        animator.CrossFadeInFixedTime(NameTbl[(int)changeNo],1.0f);
    }
}