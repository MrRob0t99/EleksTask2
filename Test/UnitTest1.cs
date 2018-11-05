using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using EleksTask.Repository;
using EleksTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var categoryList = new List<Category>
            {
                new Category()
                {
                    Id = 1,
                    Name = "Phone"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Computer"
                }
            };


            var categoryDto = new List<GetAllCategoryDto>
            {
                new GetAllCategoryDto()
                {
                    Id = 1,
                    Name = "Phone"
                },
                new GetAllCategoryDto()
                {
                    Id = 2,
                    Name = "Computer"
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "db")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.AddRange(categoryList);
                context.SaveChanges();
                var repository = new CategoryRepository(context);
                var mockMapper = new Mock<IMapper>();
                mockMapper.Setup(x => x.Map<List<GetAllCategoryDto>>(It.IsAny<List<Category>>()))
                    .Returns(categoryDto);
                var services = new CategoryService(mockMapper.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.GetAllCategories();
                var response = (result as OkObjectResult).Value as Response<List<GetAllCategoryDto>>;

                Assert.NotNull(response);
                Assert.NotNull(response.Data);
                Assert.Equal(2, response.Data.Count);
                Assert.True(categoryDto.Equals(response.Data));
            }
        }
    }
}
