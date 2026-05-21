using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RebirthSessionData")]
public class RebirthSessionData : ScriptableObject
{
    [SerializeField] float _baseRebirthPoint;

    public float GetRebirthPointByStage(int stage)
    {
        return _baseRebirthPoint * Mathf.Floor(stage / 5f);
    }
}