using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;

    public int gridRows = 2;
    public int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    public bool canReveal
    {
        get {return _secondRevealed == null;}
    }
    
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int numPairs = (gridRows * gridCols) / 2;
        int[] numbers = new int[gridRows * gridCols];

        int num = 0;
        for (int i = 0; i < numPairs; i++)
        {
            numbers[num++] = i;
            numbers[num++] = i;
        }

        numbers = SuffleArray(numbers);
        for (int j = 0; j < numbers.Length; j++)
            Debug.Log(numbers[j] + " ");

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i ==0 && j == 0)
                    card = originalCard;
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
            _firstRevealed = card;
        else
        {
            _secondRevealed  = card;
            StartCoroutine(CheckMatch());
        }
    }

    private int[] SuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int r = Random.Range(0, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            Debug.Log("Score: " + _score);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }
}
