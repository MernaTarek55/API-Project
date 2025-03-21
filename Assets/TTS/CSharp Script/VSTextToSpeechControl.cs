using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class VSTextToSpeechControl : MonoBehaviour
{
    public TTSSpeaker Class_Speaker;

    public void SpeakMyText(string MyText)
    {
        Class_Speaker.Speak(MyText);
    }
}
