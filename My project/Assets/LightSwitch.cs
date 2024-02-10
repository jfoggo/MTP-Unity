using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private Camera mainCamera;
    private bool isWhite = false;
    private string statusText = "";
    private string renderingSpeed = "";

    void Start()
    {
        mainCamera = Camera.main;

        // Set the initial background color to black
        mainCamera.backgroundColor = Color.black;
    }

    void Update()
    {
        // Check for 'd' key press
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetBackgroundColor(isWhite ? Color.black : Color.white, "D");
            isWhite = !isWhite;
            // Display the game rendering speed in milliseconds
            renderingSpeed = "Game Rendering Speed: " + (Time.deltaTime * 1000.0f).ToString("F2") + " ms";
        }
    }

    void OnGUI()
    {
        // Set the style for the buttons
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 14;

        // Define the button width and height
        float buttonWidth = 80f;
        float buttonHeight = 30f;

        // Create "On" button
        buttonStyle.normal.textColor = Color.green;
        if (GUI.Button(new Rect(Screen.width - buttonWidth - 10, 10, buttonWidth, buttonHeight), "On", buttonStyle))
        {
            SetBackgroundColor(Color.white, "On");
            isWhite = true;
            // Display the game rendering speed in milliseconds
            renderingSpeed = "Game Rendering Speed: " + (Time.deltaTime * 1000.0f).ToString("F2") + " ms";
        }

        // Create "Off" button
        buttonStyle.normal.textColor = Color.red;
        if (GUI.Button(new Rect(Screen.width - buttonWidth - 10, 10 + buttonHeight + 10, buttonWidth, buttonHeight), "Off", buttonStyle))
        {
            SetBackgroundColor(Color.black, "Off");
            isWhite = false;
            // Display the game rendering speed in milliseconds
            renderingSpeed = "Game Rendering Speed: " + (Time.deltaTime * 1000.0f).ToString("F2") + " ms";
        }

        // Display status text
        GUIStyle statusStyle = new GUIStyle(GUI.skin.label);
        statusStyle.fontSize = 12;
        statusStyle.normal.textColor = Color.green;

        GUI.Label(new Rect(10, 10, 400, 20), statusText, statusStyle);

        // Display rendering speed text
        GUI.Label(new Rect(10, 30, 400, 30), renderingSpeed, statusStyle);

        // Draw a red rectangle in the top middle of the screen
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.background = MakeTex(2, 2, Color.red); // Red background color
        boxStyle.normal.textColor = Color.yellow;
        GUI.Box(new Rect(Screen.width / 2 - 100, 10, 150, 20), isWhite ? "30.0 lm / 100.0%" : "0.0 lm / 0.0%", boxStyle);
    }

    void SetBackgroundColor(Color color, string button)
    {
        // Set the background color
        mainCamera.backgroundColor = color;

        // Update the timestamp with milliseconds
        string timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");

        // Update the status text
        statusText = "UTC Time: " + timestamp + "Z - " + button + " pressed";

        // Debug message to indicate background color change with timestamp
        Debug.Log(statusText);
    }

    // Utility method to create a texture with a single color
    Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
}
