using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{

    public float _bpm;
    public AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    private int i;
    private float staticBPM;

    private void Start() {
        staticBPM = _bpm;
    }


    private void Update() {
        //! old version
        // foreach (Intervals interval in _intervals) {
        //     float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm)));
        //     interval.CheckForNewInterval(sampledTime);
        // }

        for(i = 0; i < _intervals.Length; i++) {
            if(i > 4) {
                float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * _intervals[i].GetIntervalLength(staticBPM)));
                _intervals[i].CheckForNewInterval(sampledTime);
            } else {
                float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * _intervals[i].GetIntervalLength(_bpm)));
                _intervals[i].CheckForNewInterval(sampledTime);
            }
            
        }


    }



}

[System.Serializable]
public class Intervals {
    [SerializeField] private float _beatCount;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;
    
    public float GetIntervalLength(float bpm) {
        return 60f / (bpm * _beatCount);
    }

    public void CheckForNewInterval (float interval) {
        if (Mathf.FloorToInt(interval) != _lastInterval) {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}


