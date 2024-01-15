using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoMapper;
using InvEquip;
using InvEquip.Logic.Service;
using InvEquip.Data;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Logic;
using Microsoft.Extensions.Hosting;
using InvEquip.Data.Mapper;
using InvEquip.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

namespace InvEquipTest
{
    public class MapperFixture
    {
        public IMapper _mapper;
        public MapperFixture()
        {
            var mapperconfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new AutoMapper.Mapper(mapperconfig);
        }
    }
    public class DiFixture 
    {

        public DiFixture()
        {

            var _host = Host.CreateDefaultBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddScoped<IUnitOfWork, UnitOfWork>();
                  services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
                  services.AddScoped<ApplicationDbContext>();
                  services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
                  services.AddScoped<IPersonDeviceService, PersonDeviceService>();
                  services.AddScoped<IApplicationPersonService, ApplicationService>();
                  services.AddScoped<IApplicationDepartmentService, ApplicationService>();
                  services.AddScoped<IApplicationPersonDeviceService, ApplicationService>();
                  services.AddScoped<IApplicationDeviceTypeService, ApplicationService>();
                  services.AddScoped<IApplicationDeviceService, ApplicationService>();

              }).Build();

            var serviceScope = _host.Services.CreateScope();
            services = serviceScope.ServiceProvider;
        }
        public IServiceProvider services { get; set; }

    }
    public class UnitTest1 : IClassFixture<DiFixture>, IClassFixture<MapperFixture>
    {
        DiFixture _diFixture;
        MapperFixture _mapperFixture;
        public UnitTest1(DiFixture diFixture, MapperFixture mapperFixture)
        {
            _diFixture = diFixture;
            _mapperFixture = mapperFixture;
        }
        
        [Fact]
        public void Test1()
        {
            //Arange

            var mapper = _mapperFixture._mapper;
            var unitOfWork = _diFixture.services.GetRequiredService<IUnitOfWork>();
            ApplicationService app = new ApplicationService(unitOfWork, mapper);


            //Action

            var returnString = app.testRun();

            //Assert
            Assert.Equal("One", returnString);
        }

        [Fact]
        public void ReturnListOfDevicesFromPersonId()
        {
            //Arrange
            var mapper = _mapperFixture._mapper;
            var unitOfWork = _diFixture.services.GetRequiredService<IUnitOfWork>();
            ApplicationService applicationService = new ApplicationService(unitOfWork, mapper);

            //Act
            var list = applicationService.ShowPersonsAssignDevices(1);

            //Assert
            Assert.NotNull(list);


        }
        
        [Fact]
        public void GivePersonIdReturnDevicesWithAssignDate()
        {
            //Arrange
            var mapper = _mapperFixture._mapper;
            var unitofwork = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var personId = 1;
            var expectedItems = 4;
            //Action
            ApplicationService app = new ApplicationService(unitofwork, mapper);
            var list = app.ShowPersonsAssignDeviceWithDate(personId);
            var total_items = list.Result.Count;
            //Assert
            Assert.Equal(expectedItems, total_items);
        }

        [Fact]
        public void GivenPersonIdShowCurrentAssignedPC()
        {
            //arrange

            var mapper = _mapperFixture._mapper;
            var unitofwork = _diFixture.services.GetRequiredService<IUnitOfWork>();
            ApplicationService app = new ApplicationService(unitofwork, mapper);
            var expectedPCDto = new DeviceDateDto()
            {
                DeviceId = 2,
                StartDate = new DateTime(2000, 03, 01),
                EndDate = new DateTime(2000, 03, 05),
                DeviceType = "TestDeviceType02",
                HwModelName = "TestHwModel02",
                DeviceName = "TestDevice00002",
            };

            //action
            var resultantDto = app.ShowCurrentAssignedPCDevice(1).Result;

            //assert
            Assert.True(expectedPCDto.Equals(resultantDto));
        }

        [Fact]
        public void GivenDeviceType_ReturnAllUnAssociatedDevices()
        {
            //Arrange
            var unitofwork = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var mapper = _mapperFixture._mapper;
            ApplicationService app = new ApplicationService(unitofwork, mapper);
            int ExpectedDeviceCount = 40;

            //Action
            var freeDevicesDto = app.ShowUnassignedDevicesByType(2);
            var ActualDeviceCount = ((IList<DeviceDto>)freeDevicesDto.Result).Count;
            //Assert
            Assert.Equal(ExpectedDeviceCount, ActualDeviceCount);
        }
        [Fact]
        public async Task GivenPersonAttrib_CreateNewPerson()
        {
            //Arrange
            var _uow = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var _mapper = _mapperFixture._mapper;
            ApplicationService app = new ApplicationService(_uow, _mapper);

            string firstname = "TestBob01";
            string lastname = "TestSmith01";
            int roleId = 1;

            PersonDto newPersonDto = new PersonDto()
            {
                Fname = firstname,
                Lname = lastname,
                RoleId = roleId,
            };

            //Action
            var person = await app.CreateNewPersonAsync(newPersonDto);
            var resultantPersonDto = app.GetPersonDtoByFirstLastNameAsync(firstname, lastname).Result;
            //Assert
            Assert.True(newPersonDto.Equals(resultantPersonDto));
        }
        [Fact]
        public void GiveDeviceType_ReturnAvailableDevicesDto()
        {
            //Arrange
            var _uow =  _diFixture.services.GetRequiredService<IUnitOfWork>();
            var _app = _diFixture.services.GetRequiredService<IApplicationPersonDeviceService>();
            var _mapper = _mapperFixture._mapper;
            int deviceType = 2;
            int expectedDeviceCouunt = 14;


            //Action
            IEnumerable<DeviceDto> deviceDtos = _app.ShowUnassignedDevicesByType(deviceType).Result;

            int resultantCountDtos = ((IList<DeviceDto>)deviceDtos).Count;
            //Assert
            Assert.Equal(expectedDeviceCouunt, resultantCountDtos);
        }
        [Fact]
        public async Task ReturnDeleteResults ()
        {
            //Arrange

            var _uow = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var _app = _diFixture.services.GetRequiredService<IApplicationDeviceTypeService>();
            int entityId = 11;

            //Action
            ResultStatus result = await _app.DeleteById(entityId);

            //Assert
            Assert.True(result == ResultStatus.Success);
        }

        [Fact]
        public async Task ReturnDeleteDeviceResult()
        {
            //Arrange
            var _uow = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var _app = _diFixture.services.GetRequiredService<IApplicationDeviceService>();
            int entityId = 37;

            //Action
            ResultStatus result = await _app.DeleteDeviceDataByIdAsync(entityId);

            //Assert
            Assert.True(result == ResultStatus.Success);
        }
        [Fact]
        public async Task ReturnRetiredDevices()
        {
            //Arrange
            var _uow = _diFixture.services.GetRequiredService<IUnitOfWork>();
            var _app = _diFixture.services.GetRequiredService<IApplicationPersonDeviceService>();


            //Action
            IEnumerable<DeviceDto> deviceDto = await _app.ShowDevicesByQueryObject(null, true, false, null,null);

            //Assert
            Assert.True(deviceDto != null);
        }

    }


}
