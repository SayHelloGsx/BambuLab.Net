using AutoMapper;
using BambuLab.Sdk.Http;
using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Devices;
using Gsx.BambuLabPrinter.Public.Accounts;
using Gsx.BambuLabPrinter.Public.Devices;

namespace Gsx.BambuLabPrinter.Public;

public class BambuLabPrinterPublicApplicationAutoMapperProfile : Profile
{
    public BambuLabPrinterPublicApplicationAutoMapperProfile()
    {
        CreateMap<BambuLabAccount, BambuLabAccountDto>().MapExtraProperties();
        CreateMap<Device, DeviceDto>().MapExtraProperties();
        CreateMap<AccessTokenData, BambuLabAccessTokenCacheItem>();
        CreateMap<BambuLabAccessTokenCacheItem, AccessTokenData>();
    }
}
