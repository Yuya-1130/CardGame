using UnityEngine;
using System.Collections.Generic;
public enum Element { Fire,Water,Wind,Rock,Rigth,Brack}

[System.Serializable]
public class CardInfo
{
    public string cardName = "新しいカード";
    public Element element = Element.Fire;
    public int attackPower = 10;
    public Sprite cardImage;

}

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card Data")]
public class CardData : ScriptableObject
{
    public List<CardInfo> cardList = new List<CardInfo>();

}
