using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ParkingLotDAL
    {
        DataAccessEntity db = new DataAccessEntity();

        public List<string> GetParkingLot()
        {
            var parkingLotNames = db.ParkingLots.Select(pl => pl.TenBaiXe).ToList();

            return parkingLotNames;
        }
    }
}
