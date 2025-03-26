using UnityEngine;

public class CoroutineHost : MonoBehaviour
{


    public static int a = 8;
    
    //Variable
    public static CoroutineHost m_Instance;

    //Properties
    public static CoroutineHost Instance
    {
        get
        {
            if (m_Instance == null)
            {
                //Find CoroutineHost component (script) in the scene
                m_Instance = (CoroutineHost)FindFirstObjectByType<CoroutineHost>();
                

                //If still equal null
                if (m_Instance == null)
                {
                    //Create a GameObject and name it CoroutimeHost
                    GameObject go = new GameObject();
                    go.name = "CoroutineHost";
                    
                    //Add a component (script) on this GameObject and let m_instance refrences to this component
                    m_Instance = go.AddComponent<CoroutineHost>();
                }


                
                DontDestroyOnLoad(m_Instance);
            }

            //return m_Instance refrence variable to Instance properties
            return m_Instance;


        }
    }




}
