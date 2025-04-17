using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPointController : MonoBehaviour
{
     private RoadPointView _roadPointView;

    private void Start()
    {
        _roadPointView = GetComponent<RoadPointView>();
    }
    public void ActorTrigger()
    {
        _roadPointView.AnimateScale();
        
        _roadPointView.TryActivateItem();
    }
    
}
