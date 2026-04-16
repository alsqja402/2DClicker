using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [Header("ÄÄÆ÷³ÍÆ®")]
    [SerializeField] Session _session;

    private void Start()
    {
        _session.Play();
    }

    public void Tap()
    {
        _session.TapAttack();
    }
}
