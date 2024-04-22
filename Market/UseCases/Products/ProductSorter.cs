using Market.DAL;
using Market.DTO;
using Market.Enums;
using Market.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Market.UseCases.Products
{
    public class ProductSorter
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> sources, SortType? sortType, bool isAscending)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            switch (sortType)
            {
                case SortType.Name:
                    if (isAscending)
                    {
                        sources = sources.OrderBy(item => item.Name);
                    }
                    else
                    {
                        sources = sources.OrderByDescending(item => item.Name);
                    }
                    break;
                case SortType.Price:
                    if (isAscending)
                    {
                        sources = sources.OrderBy(item => item.PriceInRubles);
                    }
                    else
                    {
                        sources = sources.OrderByDescending(item => item.PriceInRubles);
                    }
                    break;
                default:
                    throw new Exception($"Not processed by SortType: {sortType}");
            }

            return sources.ToList();
        }
    }
}
