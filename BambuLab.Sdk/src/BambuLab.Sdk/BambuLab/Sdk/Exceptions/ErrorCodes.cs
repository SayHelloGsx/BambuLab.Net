using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Exceptions;
public static class ErrorCodes
{
    public static class MQTTClient
    {
        public const string CannotFindKeyFromData = "BambuLab:MQTTClient:0001";
        public const string ClientIsNotConnected = "BambuLab:MQTTClient:0002";
    }

    public static class Http
    {
        public const string HttpRequestFailed = "BambuLab:Http:0001";
        public const string LoginFailed = "BambuLab:Http:0002";
        public const string ApiRequestFailed = "BambuLab:Http:0003";
    }
}
