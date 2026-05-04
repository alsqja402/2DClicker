using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] GoldUI _goldUIPrefab;

    [SerializeField] RectTransform _goldSpawnPoint;

    [SerializeField] RectTransform _goldEndPoint;

    [SerializeField] int _goldBurstCount = 5;

    /// <summary>
    /// 특정 위치에 골드 UI를 생성하는 함수
    /// </summary>
    /// <param name="pos">생성 위치</param>
    public void GoldSpawnerView(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);

        for (int i = 0; i < _goldBurstCount; i++)
        {
            GoldUI goldUI = Instantiate(_goldUIPrefab, transform);
            goldUI.transform.position = screenPos;
            goldUI.SetEndPosition(_goldEndPoint);
            goldUI.GoldScatterThenMove();
        }
    }
}
