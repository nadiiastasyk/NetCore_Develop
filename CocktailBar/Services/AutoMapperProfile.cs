using AutoMapper;
using CocktailBar.Models;
using System.Collections.Generic;

namespace CocktailBar.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var list = new List<string>();
            CreateMap<Drink, Cocktail>()
                .ForMember(name => name.Name, strDrink => strDrink.MapFrom(src => src.strDrink))
                .ForMember(ingredients => ingredients.Ingredients, ingredients => ingredients.MapFrom(src => src.strIngredient1))
                .ForMember(ingredients => ingredients.Image, ingredients => ingredients.MapFrom(src => src.strDrinkThumb))
                .ForMember(ingredients => ingredients.Id, ingredients => ingredients.MapFrom(src => src.idDrink));

            //.ForMember(ingredients => ingredients.Ingredients, ingredients => ingredients.MapFrom(src => src.strIngredient2))
            //.ForMember(ingredients => ingredients.Ingredients, ingredients => ingredients.MapFrom(src => src.strIngredient3))
            //.ForMember(ingredients => ingredients.Ingredients, ingredients => ingredients.MapFrom(src => src.strIngredient4))
            //.ForMember(ingredients => ingredients.Ingredients, ingredients => ingredients.MapFrom(src => src.strIngredient5));
        }
    }
}
