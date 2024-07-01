using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public abstract class PublicCloudPrinter : Printer
{
    protected static string Host = "us.mqtt.bambulab.com";

    public PublicCloudPrinter(string accessCode, string serial, string userName)
        : base(Host, accessCode, serial, userName)
    {
    }
}
