using System;
using System.Collections.Generic;

public class Metadata {
    // provider == practitioner in example
    // recepient == patient in example
    public string Provider { get; set; }
    public string Recepient { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> Tags { get; set; } 
}