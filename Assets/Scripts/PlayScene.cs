using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [Header("컴포넌트")]
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
