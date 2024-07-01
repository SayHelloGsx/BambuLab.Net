using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Exceptions;

public class BambuLabMQTTException : BambuLabSdkException
{
    public BambuLabMQTTException(string errorCode)
        : base(errorCode)
    {
    }

    public BambuLabMQTTException(string errorCode, string message)
        : base(errorCode, message)
    {
    }

}
