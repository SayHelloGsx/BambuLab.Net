using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public static class Commands
{
    public const string TurnLightOn = "{\"system\": {\"sequence_id\": \"0\", \"command\": \"ledctrl\", \"led_node\": \"chamber_light\", \"led_mode\": \"on\",\"led_on_time\": 500, \"led_off_time\": 500, \"loop_times\": 0, \"interval_time\": 0}}";
    public const string TurnLightOff = "{\"system\": {\"sequence_id\": \"0\", \"command\": \"ledctrl\", \"led_node\": \"chamber_light\", \"led_mode\": \"off\",\"led_on_time\": 500, \"led_off_time\": 500, \"loop_times\": 0, \"interval_time\": 0}}";
    public const string Pause = "{\"print\": {\"sequence_id\": \"0\", \"command\": \"pause\"}}";
    public const string Resume = "{\"print\": {\"sequence_id\": \"0\", \"command\": \"resume\"}}";
    public const string Stop = "{\"print\": {\"sequence_id\": \"0\", \"command\": \"stop\"}}";
    public const string LoadFilamentSpool = "{\"print\": {\"command\": \"ams_change_filament\",\"target\": 255,\"curr_temp\": 215,\"tar_temp\": 215}}";
    public const string UnloadFilamentSpool = "{\"print\": {\"command\": \"ams_change_filament\",\"target\": 254,\"curr_temp\": 215,\"tar_temp\": 215}}";
    public const string ResumeFilamentAction = "{\"print\":{\"command\":\"ams_control\",\"param\":\"resume\"}}";
    public const string Calibrate = "{{\"print\":{{\"command\":\"calibration\",\"param\":\"{0}\"}}}}";
    public const string SetPrintSpeedLvl = "{\"print\": {\"sequence_id\": \"0\", \"command\": \"print_speed\", \"param\": \"{0}\"}}";
}
