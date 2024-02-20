namespace BasicCallbackProvider;

using System;
using System.Threading;

public class CallbackProvider
{
    private Timer _timer;
    private readonly Action<Payload> _callback;
    private readonly int _interval;
    private readonly Func<Payload, bool> _condition;
    private Payload _payload;

    public CallbackProvider(Action<Payload> callback, int intervalMilliseconds, Func<Payload, bool> condition)
    {
        _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        _interval = intervalMilliseconds;
        _condition = condition ?? throw new ArgumentNullException(nameof(condition));
    }

    public void Start()
    {
        _timer = new Timer(CallbackMethod, null, 0, _interval);
    }

    public string Condition()
    {
        if (_payload is not null)
            return $"status: {_payload.Status} : {_payload.Timestamp}";

        return string.Empty;
    }

    private void CallbackMethod(object state)
    {
        // Simulating the creation of a Payload. This should be replaced with actual payload creation logic.
        var payload = new Payload(false); // Defaulting to false. This should be dynamic based on your application's logic.

        _payload = payload;

        if (_condition(payload))
        {
            payload.Status = true; // Assuming the condition is met, we set the status to true.
            _callback(payload);
            Stop();
        }
        else
        {
            payload.Status = false; // If the condition is not met, ensure the status reflects this.
            _callback(payload);
            // No stop here, as the condition is not met. Adjust as per your application's logic.
        }
    }

    public void Stop()
    {
        _timer?.Change(Timeout.Infinite, 0);
    }
}
