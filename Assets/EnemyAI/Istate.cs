using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有状态都需要实现这个接口
public interface Istate
{
    //状态进入
    void OnEnter();
    //状态中
    void OnUpdate();
    //状态退出
    void OnExit();
}
