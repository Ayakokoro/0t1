using UnityEngine;
/// <summary>
/// �̳�MonoBehaviour�� ����ģʽ ���� �Լ�ȥ��֤����Ψһ��
/// </summary>
public class SingleMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    //�̳���Mono�Ľű� ����ֱ��new
    //ֻ��ͨ���϶����������� ���� ͨ�� ���ؽű���api AddComponent ȥ�ӽű� unity �ڲ�������ȥʵ������
    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }

    protected  virtual void Update()
    {

    }
}

