using UnityEngine;
using DG.Tweening;

public class StarPop : MonoBehaviour
{
    public float delay = 0f;          // Set delay per star
    public float popScale = 1.3f;     // Scale during pop
    public float popDuration = 0.3f;  // Animation speed

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        PlayPop();
    }

    void PlayPop()
    {
        rect.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(delay);
        seq.Append(
            rect.DOScale(popScale, popDuration)
                .SetEase(Ease.OutBack)
        );
        seq.Append(
            rect.DOScale(1f, 0.15f)
        );
    }
}
