using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    Image image;
    Vector2 offset;
    Material material;
    public bool isImage;
    void Awake()
    {
        
        if (isImage)
        {
            material = GetComponent<Image>().material;
            material.mainTextureOffset = Vector2.zero;
        }
        else
        {
            material = GetComponent<SpriteRenderer>().material;
            material.mainTextureOffset = Vector2.zero;
        }
        
    }
    void Update()
    {
        offset = speed * Time.deltaTime;
        if(isImage)
        {
            material.mainTextureOffset += offset;
        }
        else
        {
            material.mainTextureOffset += offset;
        }
        
    }
}
