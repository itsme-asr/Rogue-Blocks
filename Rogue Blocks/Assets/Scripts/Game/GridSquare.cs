using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image normalImg;
    public List<Sprite> normalImgs;
    void Start()
    {

    }

    public void setImg(bool setFirstImg)
    {
        normalImg.GetComponent<Image>().sprite = setFirstImg ? normalImgs[1] : normalImgs[0];
    }
}
