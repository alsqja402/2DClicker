using UnityEngine;

public class BackGroundCtlr : MonoBehaviour
{
    [SerializeField] GameObject[] _backGroundImages;

    public void ChangeBackGround(int stage)
    {
        int index = stage / 5;

        if (index >= _backGroundImages.Length)
        {
            index = _backGroundImages.Length - 1;
        }

        for (int i = 0; i < _backGroundImages.Length; i++)
        {
            _backGroundImages[i].SetActive(i == index);
        }
    }
}