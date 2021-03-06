﻿using AnemicPizza.API.DTO.Ingredient;
using AnemicPizza.Core.Models.Products;
using AutoMapper;
#pragma warning disable 1591

namespace AnemicPizza.API.Mappings
{
    public class PizzaIngredientToIngredientDTOMappingProfile : Profile
    {
        public PizzaIngredientToIngredientDTOMappingProfile()
        {
            var map = CreateMap<PizzaIngredient, IngredientDTO>();
            map.ConstructUsing((pi, ctx) =>
                ctx.Mapper.Map<IngredientDTO>(pi.Ingredient));
        }
    }
}
