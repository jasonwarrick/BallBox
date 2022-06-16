using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // Fundamental dialgoue scripts taken from Brackeys (Obviously): https://www.youtube.com/watch?v=_nRzoTzeyxU
[System.Serializable] // The class is now accessable as a dataype by all scripts in the project
public class Dialogue {
    public string name;

    [TextArea(3, 10)] // The sentence string boxes now have a minimum line count of 3, and a maximum of 10
    public string[] sentences;
}
