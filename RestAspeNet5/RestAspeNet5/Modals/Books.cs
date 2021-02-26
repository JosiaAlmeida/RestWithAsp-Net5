using RestAspeNet5.Modals.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Modals
{
    [Table("Book")]
    public class Books : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("autor")]
        public string Autor { get; set; }
        [Column("enable")]
        public bool Enable { get; set; }


    }
}
