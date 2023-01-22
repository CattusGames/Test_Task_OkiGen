using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class CollectHandler : MonoBehaviour
{
    public UnityEvent onIncrementScore;
    public UnityEvent onLevelPassed;
    public UnityEvent onWrongItem;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject[] _items;
    private int taskAmount;
    private int collectedAmount;
    private int objectIndex;
    private string taskItemName;

    void Start()
    {
        taskAmount = Random.Range(1, 6);
        objectIndex = Random.Range(0, _items.Length);
        taskItemName = _items[objectIndex].name;
        collectedAmount = 0;
        _scoreText.text = "Collect " + (taskAmount) + " "+taskItemName;
    }
    private void FixedUpdate()
    {
        if (collectedAmount >= taskAmount)
        {
            onLevelPassed.Invoke();
            _scoreText.text = "Level Passed!";
        }
    }
    public void CollectItem(GameObject item)
    {
        if (item.GetComponent<PooledObject>().name.Replace("(Clone)", "") == taskItemName)
        {
            collectedAmount++;
            _scoreText.text = "Collect " + (taskAmount - collectedAmount) + " " + taskItemName;
            onIncrementScore.Invoke();
        }
        else
        {
            WrongObjectAlert();
        }

    }

    public void WrongObjectAlert()
    {
        onWrongItem.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Fruits")
        {
            if (collision.gameObject.GetComponent<PooledObject>().hasEntered == false)
            {
                CollectItem(collision.gameObject);
                collision.gameObject.GetComponent<PooledObject>().hasEntered = true;
            }
            
        }

    }
}
