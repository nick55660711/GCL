using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class CardSystem : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    private string[] type = { "Spades", "Diamond", "Heart", "Club" };


    private void GetAllCard()
    {
        if (cards.Count == 52) return;

        for (int i = 0; i < type.Length; i++)
        {
            for (int j= 1 ; j < 14; j++)
            {
                string number = j.ToString();


                switch (j)
                {
                    case 1:
                        number = "A";
                        break;
                         case 11:
                        number = "J";
                        break;
                         case 12:
                        number = "Q";
                        break;
                         case 13:
                        number = "K";
                        break;


                }


                GameObject card = Resources.Load<GameObject>("PlayingCards_" + number + type[i]);

                cards.Add(card);


            }
        }

    }


    IEnumerator SetChildPosition()
    {
       yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < transform.childCount; i++)
        {
            int x = i % 13;
            int y = i / 13;
            Transform child = transform.GetChild(i);
            child.eulerAngles = new Vector3(180, 0, 0);
            child.localScale = Vector3.one * 20;
            child.position = new Vector3((x - 6) * 1.3f ,4- y*2, 0);
            yield return null;
        }

    }


    void DeleteAllChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }

    public void ChooseCardByType(string type) 
    {
        DeleteAllChild();

       var temp = cards.Where((x) => x.name.Contains(type));

        foreach(var item in temp)
        {
            Instantiate(item, transform);
        }

        StartCoroutine(SetChildPosition());
    }

    public void sort()
    {
        DeleteAllChild();

        var sort = from card in cards
                   where card.name.Contains(type[3]) || card.name.Contains(type[2]) || card.name.Contains(type[1]) || card.name.Contains(type[0])
                   select card;

        foreach (var item in sort)
        {
            print(item.name);
            Instantiate(item, transform);
        }

        StartCoroutine(SetChildPosition());
    }

     public void shuffle()
    {
        DeleteAllChild();
        // 集合是參考型別
        // 陣列是實質型別

        List<GameObject> shuffle = cards.ToArray().ToList(); //轉成陣列(實質型別)後再轉回清單
        List<GameObject> newCards = new List<GameObject>(); //用來儲存洗牌後的新牌組

        for (int i = 0; i < cards.Count; i++)
        {
        int r = Random.Range(0, shuffle.Count);
        GameObject temp = shuffle[r];
        newCards.Add(temp);
        shuffle.RemoveAt(r);
        }

        foreach (var item in newCards)
        {
            Instantiate(item, transform);
        }

        StartCoroutine(SetChildPosition());

    }








    private void Start()
    {

        GetAllCard();

    }









}
