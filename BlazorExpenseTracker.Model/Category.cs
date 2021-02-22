using System.ComponentModel.DataAnnotations;

namespace BlazorExpenseTracker.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por Favor, rellene la descripcion de la categoria")]
        [StringLength(150)]
        public string Name { get; set; }
    }
}