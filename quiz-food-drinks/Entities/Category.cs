namespace quiz_food_drinks.Entities;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CategoryName { get; set; }
}