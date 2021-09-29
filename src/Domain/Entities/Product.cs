using TopupPortal.Domain.Common;
using TopupPortal.Domain.Enums;
using TopupPortal.Domain.Events;
using System;
using System.Collections.Generic;

namespace TopupPortal.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal Rate { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
