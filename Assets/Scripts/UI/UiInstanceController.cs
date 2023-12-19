using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInstanceController : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiPrefab;
    private Stack<GameObject> stack;
    private float offset;

    private int instanceMaxCount;

    private float _instanceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        offset = 40;
        instanceMaxCount = GameObject.Find("Player").GetComponent<PlayerHealth>().health;
        stack = new Stack<GameObject>();

        UpdateInstanceCount();
        CreateInstance(instanceMaxCount);

    }

    public void CreateInstance(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateInstance();
        }
    }
    public void CreateInstance()
    {
        if (_instanceCount < instanceMaxCount)
        {
            Vector2 newPosition = new Vector2
            (transform.position.x + (_instanceCount * offset)
            , transform.position.y);

            GameObject newInstance =
            Instantiate(_uiPrefab, transform.position, Quaternion.identity);

            newInstance.transform.SetParent(transform);
            newInstance.transform.position = newPosition;

            stack.Push(newInstance);
            UpdateInstanceCount();
        }
    }

    public void DestroyInstance()
    {
        if (stack.Count > 0)
        {
            GameObject instance = stack.Pop();
            Destroy(instance);
            UpdateInstanceCount();
        }
    }

    private void UpdateInstanceCount()
    {
        _instanceCount = stack.Count;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
