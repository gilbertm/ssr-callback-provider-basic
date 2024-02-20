namespace BasicCallbackProvider;

public delegate void CallbackDelegate(string response);

public class Payload
{
    public bool Status { get; set; }
    public DateTime Timestamp { get; set; }

    public Payload(bool status)
    {
        Status = status;
        Timestamp = DateTime.UtcNow;
    }
}

