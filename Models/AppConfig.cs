using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{
    public class AppConfig
    {
        public int Id { get; set; }
        public string? Key { get; set; }

        public string? Value { get; set; }
    }
}