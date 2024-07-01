﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public enum PrintStatusEnum
{
    PRINTING = 0,
    AUTO_BED_LEVELING = 1,
    HEATBED_PREHEATING = 2,
    SWEEPING_XY_MECH_MODE = 3,
    CHANGING_FILAMENT = 4,
    M400_PAUSE = 5,
    PAUSED_FILAMENT_RUNOUT = 6,
    HEATING_HOTEND = 7,
    CALIBRATING_EXTRUSION = 8,
    SCANNING_BED_SURFACE = 9,
    INSPECTING_FIRST_LAYER = 10,
    IDENTIFYING_BUILD_PLATE_TYPE = 11,
    CALIBRATING_MICRO_LIDAR = 12,
    HOMING_TOOLHEAD = 13,
    CLEANING_NOZZLE_TIP = 14,
    CHECKING_EXTRUDER_TEMPERATURE = 15,
    PAUSED_USER = 16,
    PAUSED_FRONT_COVER_FALLING = 17,
    CALIBRATING_LIDAR = 18,
    CALIBRATING_EXTRUSION_FLOW = 19,
    PAUSED_NOZZLE_TEMPERATURE_MALFUNCTION = 20,
    PAUSED_HEAT_BED_TEMPERATURE_MALFUNCTION = 21,
    FILAMENT_UNLOADING = 22,
    PAUSED_SKIPPED_STEP = 23,
    FILAMENT_LOADING = 24,
    CALIBRATING_MOTOR_NOISE = 25,
    PAUSED_AMS_LOST = 26,
    PAUSED_LOW_FAN_SPEED_HEAT_BREAK = 27,
    PAUSED_CHAMBER_TEMPERATURE_CONTROL_ERROR = 28,
    COOLING_CHAMBER = 29,
    PAUSED_USER_GCODE = 30,
    MOTOR_NOISE_SHOWOFF = 31,
    PAUSED_NOZZLE_FILAMENT_COVERED_DETECTED = 32,
    PAUSED_CUTTER_ERROR = 33,
    PAUSED_FIRST_LAYER_ERROR = 34,
    PAUSED_NOZZLE_CLOG = 35,
    UNKNOWN = -1,
    IDLE = 255
}