using PaparaProject.Domain.Entities;
using PaparaProject.Persistence.Repositories.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            EfFlatTypeRepository ef = new EfFlatTypeRepository();
            FlatType flatType = new FlatType
            {
                FlatTypeName = "2+1",
                CreatedBy = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            await ef.AddAsync(flatType);

            foreach (var item in await ef.GetAllAsync())
            {
                Console.WriteLine(item.FlatTypeName);
            }

            Console.ReadKey();
        }
    }
}
