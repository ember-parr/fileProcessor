using System;
using System.Collections.Generic;

public class Metadata {
    // provider == practitioner in example
    // recepient == patient in example
    public string Practitioner { get; set; }
    public string Patient { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> Tags { get; set; } 
    public AudioFile File { get; set; }
}