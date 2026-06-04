using UnityEngine;

public class BackGroundCtlr : MonoBehaviour
{
    [SerializeField] GameObject[] _backGroundImages;
    [SerializeField] string[] _backGroundNames;
    [SerializeField] TransitionImageData[] _transitionImageDatas;

    public void ChangeBackGround(int stage)
    {
        int index = GetBackGroundIndex(stage);

        for (int i = 0; i < _backGroundImages.Length; i++)
        {
            _backGroundImages[i].SetActive(i == index);
        }
    }

    public string GetBackGroundName(int stage)
    {
        int index = GetBackGroundIndex(stage);

        if (index >= _backGroundNames.Length)
            return "";

        return _backGroundNames[index];
    }

    public TransitionImageData GetTransitionImageData(int stage)
    {
        int index = GetBackGroundIndex(stage);

        if (index >= _transitionImageDatas.Length)
            return null;

        return _transitionImageDatas[index];
    }

    int GetBackGroundIndex(int stage)
    {
        int index = (stage - 1) / 5;

        if (index >= _backGroundImages.Length)
            index = _backGroundImages.Length - 1;

        return index;
    }

    public bool HasNextTransition(int stage)
    {
        int index = (stage - 1) / 5;

        return index < _transitionImageDatas.Length;
    }

    [System.Serializable]
    public class TransitionImageData
    {
        public Sprite image;
        public Vector2 position;
        public Vector2 size;
    }
}