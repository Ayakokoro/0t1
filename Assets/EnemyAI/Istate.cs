using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����״̬����Ҫʵ������ӿ�
public interface Istate
{
    //״̬����
    void OnEnter();
    //״̬��
    void OnUpdate();
    //״̬�˳�
    void OnExit();
}
