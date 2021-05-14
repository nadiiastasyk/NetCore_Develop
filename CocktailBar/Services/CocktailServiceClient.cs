using AutoMapper;
using CocktailBar.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace CocktailBar.Services
{
    public class CocktailServiceClient : ICocktailServiceClient
    {
        private readonly IRestClient _restClient;

        private readonly IMapper _mapper;

        public CocktailServiceClient(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public Cocktail[] GetCocktails()
        {
            try
            {
                var request = new RestRequest("https://www.thecocktaildb.com/api/json/v1/1/search.php?s=margarita", Method.GET);
                var result = _restClient.Execute(request).Content;

                DrinkAPIList jsonResponse = JsonConvert.DeserializeObject<DrinkAPIList>(result);
                Cocktail[] mappedResponse = _mapper.Map<Drink[], Cocktail[]>(jsonResponse.drinks);

                return mappedResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
