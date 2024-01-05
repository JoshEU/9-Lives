using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {
    public List<RawImage> lifeCatHealthImages = new List<RawImage>();
    public List<RawImage> deathCatHealthImages = new List<RawImage>();
    public static int nextLCHeartToGain;
    public static int nextDCHeartToGain;
}