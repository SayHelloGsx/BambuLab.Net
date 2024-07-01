using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public enum GcodeStateEnum
{
    IDLE,
    PREPARE,
    RUNNING,
    PAUSE,
    FINISH,
    FAILED,
    UNKNOWN
}
