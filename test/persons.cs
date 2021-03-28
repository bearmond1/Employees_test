namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class persons
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string first_name { get; set; }

        [Required]
        [StringLength(100)]
        public string second_name { get; set; }

        [Required]
        [StringLength(100)]
        public string last_name { get; set; }

        public DateTime? date_employ { get; set; }

        public DateTime? date_uneploy { get; set; }

        public int status { get; set; }

        public int id_dep { get; set; }

        public int id_post { get; set; }
    }
}
