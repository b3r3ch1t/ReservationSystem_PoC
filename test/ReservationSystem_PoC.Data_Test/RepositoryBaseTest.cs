﻿using ReservationSystem_PoC.Data.Context;
using System.Linq;
using ReservationSystem_PoC.Common;
using ReservationSystem_PoC.Common.Context;
using Xunit;

namespace ReservationSystem_PoC.Data.Test
{
    public class RepositoryBaseTest
    {
        private readonly ReservarionSystemDbContext _db;



        public RepositoryBaseTest()
        {

            _db = ReservarionSystemDbContextFaker.GetDatabaseInMemory();

            var x = _db.ContactTypes.ToList();
        }


        [Fact]
        public void Get()
        {

        }
    }
}
