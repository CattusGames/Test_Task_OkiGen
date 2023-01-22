using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisappear : MonoBehaviour
{
    [SerializeField] private GameObject _disappearText;
    [SerializeField] private CollectHandler _collectHandler;
    private void Start()
    {
        _collectHandler.onIncrementScore.AddListener(Disappear);
    }
    private void Disappear()
    {
        Instantiate(_disappearText, gameObject.GetComponent<RectTransform>());
    }
}
