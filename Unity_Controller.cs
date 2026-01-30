using UnityEngine;
using UnityEngine.XR;
using WebSocketSharp; // Import after installing WebSocketSharp

public class SendJoystickDataToOmniRobot : MonoBehaviour
{
    public string esp32IP = "192.168.4.1"; // ESP32 AP IP (default for softAP)
    public int webSocketPort = 81;
    private WebSocketSharp.WebSocket ws;
    private float lastVx, lastVy;
    private const int vr = 0; // No rotation; set to variable if you add rotation input

    void Start()
    {
        string wsUrl = $"ws://{esp32IP}:{webSocketPort}/";
        ws = new WebSocketSharp.WebSocket(wsUrl);
        ws.OnOpen += (sender, e) => Debug.Log("Connected to ESP32 WebSocket");
        ws.OnError += (sender, e) => Debug.LogError("WebSocket Error: " + e.Message);
        ws.OnClose += (sender, e) => Debug.Log("WebSocket Closed");
        ws.Connect();
    }

    void Update()
    {
        if (ws == null || ws.ReadyState != WebSocketState.Open) return;

        // Get joystick input from OpenXR controller (primary thumbstick on active/left hand)
        InputDevice controller = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // Or XRNode.RightHand if preferred
        Vector2 joystick;
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick))
        {
            float vx = joystick.x * 100f; // Scale -1 to 1 -> -100 to 100
            float vy = joystick.y * 100f; // Y forward positive

            // Send only if change is significant (threshold to reduce spam)
            if (Mathf.Abs(vx - lastVx) > 1f || Mathf.Abs(vy - lastVy) > 1f)
            {
                string msg = $"V {Mathf.RoundToInt(vx)} {Mathf.RoundToInt(vy)} {vr}";
                ws.Send(msg);
                lastVx = vx;
                lastVy = vy;
            }
        }
    }

    void OnApplicationQuit()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }
}
