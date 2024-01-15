using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.Seed
{
    public class NumberCycler
    {
        int _startNum;
        int _current;
        int _avoidModZeroNum;
        public NumberCycler(int start, int maxNumberIncld)
        {
            _startNum = start;
            _current = start;
            _avoidModZeroNum = maxNumberIncld + 1;
        }
        public void setMinMax(int start, int maxNumberIncld)
        {
            _startNum = start;
            _current = start;
            _avoidModZeroNum = maxNumberIncld + 1;
        }

        public int NextNum()
        {
            
            while(_current % _avoidModZeroNum < _startNum)
            {
                _current++;
            }

            _current = _current % _avoidModZeroNum;
            return _current++;
            
        }

    }
    public static class SeedDb
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                .HasData(
                    new Vendor
                    {
                        Id = 1,
                        Deleted= false,
                        VendorName = "Unassigned",
                    }
                );
            modelBuilder.Entity<Department>()
                .HasData(
                    new Department
                    {
                        Id = 1,
                        Deleted = false,
                        DepartmentName = "Unassigned",

                    }
                );
            modelBuilder.Entity<DeviceType>()
                .HasData(
                    new DeviceType
                    {
                        Id = 1,
                        Deleted = false,
                        DeviceTypeName = "Unassigned",
                    }
                );
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id =1,
                        Deleted = false,
                        DepartmentId = 1,
                        RoleName = "Unassigned",
                    }
                );
            modelBuilder.Entity<Contract>()
                .HasData(
                    new Contract
                    {
                        Id = 1,
                        Deleted = false,
                        ContractName = "Unassigned",
                        VendorId = 1,
                        StartDate = new DateTime(1980,1,1),
                        EndDate = new DateTime(1980,1,2),
                    }
                );
            modelBuilder.Entity<HwModel>()
                .HasData(
                    new HwModel
                    {
                        Deleted = false,
                        Id = 1,
                        DeviceTypeId = 1,
                        VendorId = 1,
                        HwModelName = "Unassigned",
                    }
                );
            modelBuilder.Entity<Person>()
                .HasData(
                    new Person
                    {
                        Deleted = false,
                        Id = 1,
                        Fname = "FirstNameTest",
                        Lname = "LastNameTest",
                        RoleId = 1,
                    }
                );
            modelBuilder.Entity<Device>()
                .HasData(
                    new Device
                    {
                        Deleted = false,
                        Id = 1,
                        AssetNumber = "A0001",
                        ServiceTag = "A1A",
                        ContractId = 1,
                        HwModelId = 1,
                        Notes = "TestNotes",
                        DeviceName = "Device0000",

                    }
                );
            modelBuilder.Entity<PersonDevice>()
                .HasData(
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 1,
                        DeviceId = 1,
                        PersonId = 1,
                        StartDate = new DateTime(1980, 2, 1),
                        EndDate = new DateTime(1980, 2, 3),

                    }
                );
        }

        public static void SeedTestData01(this ModelBuilder modelBuilder)
        {


            //End Point Entities
            for (int i = 2; i < 7; i++)
            {
                modelBuilder.Entity<Vendor>()
                    .HasData(
                        new Vendor
                        {
                            Id = i,
                            Deleted = false,
                            VendorName = "TestVendor0" + i.ToString(),
                        }
                    );

                modelBuilder.Entity<Department>()
                    .HasData(
                        new Department
                        {
                            Id = i,
                            Deleted = false,
                            DepartmentName = "TestDepartment0" + i.ToString(),

                        }
                    );
                modelBuilder.Entity<DeviceType>()
                    .HasData(
                        new DeviceType
                        {
                            Id = i,
                            Deleted = false,
                            DeviceTypeName = "TestDeviceType0" + i.ToString(),
                        }
                    );

                modelBuilder.Entity<Role>()
                    .HasData(
                        new Role
                        {
                            Id = i,
                            Deleted = false,
                            DepartmentId = 1,
                            RoleName = "TestRole0" + i.ToString(),
                        }
                    );

            }
        }


        public static void SeedTestData02(this ModelBuilder modelBuilder)
        {
            NumberCycler numCycle = new NumberCycler(2, 6);

            for (int i = 2; i < 12; i++) {

                    modelBuilder.Entity<Contract>()
                       .HasData(
                           new Contract
                           {
                               Id = i,
                               Deleted = false,
                               ContractName = "TestContract0" + i.ToString(),
                               VendorId = numCycle.NextNum(),
                               StartDate = new DateTime(1980, 1, 1),
                               EndDate = new DateTime(1980, 1, 2),
                           }
                       );

            }

            numCycle.setMinMax(2, 5);

            for (int i = 2; i < 8; i++)
            {
                int nextNum = numCycle.NextNum();
                    modelBuilder.Entity<HwModel>()
                        .HasData(
                            new HwModel
                            {
                                Deleted = false,
                                Id = i,
                                DeviceTypeId = nextNum,
                                VendorId = nextNum,
                                HwModelName = "TestHwModel0" + i.ToString(),
                            }
                        );

            }

            numCycle.setMinMax(2, 3);

            for (int i = 2; i < 14; i++)
            {
                int nextNum = numCycle.NextNum();
                modelBuilder.Entity<Person>()
                        .HasData(
                            new Person
                            {
                                Deleted = false,
                                Id = i,
                                Fname = "FirstNameTest0" + i.ToString(),
                                Lname = "LastNameTest0" + i.ToString(),
                                RoleId = nextNum,
                            }
                        );
                
            }
        }
        public static void SeedTestData03(this ModelBuilder modelBuilder)
        {
            NumberCycler numCycle = new NumberCycler(2, 6);
            for (int i = 2; i < 42; i++)
            {
                int nextNum = numCycle.NextNum();
                modelBuilder.Entity<Device>()
                        .HasData(
                            new Device
                            {
                                Deleted = false,
                                Id = i,
                                AssetNumber = "A00010" + i.ToString(),
                                ServiceTag = "A1A0" + i.ToString(),
                                ContractId = nextNum,
                                HwModelId = nextNum,
                                Notes = "TestNotes0" + i.ToString(),
                                DeviceName = "TestDevice0000" + i.ToString(),

                            }
                        );
                
            }
        }
        public static void SeedTestData04(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonDevice>()
                .HasData(
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 2,
                        DeviceId = 2,
                        PersonId = 1,
                        StartDate = new DateTime(2000, 2, 1),
                        EndDate = new DateTime(2000, 2, 5),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 3,
                        DeviceId = 2,
                        PersonId = 2,
                        StartDate = new DateTime(2000, 2, 6),
                        EndDate = new DateTime(2000, 2, 10),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 4,
                        DeviceId = 2,
                        PersonId = 3,
                        StartDate = new DateTime(2000, 2, 11),
                        EndDate = new DateTime(2000, 2, 16),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 5,
                        DeviceId = 3,
                        PersonId = 2,
                        StartDate = new DateTime(2000, 2, 6),
                        EndDate = new DateTime(2000, 2, 10),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 6,
                        DeviceId = 3,
                        PersonId = 1,
                        StartDate = new DateTime(2000, 2, 1),
                        EndDate = new DateTime(2000, 2, 5),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 7,
                        DeviceId = 3,
                        PersonId = 2,
                        StartDate = new DateTime(2000, 3, 1),
                        EndDate = new DateTime(2000, 3, 5),

                    },
                    new PersonDevice
                    {
                        Deleted = false,
                        Id = 8,
                        DeviceId = 2,
                        PersonId = 1,
                        StartDate = new DateTime(2000, 3, 1),
                        EndDate = new DateTime(2000, 3, 5),

                    }
                );

        }
    }
}
