using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������O�̃N���X�̕���
//partial class ���g���ƁA�������O�̃N���X���ʂ̃t�@�C��
//�Œ�`�����

partial class KyojinController
{
    //enum���g�����A�j���[�V�����ԍ��̒�`
    //stay=0,Run=1...�Ƃ������A�Ԃ��ݒ肳���
    enum AnimNo
    {
        giant_walk, germanSuplex,giant_attack, giant_drop, giant_throw
    };
    //AnimNo�ɑΉ�����������z����쐬
    //Unity�Őݒ肵���A�j���[�V������
    //���O�ƈ�v���Ă��Ȃ��Ƃ����Ȃ�
    string[] NameTbl =
    {
        "giant_walk","germanSuplex","giant_attack","giant_drop","giant_throw"
    };
    //�R���|�[�l���g�p
    Animator animator;
    //�ϐ�
    AnimNo oldAnimNo = 0;   //���ݍĐ����̃A�j���[�V�����ԍ�
    AnimNo newAnimNo = 0;   //�V�����ݒ肷��A�j���[�V�����ԍ�


    //���������\�b�h
    void InitAnim()
    {
        animator = GetComponent<Animator>();
    }
    //�A�b�v�f�[�g��ԍŏ��ɌĂ΂�郁�\�b�h
    void PreAnimUpdate()
    {
        oldAnimNo = newAnimNo;//���ݍĐ����̃A�j���[�V�����ԍ���ۑ�
    }

    //�A�j���[�V������ύX
    //�A�j���[�^�[�̍Đ����߂����s
    //�ύX�������ԍ��i�����j����
    //�z��̕�������擾���Đݒ�
    void changeAnim(AnimNo changeNo)
    {
        animator.CrossFadeInFixedTime(NameTbl[(int)changeNo],1.0f);
    }
}