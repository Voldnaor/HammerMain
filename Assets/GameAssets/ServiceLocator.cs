using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class ServiceLocator : MonoBehaviour
{
    private IDictionary<Type, Object> _serviceReferences;

    public static ServiceLocator Instance { get; private set; } = null;
    protected void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        } 
        else if(Instance == this)
        { 
            Destroy(gameObject); 
        }
        _serviceReferences = new Dictionary<Type, Object>();
        DontDestroyOnLoad(gameObject);
    }
    
    public T GetService<T>() where T : class 
    {
        if (!_serviceReferences.ContainsKey(typeof(T)))
        {
            throw new ArgumentException();
        }
        else
        {
            return _serviceReferences[typeof(T)] as T;
        }
    }

    public void RegisterService<T>(T value) where T : class
    {
        if (_serviceReferences.ContainsKey(typeof(T)))
        {
            throw new ArgumentException();
        }
        else
        {
            _serviceReferences.Add(typeof(T),value);
        }
    }

    public void DeregisterService<T>()
    {
        if (!_serviceReferences.ContainsKey(typeof(T)))
        {
            throw new ArgumentException();
        }
        else
        {
            _serviceReferences.Remove(typeof(T));
        }
    }
}