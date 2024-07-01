using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Exceptions;

public class BambuLabHttpException : BambuLabSdkException
{
    public BambuLabHttpException(string errorCode)
    : base(errorCode)
    {
    }

    public BambuLabHttpException(string errorCode, string message)
        : base(errorCode, message)
    {
    }
}
