using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup; // ドロップ判定を一時的に無効化するために使う
    private Transform originalParent;
    private HandSort handSort;
   [SerializeField] private Transform canvasTransform;

    [HideInInspector] public bool isDropped = false;

    private void Awake()
    {
        // CanvasGroupを安全に取得（無ければ自動追加）
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        handSort = GetComponentInParent<HandSort>();

        Debug.Log("Drag Started");

        originalParent = transform.parent; // 現在の親を記憶

        // ドラッグ中は他のUI（ドロップ先）がこの物体を貫通して検知できるように、
        // 自分のRaycast（当たり判定）を一時的にオフにする
        canvasGroup.blocksRaycasts = false;

       

        if (canvasTransform != null)
        {
            transform.SetParent(canvasTransform);
        }
        else
        {
            // もし Inspector で設定し忘れている場合の保険
            transform.SetParent(transform.root);
        }

        if (canvasGroup != null)
        {
            // マウスカーソルが「下のドロップエリア」に届くようにする！
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("【テスト】FieldAreaにドロップされました！");

        // 親のRectTransformを安全に取得（nullならtransform.rootを使う）
        RectTransform parentRect = transform.parent as RectTransform;
        if (parentRect == null)
        {
            parentRect = transform.root as RectTransform;
        }

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        transform.localPosition = localPoint;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            //指を離したら当たり判定を元に戻す
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f;          // 透明度を元に戻す
        }
        Debug.Log("Drag Ended");




        // ドロップ先のスクリプト（DropHandler）が、このオブジェクトを自分の親にしなかった場合、
        // つまりどこにもドロップされなかった場合は、元の親（場所）に戻す
        if (!isDropped)
        {
            //if (originalParent != null)
            //{
            //    transform.SetParent(originalParent);
            //}

           

            if (handSort != null)
            {
                handSort.ArrangeCards();
            }
        }
        else
        {
            // ドロップ成功時：残った手札だけ綺麗に詰め直す
            if (handSort != null)
            {
                handSort.ArrangeCards();
            }
        }

    }
}
