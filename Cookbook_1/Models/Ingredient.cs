using Cookbook_1.ENums;

namespace Cookbook_1.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Тут ещё будет список рецептов, чтобы получить все рецепты, где используется ингредиент
    }
}
