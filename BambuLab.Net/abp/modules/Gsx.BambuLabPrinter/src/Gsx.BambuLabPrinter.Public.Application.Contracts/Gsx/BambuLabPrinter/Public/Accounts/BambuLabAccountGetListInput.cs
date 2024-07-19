using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class BambuLabAccountGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
