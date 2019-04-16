using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetAllHierarchy : MonoBehaviour
{
    private GameObject[] AllGo;
    const string clientList = "unity-scene.txt";

    [MenuItem("Debug/GetAll")]
    public static void GenerateLocReport()
    {

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        string filename = Path.Combine(Application.dataPath, clientList);

        using (StreamWriter writer = new StreamWriter(filename, false))
        {
            for (int i = 0; i < rootObjects.Count; ++i)
            {
                GameObject gameObject = rootObjects[i];

                Debug.Log(gameObject.name);

                DumpGameObject(gameObject, writer, "/", "");

            }
        }
        AssetDatabase.Refresh();
    }


    private static void DumpGameObject(GameObject gameObject, StreamWriter writer, string indent, string parentName)
    {
        foreach (Component component in gameObject.GetComponents<Component>())
        {
            if (component == null)
            {
                Debug.Log("NULL");
            }
            else
            {
                if ((component.GetType().Name == "Text")||(component.GetType().Name == "Button"))
                {
                    if ((component.GetType().Name == "Text"))
                    {
                        if ((component.GetType().Name == "Text"))
                        {
                            Text text = gameObject.GetComponent<Text>();
                            writer.WriteLine(text.text + " = " + parentName + gameObject.name);
                        }                      
                    }  
                    else if((component.GetType().Name == "Button"))
                    {
                        writer.WriteLine(gameObject.name + " = " + parentName + gameObject.name);
                    }
                    else
                    {
                        writer.WriteLine(parentName + gameObject.name);
                    }
                }
                else
                {
                    writer.WriteLine(parentName + gameObject.name);
                }
            }
        }
        if (gameObject.transform.childCount > 0)
        {

            if (parentName != "")
            {
                if (!parentName.EndsWith("/"))
                    parentName = parentName + "/";
            }


            parentName = parentName + gameObject.name;
        }
        else
        {
            parentName = parentName + gameObject.name;
           
        }

        foreach (Transform child in gameObject.transform)
        {
            if (!parentName.EndsWith("/"))
                parentName = parentName + "/";
            DumpGameObject(child.gameObject, writer, indent, parentName);
        }
    }
}
