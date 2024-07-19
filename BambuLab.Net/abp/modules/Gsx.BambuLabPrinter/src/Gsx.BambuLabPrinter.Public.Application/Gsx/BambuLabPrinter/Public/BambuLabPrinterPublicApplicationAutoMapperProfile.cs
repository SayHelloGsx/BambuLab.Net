using AutoMapper;
using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Public.Accounts;

namespace Gsx.BambuLabPrinter.Public;

public class BambuLabPrinterPublicApplicationAutoMapperProfile : Profile
{
    public BambuLabPrinterPublicApplicationAutoMapperProfile()
    {
        CreateMap<BambuLabAccount, BambuLabAccountDto>().MapExtraProperties();
    }
}
