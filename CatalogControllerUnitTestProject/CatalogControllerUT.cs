using Catalog.API.Controllers;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Diagnostics;

namespace CatalogControllerUnitTestProject
{
    public class CatalogControllerUT
    {
        private readonly CatalogController _sut;
        private readonly Mock<IProductRepository> _repositoryMock = new Mock<IProductRepository>();
        private readonly Mock<ILogger<CatalogController>> _loggerMock = new Mock<ILogger<CatalogController>>();

        public CatalogControllerUT()
        {
            _sut = new CatalogController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void GetProducts_ValidCall()
        {
            _repositoryMock.Setup(x => x.GetProducts()).ReturnsAsync(GetSampleProducts());

           
            var actual = await _sut.GetProducts();
            var actual_products = (actual.Result as OkObjectResult).Value as IEnumerable<Product>;

            var expected = GetSampleProducts();
            Assert.Equal((actual.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<ActionResult<IEnumerable<Product>>>(actual);
            Assert.Equal(actual_products.Count(), expected.Count());


            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Equal(expected.ElementAt(i).Id, actual_products.ElementAt(i).Id);
                Assert.Equal(expected.ElementAt(i).Name, actual_products.ElementAt(i).Name);
                Assert.Equal(expected.ElementAt(i).Category, actual_products.ElementAt(i).Category);
                Assert.Equal(expected.ElementAt(i).Summary, actual_products.ElementAt(i).Summary);
                Assert.Equal(expected.ElementAt(i).Description, actual_products.ElementAt(i).Description);
                Assert.Equal(expected.ElementAt(i).ImageFile, actual_products.ElementAt(i).ImageFile);
                Assert.Equal(expected.ElementAt(i).Price, actual_products.ElementAt(i).Price);
            }
        }

        [Fact]
        public async void GetProductById_ValidCall()
        {
            var productID = "602d2149e773f2a3990b47f5";
            _repositoryMock.Setup(x => x.GetProduct(productID)).ReturnsAsync(GetSampleProducts().FirstOrDefault(p => p.Id == productID));

            var actual = await _sut.GetProductById(productID);
            var actual_product = (actual.Result as OkObjectResult).Value as Product;

            var expected = GetSampleProducts().FirstOrDefault(p => p.Id == productID);

            Assert.Equal((actual.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.True(actual_product != null);

            Assert.Equal(expected.Id, actual_product.Id);
            Assert.Equal(expected.Name, actual_product.Name);
            Assert.Equal(expected.Category, actual_product.Category);
            Assert.Equal(expected.Summary, actual_product.Summary);
            Assert.Equal(expected.Description, actual_product.Description);
            Assert.Equal(expected.ImageFile, actual_product.ImageFile);
            Assert.Equal(expected.Price, actual_product.Price);
        }

        [Fact]
        public async void GetProductByCategory_ValidCall()
        {
            var productCategory = "Phones";
            _repositoryMock.Setup(x => x.GetProductByCategory(productCategory))
                .ReturnsAsync(GetSampleProducts().Where(p => p.Category == productCategory));

            var response = await _sut.GetProductByCategory(productCategory);

            Assert.IsType<ActionResult<IEnumerable<Product>>>(response);

        }

        [Fact]
        public async void GetProductById_NotFoundId()
        {
            // random id
            var productID = "602d2149e773f2a3990b47f9";
            _repositoryMock.Setup(x => x.GetProduct(productID)).ReturnsAsync(GetSampleProducts().FirstOrDefault(p => p.Id == productID));

            ActionResult<Product> response = await _sut.GetProductById(productID);
            var actual = response.Result as NotFoundResult;
            Assert.NotNull(actual);

        }

        [Fact]
        public async void CreateProduct_ValidCall()
        {
            var product = new Product()
            {
                Id = "602d2149e773f2a3990b47f9",
                Name = "IPhone X",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            };

            _repositoryMock.Setup(x => x.CreateProduct(product));

            ActionResult<Product> response = await _sut.CreateProduct(product);
            Assert.IsType<ActionResult<Product>>(response);
            Assert.NotNull(response.Result as CreatedAtRouteResult);
            _repositoryMock.Verify(x => x.CreateProduct(product), Times.Exactly(1));
        }

        [Fact]
        public async void UpdateProduct_ValidCall()
        {
            var product = new Product();

            _repositoryMock.Setup(x => x.UpdateProduct(product));

            var response = await _sut.UpdateProduct(product);
            Assert.NotNull(response as OkObjectResult);
            _repositoryMock.Verify(x => x.UpdateProduct(product), Times.Exactly(1));
        }

        [Fact]
        public async void DeleteProduct_ValidCall()
        {
            string id = "602d2149e773f2a3990b47f9";

            _repositoryMock.Setup(x => x.DeleteProduct(id));

            var response = await _sut.DeleteProductById(id);
            Assert.IsType<OkObjectResult>(response);
            _repositoryMock.Verify(x => x.DeleteProduct(id), Times.Exactly(1));
        }

        private IEnumerable<Product> GetSampleProducts()
        {
            List<Product> output = new List<Product>
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                }
            };

            return output;
        }
    }
}
