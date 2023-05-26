using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WajedApi.Models
{

    // id name userId description page pageId createdAt
    public class Alert
    {

        public int Id { get; set; }
        public string? UserId { get; set; }

        public string? Description { get; set; }
        public string? Page { get; set; }
        public int PageId { get; set; }

        

        public DateTime CreatedAt { get; set; }
        public Alert()
        {

            CreatedAt = DateTime.Now;

        }
        
    }
}