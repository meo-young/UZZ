using UnityEngine;

public class Variable : MonoBehaviour
{
    public static Variable instance;

    private void Awake() {
        if(instance == null)
            instance = this;
    }

    public T Init<T>(Transform target, string variableName, T variable) where T : Component
    {
        return target.Find(variableName).GetComponent<T>();
    }
}
