using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class mudarcor : MonoBehaviour
{
    public Texture2D texture;
    void Start()
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        for (int cont = 0; cont < texture.width; cont++)
        {
            for (int cont2 = 0; cont2 < texture.height; cont2++)
            {
                Color cor = texture.GetPixel(cont, cont2);
                if (cor.a==0)
                {
                    newTexture.SetPixel(cont, cont2, Color.clear);
                }
                else
                {
                    newTexture.SetPixel(cont, cont2, Color.white);
                }
            }
        }
        newTexture.Apply();

        byte[] bytes = newTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/SavedScreen.png", bytes);
    }
}

