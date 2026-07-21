using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CardData cardDatabase;

    void Start()
    {
        // 例: 0番目のカードの情報を取得する
        CardInfo firstCard = cardDatabase.cardList[0];
        Debug.Log($"カード名: {firstCard.cardName}, 属性: {firstCard.element}");

        // 例: ランダムに1枚カードを選んで取得する
        int randomIndex = Random.Range(0, cardDatabase.cardList.Count);
        CardInfo randomCard = cardDatabase.cardList[randomIndex];
        Debug.Log($"ランダムで選ばれたカード: {randomCard.cardName}");
    }
}
