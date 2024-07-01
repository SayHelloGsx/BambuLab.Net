using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Http;

public class BambuLabResponseData
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string Error { get; set; }

    public BambuLabResponseData()
    {
    }

    public bool IsSuccess()
    {
        return Message == "success";
    }
}
