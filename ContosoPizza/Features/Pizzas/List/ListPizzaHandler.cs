using ContosoPizza.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.List
{
    public class ListPizzaHandler : IRequestHandler<ListPizzaRequest, ResultOf<List<ListPizzaResponse>>>
    {
        public ApplicationDbContext _context;

        public ListPizzaHandler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ResultOf<List<ListPizzaResponse>>> Handle(ListPizzaRequest request, CancellationToken cancellationToken)
        {
            var pizzaQuery = _context.Pizzas.AsQueryable();
            if (request.IsGlutenFreeFilter.HasValue)
            {
                pizzaQuery = pizzaQuery.Where(x => x.IsGlutenFree == request.IsGlutenFreeFilter);
            }

            if (!String.IsNullOrWhiteSpace(request.FilterByName))
            {
                pizzaQuery = pizzaQuery.Where(x => x.Name.Contains(request.FilterByName));
            }

            List<ListPizzaResponse> pizzas = await pizzaQuery.Skip((request.PageNumber - 1) * request.Quantity).Take(request.Quantity).Select(p => new ListPizzaResponse
            {
                Id = p.Id,
                IsGlutenFree = p.IsGlutenFree,
                Name = p.Name,
                Price = p.Price
            }).ToListAsync(cancellationToken);


            return pizzas;
        }
    }
}