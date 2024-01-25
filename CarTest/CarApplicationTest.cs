﻿using Car.Core.Dto;
using Car.Core.ServiceInterface;

namespace CarTest
{
    public class CarApplicationTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyCar_WhenReturnResult()
        {
            CarsDto dto = new CarsDto();

            dto.CarBrand = "Toyota";
            dto.CarModel = "C-HR";
            dto.CarYear = 2024;
            dto.CarColor = "Gray";
            dto.CarPrice = 32000;
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            var result = await Svc<ICarServices>().Create(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_DeleteByIdCar_WhenDeleteCar()
        {
            //Arrange
            CarsDto car = MockCarData();

            //Act
            var addCar = await Svc<ICarServices>().Create(car);
            var result = await Svc<ICarServices>().Delete((Guid)addCar.Id);

            //Assert
            Assert.Equal(result, addCar);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdCar_WhenDidNotDeleteTheWrightCar()
        {
            CarsDto car = MockCarData();

            var addCar = await Svc<ICarServices>().Create(car);
            var addCar2 = await Svc<ICarServices>().Create(car);

            var result = await Svc<ICarServices>().Delete((Guid)addCar2.Id);

            Assert.NotEqual(result.Id, addCar.Id);
        }

        [Fact]
        public async Task ShouldNot_UpdateCar_WhenNotUpdateDatata()
        {
            CarsDto dto = MockCarData();
            await Svc<ICarServices>().Create(dto);

            CarsDto nullUpdate = MockNullCar();
            await Svc<ICarServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        private CarsDto MockCarData()
        {
            CarsDto car = new()
            {
                CarBrand = "BMW",
                CarModel = "iX2",
                CarYear = 2023,
                CarColor = "red",
                CarPrice = 85700,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return car;
        }

        private CarsDto MockNullCar()
        {
            CarsDto nullDto = new()
            {
                Id = null,
                CarBrand = "Audi",
                CarModel = "A6",
                CarYear = 2024,
                CarColor = "Black",
                CarPrice = 58325,
                CreatedAt = DateTime.Now.AddYears(1),
                UpdatedAt = DateTime.Now.AddYears(1),
            };

            return nullDto;
        }
    }
}