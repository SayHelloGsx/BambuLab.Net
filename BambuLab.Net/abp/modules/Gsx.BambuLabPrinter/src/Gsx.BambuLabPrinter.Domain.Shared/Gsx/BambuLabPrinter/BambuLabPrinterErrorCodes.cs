namespace Gsx.BambuLabPrinter;

public static class BambuLabPrinterErrorCodes
{
    private const string ErrorCodePrefix = "BambuLabPrinter";

    public static class Accounts
    {
        private const string AccountsPrefix = $"{ErrorCodePrefix}:Account";
        public const string AccountAlreadyExist = $"{AccountsPrefix}:0001";
    }
}
