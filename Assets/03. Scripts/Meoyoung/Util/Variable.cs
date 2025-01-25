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
        T[] components = target.GetComponentsInChildren<T>();
        foreach(var component in components)
        {
            if(component.name == variableName)
                return component;
        }
        return null;
    }
}

