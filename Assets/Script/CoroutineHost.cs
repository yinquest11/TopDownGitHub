using UnityEngine;

public class CoroutineHost : MonoBehaviour
{


   public static CoroutineHost Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (CoroutineHost)FindFirstObjectByType<CoroutineHost>();

                if(m_Instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CoroutineHost";
                    m_Instance = go.AddComponent<CoroutineHost>();
                }

                DontDestroyOnLoad(m_Instance);
            }

            return m_Instance;
        }
    }

    public static CoroutineHost m_Instance;
}
