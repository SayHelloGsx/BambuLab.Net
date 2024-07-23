namespace Gsx.BambuLabPrinter;

public static class BambuLabPrinterErrorCodes
{
    private const string ErrorCodePrefix = "BambuLabPrinter";

    public static class Accounts
    {
        private const string AccountsPrefix = $"{ErrorCodePrefix}:Account";
        public const string AccountAlreadyExist = $"{AccountsPrefix}:0001";
        public const string CloudTypeNotSupport = $"{AccountsPrefix}:0002";
        public const string LoginFailed = $"{AccountsPrefix}:0003";
        public const string NotOwner = $"{AccountsPrefix}:0004";
    }
}
