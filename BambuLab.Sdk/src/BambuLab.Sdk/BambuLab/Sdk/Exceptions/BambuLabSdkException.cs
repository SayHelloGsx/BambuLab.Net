using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Exceptions;

public class BambuLabSdkException : Exception
{
    protected virtual Dictionary<string, string> CustomData { get; } = new Dictionary<string, string>();

    public string ErrorCode { get; }

    public BambuLabSdkException(string errorCode)
    {
        ErrorCode = errorCode;
    }

    public BambuLabSdkException(string errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public BambuLabSdkException(string errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }


    public virtual BambuLabSdkException WithData(string key, string value)
    {
        CustomData.Add(key, value);
        return this;
    }
}
