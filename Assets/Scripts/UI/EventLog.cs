using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : MonoBehaviour
{

    public EventLogItem[] eventLog;
    public GameObject eventLogTextItemHolder;
    public GameObject eventLogTextItem;
    public int lastNEventLogDisplayed = 3;


    void Awake()
    {
        eventLog = new EventLogItem[lastNEventLogDisplayed];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEvent(EventLogItem _eventLogItem)
    {
        for (int i = eventLog.Length - 1; i > 0; i--)
        {
            eventLog[i] = eventLog[i - 1];
        }
        eventLog[0] = _eventLogItem;

        UpdateEventLogUI();
    }
    public void UpdateEventLogUI()
    {
        //there are 3 fix TMP labels in the scene
        //we will update them with the last 3 events, 
        //if there are less than 3 events, we will display the ones we have
        //if there are more than 3 events, we will display the last 3 events
        //the last event will be displayed on the bottom
        //the first event will be displayed on the top
        for(int i = 0; i < eventLog.Length; i++)
        {
            if(eventLog[i] != null)
            {
                //eventLogTextItemHolder.transform.GetChild(i).GetComponent<TMPro.TextMeshProUGUI>().text = eventLog[i].eventText + " " + eventLog[i].eventTime;
                eventLogTextItemHolder.transform.GetChild(i).GetComponent<TMPro.TextMeshProUGUI>().text = eventLog[i].eventText;
            }
        }
    

    }



}
