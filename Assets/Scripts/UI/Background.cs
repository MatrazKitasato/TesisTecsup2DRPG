using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    Image image;
    Vector2 offset;
    Material material;
    void Awake()
    {
        material = GetComponent<Material>();
        image = GetComponent<Image>();
        image.material.mainTextureOffset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        offset = speed * Time.deltaTime;
        image.material.mainTextureOffset += offset;
    }
}
