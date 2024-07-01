using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public enum FilamentEnum
{
    [FilamentInfo("GFL00", 190, 250, "PLA")]
    POLYLITE_PLA,

    [FilamentInfo("GFL01", 190, 250, "PLA")]
    POLYTERRA_PLA,

    [FilamentInfo("GFB00", 240, 270, "ABS")]
    BAMBU_ABS,

    [FilamentInfo("GFN03", 270, 300, "PA-CF")]
    BAMBU_PA_CF,

    [FilamentInfo("GFC00", 260, 280, "PC")]
    BAMBU_PC,

    [FilamentInfo("GFA00", 190, 250, "PLA")]
    BAMBU_PLA_Basic,

    [FilamentInfo("GFA01", 190, 250, "PLA")]
    BAMBU_PLA_Matte,

    [FilamentInfo("GFS01", 190, 250, "PA-S")]
    SUPPORT_G,

    [FilamentInfo("GFS00", 190, 250, "PLA-S")]
    SUPPORT_W,

    [FilamentInfo("GFU01", 200, 250, "TPU")]
    BAMBU_TPU_95A,

    [FilamentInfo("GFB99", 240, 270, "ABS")]
    ABS,

    [FilamentInfo("GFB98", 240, 270, "ASA")]
    ASA,

    [FilamentInfo("GFN99", 270, 300, "PA")]
    PA,

    [FilamentInfo("GFN98", 270, 300, "PA-CF")]
    PA_CF,

    [FilamentInfo("GFC99", 260, 280, "PC")]
    PC,

    [FilamentInfo("GFG99", 220, 260, "PETG")]
    PETG,

    [FilamentInfo("GFL99", 190, 250, "PLA")]
    PLA,

    [FilamentInfo("GFL98", 190, 250, "PLA-CF")]
    PLA_CF,

    [FilamentInfo("GFS99", 190, 250, "PVA")]
    PVA,

    [FilamentInfo("GFU99", 200, 250, "TPU")]
    TPU
}
