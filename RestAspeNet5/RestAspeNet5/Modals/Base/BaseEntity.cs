using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Modals.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long ID { get; set; }
    }
}
