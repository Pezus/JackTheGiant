using UnityEngine;
using System.Collections;

public class CollectableScripts : MonoBehaviour {

    void OnEnable()
    {
        Invoke ("DestroyCollectable", 6f);

    }

	void DestroyCollectable()
    {
        gameObject.SetActive(false);
    }
}
