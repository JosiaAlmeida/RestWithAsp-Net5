using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Modals
{
    [Table("Book")]
    public class Books
    {
        [Column("id")]
        public long ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("autor")]
        public string Autor { get; set; }


    }
}
