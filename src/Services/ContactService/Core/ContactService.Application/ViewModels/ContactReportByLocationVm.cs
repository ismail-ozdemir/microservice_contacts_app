using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.ViewModels
{
    public class ContactReportByLocationVm
    {
        public string LocationName { get; set; } = string.Empty;
        public long PersonCountInLocation { get; set; }
        public long PhoneNumberCountInLocation { get; set; }
    }
}
