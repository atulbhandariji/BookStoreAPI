using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagemetAPI.Model
{
    public class TBLEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public int Age { get; set; }

        public DateTime Doj { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        public int IsMarried { get; set; }

        public int IsActive { get; set; }

        public int DesignationID { get; set; }

        [NotMapped]
        public string Designation { get; set; }
    }
}
