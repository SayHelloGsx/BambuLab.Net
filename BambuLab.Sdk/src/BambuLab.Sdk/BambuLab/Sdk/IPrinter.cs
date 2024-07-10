using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BambuLab.Sdk;

public interface IPrinter : IPrinterReader, IPrinterOperator, IAsyncDisposable
{
    event Func<PrintStatusEnum, GcodeStateEnum, Task> OnPrintStatusChanged;
}
