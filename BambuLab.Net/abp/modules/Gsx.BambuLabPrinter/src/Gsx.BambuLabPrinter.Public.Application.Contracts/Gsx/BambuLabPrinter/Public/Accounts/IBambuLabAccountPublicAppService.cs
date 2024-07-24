using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public interface IBambuLabAccountPublicAppService : IApplicationService
{
    Task<PagedResultDto<BambuLabAccountDto>> GetListAsync(BambuLabAccountGetListInput input);
    Task<BambuLabAccountDto> GetAsync(Guid id);
    Task<BambuLabAccountDto> CreateAsync(CreateBambuLabAccountDto input);
    Task<BambuLabAccountDto> UpdateAsync(Guid id, UpdateBambuLabAccountDto input);
    Task<BambuLabAccountDto> SyncUserNameAsync(Guid id);
    Task DeleteAsync(Guid id);
}
