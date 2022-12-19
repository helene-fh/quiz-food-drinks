using System.ComponentModel.DataAnnotations;

namespace quiz_food_drinks.ViewModels;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}