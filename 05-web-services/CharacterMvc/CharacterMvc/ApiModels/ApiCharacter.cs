using System.ComponentModel.DataAnnotations;

namespace CharacterMvc.ApiModels
{
    public class ApiCharacter
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
