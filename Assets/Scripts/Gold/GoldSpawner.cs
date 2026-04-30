using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] GoldUI _goldUIPrefab;

    [SerializeField] RectTransform _goldSpawnPoint;

    [SerializeField] RectTransform _goldEndPoint;

    /// <summary>
    /// 특정 위치에 골드 UI를 생성하는 함수
    /// </summary>
    /// <param name="pos">생성 위치</param>
    public void GoldSpawnerView(Vector3 pos)
    {
        // 골드 UI 프리팹을 인스턴스화하여 생성
        GoldUI goldUI = Instantiate(_goldUIPrefab, transform);

        // 생성 위치를 스크린 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);

        // 생성 위치 적용
        goldUI.transform.position = screenPos;

        // 골드 UI의 목표 위치 설정
        goldUI.SetEndPosition(_goldEndPoint);

        // 이동 시작
        goldUI.GoldMove();
    }
}
