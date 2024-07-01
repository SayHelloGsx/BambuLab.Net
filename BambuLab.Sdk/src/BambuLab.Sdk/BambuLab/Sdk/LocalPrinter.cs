using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public class LocalPrinter : Printer
{
    protected static string LocalUserName = "bblp";

    public LocalPrinter(string ipAddress, string accessCode, string serial)
        : base(ipAddress, accessCode, serial, LocalUserName)
    {
    }
}
