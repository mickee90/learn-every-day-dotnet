using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Models
{
    public class User : BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DefaultValue(2)]
        public int UserTypeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string ZipCode { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Phone { get; set; }

        [Required]
        [DefaultValue(false)]
        public Boolean Disabled { get; set; }

        [Required]
        [DefaultValue(false)]
        public Boolean Banned { get; set; }

        [Required]
        [DefaultValue(1)]
        public int CountryId { get; set; }

        [MaxLength(255)]
        public string Avatar { get; set; }
    }
}
