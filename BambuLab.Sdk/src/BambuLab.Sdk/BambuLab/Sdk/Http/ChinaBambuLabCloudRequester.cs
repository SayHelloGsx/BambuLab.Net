using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Http;

public class ChinaBambuLabCloudRequester : BambuLabCloudRequester
{
    protected override string Domain => "https://api.bambulab.cn/";

    public ChinaBambuLabCloudRequester(string account, string password) : base(account, password)
    {
    }
}
