using DG.Tweening;
using UnityEngine;

public class PopAnim : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, 0.3f).SetEase(Ease.OutBack);
    }
}
