using CoreArchitectureDesign.Business.Abstractions;
using CoreArchitectureDesign.Core.Common;
using CoreArchitectureDesign.Core.Enums;
using CoreArchitectureDesign.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace CoreArchitectureDesign.Test.Core
{
    public class EfRepositoryTestWithMockObjects
    {
        private readonly ICategoryService categoryService;

        public EfRepositoryTestWithMockObjects()
        {
            var cList = new List<Categories>
            {
                new Categories {CategoryId=1,CategoryName="TestCategory1",Description="TestCategoryDescription1" },
                new Categories {CategoryId=2,CategoryName="TestCategory2",Description="TestCategoryDescription2" },
                new Categories {CategoryId=3,CategoryName="TestCategory3",Description="TestCategoryDescription3" }
            };


            var categoryList = new Result<IEnumerable<Categories>>
            {
                ResultObject = cList
            };

            var category = new Result<Categories>();

            var mockCategoryRepository = new Mock<ICategoryService>();

            // Setup for GetList
            mockCategoryRepository.Setup(mr => mr.GetList(null)).Returns(categoryList);

            // Setup for GetById
            mockCategoryRepository.Setup(mr => mr.GetById(It.IsAny<int>())).Callback(
                (int targetId) =>
                {
                    var original = cList.SingleOrDefault(q => q.CategoryId == targetId);

                    if (original == null)
                    {
                        category.ResultStatus = false;
                        category.ResultObject = null;
                        category.ResultMessage = "Fail";
                        category.ResultCode = (int)ResultStatusCodes.NotFound;
                    }
                    else
                    {
                        category.ResultObject = original;
                    }
                }).Returns(category);


            // Setup for Insert
            mockCategoryRepository.Setup(mr => mr.Add(It.IsAny<Categories>())).Callback(
                (Categories target) =>
                    {
                        cList.Add(target);
                        categoryList.ResultObject = cList;
                    });

            // Setup for Update
            mockCategoryRepository.Setup(mr => mr.Update(It.IsAny<Categories>())).Callback(
                (Categories target) =>
                {
                    var original = cList.SingleOrDefault(q => q.CategoryId == target.CategoryId);

                    if (original == null)
                    {
                        throw new InvalidOperationException();
                    }

                    original.CategoryName = target.CategoryName;
                    original.Description = target.Description;
                    categoryList.ResultObject = cList;
                });

            // Setup for Delete
            mockCategoryRepository.Setup(mr => mr.Delete(It.IsAny<Categories>())).Callback(
                (Categories target) =>
                {
                    var original = cList.SingleOrDefault(q => q.CategoryId == target.CategoryId);

                    cList.Remove(original);
                    categoryList.ResultObject = cList;
                });

            // Setup for DeleteAll
            mockCategoryRepository.Setup(mr => mr.DeleteAll(It.IsAny<Expression<Func<Categories, bool>>>())).Callback(
               (Expression<Func<Categories, bool>> filter) =>
               {
                   if (filter == null) return;

                   var filteredList = cList.AsQueryable().Where(filter).ToList();

                   cList.Except(filteredList).ToList();

                   categoryList.ResultObject = cList.Except(filteredList).ToList();
               });

            this.categoryService = mockCategoryRepository.Object;
        }

        [Fact]
        public void GetList_CategoryItemsThanCheckCountTest_Succeded()
        {
            var expected = this.categoryService.GetList(null).ResultObject.ToList().Count;
            Assert.True(expected > 0);
        }

        [Fact]
        public void IsEqual_InsertCategoryThanCheckGetListCount_True()
        {
            int actual = this.categoryService.GetList().ResultObject.ToList().Count + 1;

            var user = new Categories { CategoryId = 4, CategoryName = "TestCategory4", Description = "TestCategoryDescription4" };

            this.categoryService.Add(user);

            int expected = this.categoryService.GetList().ResultObject.ToList().Count;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IsEqual_UpdateCategoryThanCheckItIsUpdated_True()
        {
            var actual = new Categories { CategoryId = 2, CategoryName = "TestCategory2_Updated", Description = "TestCategory2Description_Updated" };

            this.categoryService.Update(actual);

            var expected = this.categoryService.GetList().ResultObject.ToList().SingleOrDefault(m => m.CategoryId == actual.CategoryId);

            Assert.NotNull(expected);
            Assert.Equal(actual.CategoryName, expected.CategoryName);
            Assert.Equal(actual.Description, expected.Description);
        }

        [Fact]
        public void Delete_CategoryItem_Succeed()
        {
            var deleteObject = new Categories { CategoryId = 2, CategoryName = "TestCategory2", Description = "TestCategoryDescription2" };

            this.categoryService.Delete(deleteObject);

            var expected = this.categoryService.GetList().ResultObject.ToList().SingleOrDefault(m => m.CategoryId == deleteObject.CategoryId);

            Assert.Null(expected);
        }

        [Fact]
        public void DeleteAll_CategoryItems_Succeed()
        {
            this.categoryService.DeleteAll(m => m.CategoryName.Contains("Test"));

            int expectedCount = this.categoryService.GetList().ResultObject.ToList().Count;

            Assert.Equal(0, expectedCount);
        }

        [Fact]
        public void GetById_CategoryItem_Succeed()
        {
            var actual = this.categoryService.GetById(2);

            Assert.NotNull(actual);
        }

        [Fact]
        public void GetById_CategoryItem_ReturnNull()
        {
            var actual = this.categoryService.GetById(5);

            Assert.Null(actual.ResultObject);
        }
    }
}
