using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public class ChinaCloudPrinter : Printer
{
    protected static string Host = "cn.mqtt.bambulab.com";

    public ChinaCloudPrinter(string accessCode, string serial, string userName)
        : base(Host, accessCode, serial, userName)
    {
    }
}
