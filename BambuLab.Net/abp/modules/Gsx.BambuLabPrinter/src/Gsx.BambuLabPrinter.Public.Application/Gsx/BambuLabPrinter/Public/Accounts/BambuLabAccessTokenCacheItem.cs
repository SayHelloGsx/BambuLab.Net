using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Gsx.BambuLabPrinter.Public.Accounts;

[CacheName("BambuLabAccessTokens")]
public class BambuLabAccessTokenCacheItem
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public int RefreshExpiresIn { get; set; }
    public string TfaKey { get; set; }
    public string AccessMethod { get; set; }
}
