using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI eventText; // UI for events/messages

    private int currentHour = 7;
    private bool isPM = false;
    private int budget = 1000;

    void Start()
    {
        UpdateUI();
    }

    public void TakeJeep()
    {
        int fare = 15;
        if (budget < fare)
        {
            eventText.text = "Insufficient Funds!";
            return;
        }

        currentHour += 1;
        budget -= fare;
        UpdateTimeFormat();
        TriggerRandomEvent("jeep");
        UpdateUI();
    }

    public void TakeTricycle()
    {
        int fare = 30;
        if (budget < fare)
        {
            eventText.text = "Insufficient Funds!";
            return;
        }

        currentHour += 1;
        budget -= fare;
        UpdateTimeFormat();
        TriggerRandomEvent("tricycle");
        UpdateUI();
    }

    public void Walk()
    {
        currentHour += 2;
        UpdateTimeFormat();
        eventText.text = ""; // No random events for walking
        UpdateUI();
    }

    public void TakeGrab()
    {
        int fare = 150;
        if (budget < fare)
        {
            eventText.text = "Insufficient Funds!";
            return;
        }

        int eventChance = Random.Range(0, 100);

        if (eventChance < 10) // 10% chance the Grab ride gets canceled
        {
            eventText.text = "Your Grab ride was canceled! Choose another option.";
        }
        else
        {
            currentHour += 1;
            budget -= fare;
            UpdateTimeFormat();
            TriggerRandomEvent("grab");
        }

        UpdateUI();
    }

    void TriggerRandomEvent(string transportMode)
    {
        int eventChance = Random.Range(0, 100);

        if (eventChance < 20) // 20% chance of a negative event
        {
            int negativeEvent = Random.Range(0, 2);
            if (negativeEvent == 0)
            {
                eventText.text = "Heavy traffic! +1 hour";
                currentHour += 1;
            }
            else if (negativeEvent == 1 && transportMode != "grab") // Fare hike applies to non-Grab rides
            {
                eventText.text = "Fare hike! +10 pesos";
                budget -= 10;
            }
        }
        else if (eventChance < 30) // 10% chance of a positive event
        {
            int positiveEvent = Random.Range(0, 2);
            if (positiveEvent == 0)
            {
                eventText.text = "You got a free ride! +0 cost";
                budget += 15; // Refund their last cost
            }
            else
            {
                eventText.text = "Traffic is light! -30 mins";
                currentHour -= 1;
            }
        }
        else
        {
            eventText.text = ""; // No event
        }

        UpdateTimeFormat();
    }

    void UpdateTimeFormat()
    {
        if (currentHour >= 12)
        {
            if (!isPM)
            {
                isPM = true; // Switch to PM
            }
            else if (currentHour > 12)
            {
                currentHour -= 12; // Convert to 12-hour format
            }
        }
    }

    void UpdateUI()
    {
        string amPm = isPM ? "PM" : "AM";
        timeText.text = $"Time: {currentHour}:00 {amPm}";
        budgetText.text = $"Budget: ₱{budget}";
    }
}
