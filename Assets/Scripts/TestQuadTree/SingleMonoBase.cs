using UnityEngine;
/// <summary>
/// 继承MonoBehaviour的 单例模式 对象 自己去保证他的唯一性
/// </summary>
public class SingleMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    //继承来Mono的脚本 不能直接new
    //只能通过拖动到对象身上 或者 通过 加载脚本的api AddComponent 去加脚本 unity 内部帮我们去实例化它
    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }

    protected  virtual void Update()
    {

    }
}

