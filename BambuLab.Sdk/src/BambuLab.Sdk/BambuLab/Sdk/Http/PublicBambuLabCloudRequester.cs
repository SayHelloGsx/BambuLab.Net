using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Http;

public class PublicBambuLabCloudRequester : BambuLabCloudRequester
{
    protected override string Domain => "https://api.bambulab.com/";

    public PublicBambuLabCloudRequester() : base()
    {
    }
}
