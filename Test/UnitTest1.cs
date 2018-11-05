using System.Collections.Generic;
using AutoMapper;
using EleksTask;
using EleksTask.Dto;
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
        private readonly List<Category> _categories;
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly Mock<IMapper> _mapperMock;

        public UnitTest1()
        {
            _mapperMock = new Mock<IMapper>();
            _categories = new List<Category>
            {
                new Category()
                {
                    Name = "Phone"
                },
                new Category()
                {
                    Name = "Computer"
                }
            };


            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "db")
                .Options;


            using (var context = new ApplicationContext(_options))
            {
                context.AddRange(_categories);
                context.SaveChanges();
            }
        }

        [Fact]
        public async void Test1()
        {

            var _getAllCategory = new List<GetAllCategoryDto>
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

            using (var context = new ApplicationContext(_options))
            {
                var repository = new CategoryRepository(context);
                
                _mapperMock.Setup(x => x.Map<List<GetAllCategoryDto>>(It.IsAny<List<Category>>()))
                    .Returns(_getAllCategory);

                var services = new CategoryService(_mapperMock.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.GetAllCategories();
                var response = (result as OkObjectResult).Value as Response<List<GetAllCategoryDto>>;

                Assert.NotNull(response);
                Assert.NotNull(response.Data);
                Assert.Equal(2, response.Data.Count);
                Assert.True(_getAllCategory.Equals(response.Data));
            }
        }

        [Fact]
        public async void Test2()
        {
            var categoryDto = new GetCategoryDto()
            {
                Id = 1,
                Name = "Phone"
            };

            using (var context = new ApplicationContext(_options))
            {
                var repository = new CategoryRepository(context);

                _mapperMock.Setup(x => x.Map<GetCategoryDto>(It.IsAny<Category>()))
                    .Returns(categoryDto);

                var services = new CategoryService(_mapperMock.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.GetByIdAsync(1);
                var response = (result as OkObjectResult).Value as Response<GetCategoryDto>;

                Assert.NotNull(response);
                Assert.NotNull(response.Data);
                Assert.True(categoryDto.Equals(response.Data));
            }
        }

        [Fact]
        public async void Test3()
        {
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CategoryRepository(context);

                var services = new CategoryService(_mapperMock.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.DeleteCategoryAsync(1);
                var response = (result as OkObjectResult).Value as Response<bool>;

                Assert.NotNull(response);
                Assert.True(response.Data);

                var res =await repository.GetAll();

                Assert.Single(res);
                Assert.Equal(2,res[0].Id);
            }
        }

        [Fact]
        public async void Test4()
        {
            var newName = "For Home";
            var id = 1;
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CategoryRepository(context);

                var services = new CategoryService(_mapperMock.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.RenameCategoryAsync(id,newName);
                var response = (result as OkObjectResult).Value as Response<bool>;

                Assert.NotNull(response);
                Assert.True(response.Data);

                var res = await repository.GetById(id);

                Assert.Equal(newName,res.Name);
            }
        }

        [Fact]
        public async void Test5()
        {
            var categotyName = "Sport";
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CategoryRepository(context);

                var services = new CategoryService(_mapperMock.Object, repository);
                var controller = new CategoryController(services);
                var result = await controller.CreateCategoryAsync(categotyName);
                var response = (result as OkObjectResult).Value as Response<int>;
                Assert.NotNull(response);
                Assert.InRange(response.Data,1,int.MaxValue);
                var item =await repository.GetById(3);
                Assert.Equal(categotyName,item.Name);
            }
        }
    }
}
