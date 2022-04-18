using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyLogTransfer.Models
{
    public class FileInformation
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
