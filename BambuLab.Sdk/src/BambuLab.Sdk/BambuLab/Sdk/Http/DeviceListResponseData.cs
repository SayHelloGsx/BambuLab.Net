using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk.Http;

public class DeviceListResponseData : BambuLabResponseData
{
    public List<Dictionary<string, object>> Devices { get; set; }

    public DeviceListResponseData() : base()
    {
    }

    public virtual int GetIndexByDevId(string devId)
    {
        return Devices.IndexOf(Devices.Find(x => x["dev_id"].ToString() == devId));
    }

    public virtual string GetDevId(int index)
    {
        return Devices[index]["dev_id"].ToString();
    }

    public virtual string GetName(int index)
    {
        return Devices[index]["name"].ToString();
    }

    public virtual bool Online(int index)
    {
        return Convert.ToBoolean(Devices[index]["online"]);
    }

    public virtual string GetPrintStatus(int index)
    {
        return Devices[index]["print_status"].ToString();
    }

    public virtual string GetDevModelName(int index)
    {
        return Devices[index]["dev_model_name"].ToString();
    }

    public virtual string GetDevProductName(int index)
    {
        return Devices[index]["dev_product_name"].ToString();
    }

    public virtual string GetDevAccessCode(int index)
    {
        return Devices[index]["dev_access_code"].ToString();
    }

    public virtual float GetNozzleDiameter(int index)
    {
        return Convert.ToSingle(Devices[index]["nozzle_diameter"]);
    }
}
