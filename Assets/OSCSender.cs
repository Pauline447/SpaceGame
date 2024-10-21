using UnityEngine;
using OscJack;


public class OSCSender : MonoBehaviour
{
    [SerializeField] private int m_area;
    private string m_playerTag = "Player";
    // Define the OSC client
    private OscClient client;

    // Define the IP address and port of the OSC server (VCV Rack)
    public string oscServerIp = "127.0.0.1";
    public int oscServerPort = 8000;

    void Start()
    {
        // Initialize the OSC client
        client = new OscClient(oscServerIp, oscServerPort);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == m_playerTag)
        {
            Debug.Log("Trigger");
            if(m_area ==0)
            {
                SendOscMessage("/area1", 100f);
                SendOscMessage("/area2", 0f);
            }
            else if(m_area ==1)
            {
                SendOscMessage("/area1", 0f);
                SendOscMessage("/area2", 100f);
            }
            else if (m_area ==2)
            {
                SendOscMessage("/area1", 0f);
                SendOscMessage("/area2", 0f);
            }
        }
    }

    // Function to send an OSC message
    void SendOscMessage(string address, float value)
    {
        Debug.Log("send Message");

        client.Send(address, value);
        Debug.Log($"OSC Message Sent: {address} {value}");
    }
    public void SendOscMessageAfterEsc()
    {
        SendOscMessage("/area1", 0f);
        SendOscMessage("/area2", 0f);
    }
    void OnDestroy()
    {
        // Close the OSC client when the game object is destroyed
        client.Dispose();
    }
}
