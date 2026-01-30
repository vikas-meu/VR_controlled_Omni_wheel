# Omni-Directional Robot Control via Meta Quest Joystick (OpenXR)

A wireless holonomic (omni-wheel) robot controlled in real-time using the **Meta Quest** thumbstick over **WiFi + WebSocket**.

### Features
- Real-time control using **Meta Quest left/right thumbstick**
- Smooth velocity scaling (-100 to +100)
- WebSocket-based low-latency communication
- ESP32 in **Access Point (AP) mode** (no router needed)
- Ramping, speed scaling, and per-wheel calibration on robot side
- OpenXR compatible (works with Quest 2/3/Pro)

---

## Hardware Required
- Meta Quest 2 / Quest 3 / Quest Pro
- ESP32 development board (ESP32-WROOM, ESP32-S3, etc.)
- 3× DC motors with driver (L298N / DRV8833 / TB6612)
- Omni-directional wheels (3-wheel configuration)
- Power bank / battery for ESP32 + motors

---

## Software Requirements

### Unity (2021.3 LTS or newer recommended)
- Unity Editor with **Android Build Support**
- **OpenXR** enabled

### ESP32
- Arduino IDE 2.x
- ESP32 board support (by Espressif)

---

## Required Libraries & Installation

### 1. Unity Packages

| Package                        | How to Install                                      | Version Recommended |
|-------------------------------|-----------------------------------------------------|---------------------|
| **OpenXR Plugin**             | Window → Package Manager → Search "OpenXR Plugin"   | Latest             |
| **XR Plugin Management**      | Window → Package Manager → Search "XR Plugin Management" | Latest       |
| **WebSocketSharp**            | Via **NuGetForUnity** (recommended)                 | 1.5.0+             |

**Install NuGetForUnity** (for WebSocketSharp):
1. Download latest `.unitypackage` from:  
   https://github.com/GlitchEnzo/NuGetForUnity/releases
2. Assets → Import Package → Custom Package
3. After import → **NuGet → Manage NuGet Packages** → Search **WebSocketSharp** → Install

### 2. ESP32 Arduino Libraries

| Library                        | Installation Method                                 |
|-------------------------------|-----------------------------------------------------|
| **WebSockets** (arduinoWebSockets) | Library Manager → Search **"WebSockets"** by **Links2004** |

**Install via Library Manager**:
- Arduino IDE → **Sketch → Include Library → Manage Libraries**
- Search: **`WebSockets`**
- Install: **WebSockets by Markus Sattler (Links2004)**

---

## Project Files

- `omni_controller.cs` → Unity script (attach to any GameObject)
- `omni_robot_esp32.ino` → ESP32 Arduino sketch

---

## Setup Instructions

### Step 1: ESP32 Setup
1. Open `omni_robot_esp32.ino`
2. Select your ESP32 board + port
3. Upload the code
4. Open **Serial Monitor** (115200 baud)
5. Note the **AP IP address** (default: `192.168.4.1`)

**WiFi AP Credentials** (in code):
- SSID: `RobotControl`
- Password: `12345678`

### Step 2: Unity Setup
1. Open your Quest-compatible Unity project
2. Import required packages (OpenXR + WebSocketSharp)
3. Create new script → rename to `omni_controller.cs`
4. Paste the Unity code
5. Attach script to any GameObject
6. Set **esp32IP** to your ESP32's IP (usually `192.168.4.1`)

### Step 3: Run
1. Connect your **Meta Quest** to PC (Link/Air Link) or build to headset
2. Connect Quest to ESP32 WiFi:
   - Settings → WiFi → Join `RobotControl` → password `12345678`
3. Play scene (Editor) or run on Quest
4. Use **left thumbstick** to drive the robot

---

## Controls

| Input                     | Action                  |
|--------------------------|-------------------------|
| Left Thumbstick          | Forward / Backward / Strafe |
| Right Thumbstick (future)| Rotation (vr)           |
| Release stick            | Robot stops automatically |

---

## Configuration Options (Unity)

```csharp
public string esp32IP = "192.168.4.1";   // ← Change if your ESP32 uses different IP
public int webSocketPort = 81;
```

**ESP32 tuning**:
- `MAX_SPEED_SCALE = 0.7f;` → overall speed limit
- `RAMP_RATE = 0.1f;` → acceleration smoothness
- `Kb1/Kb2/Kb3` → deadband compensation

---

## Troubleshooting

| Problem                                 | Solution |
|-----------------------------------------|--------|
| `WebSocket` ambiguous reference         | Use `WebSocketSharp.WebSocket` |
| OVRInput not found                      | Use OpenXR version (this one) |
| Can't connect to ESP32                  | Check Quest is connected to `RobotControl` WiFi |
| Robot doesn't move                      | Check Serial Monitor for WebSocket connection |
| High latency                            | Reduce send threshold from `1f` → `3f` |

---

## Future Improvements
- Right thumbstick → rotation (`vr`)
- Haptic feedback on robot stop
- Battery voltage monitoring
- Web dashboard (already included)

---

**I AM VIKAS... I LOVE ROBOTICS❤️**

---
