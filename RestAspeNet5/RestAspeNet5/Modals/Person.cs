using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Modals
{
    [Table("person2")]
    public class Person
    {
        [Column("id")]
        public long ID { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("address")]
        public string Adress { get; set; }


    }
}
