using System.Collections;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Session _session;

    private void Start()
    {
        StartCoroutine(_session.PlayWithIntroTransition());
    }

    public void Tap()
    {
        _session.TapAttack();
    }
}