using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorExpenseTracker.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage="Por Favor, rellene la descripcion de la categoria")]
        [EmailAddress]
        [StringLength(150)]
        public string Name { get; set; }
    }
}
