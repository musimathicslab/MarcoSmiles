using System;
using System.Linq;
using UnityEngine;

public class SaveListButtonScript : MonoBehaviour
{

    public static event Action<string, int> OnCreateList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        OnCreateList?.Invoke(KeysScript.SelectedKeys.ElementAt(0).name, KeysScript.SelectedKeys.Count);
    }

}
