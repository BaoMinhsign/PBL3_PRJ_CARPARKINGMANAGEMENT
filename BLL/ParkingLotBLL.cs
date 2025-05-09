using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ParkingLotBLL
    {
        ParkingLotDAL dl = new ParkingLotDAL();

        public List<string> getParkingLot()
        {
            List<string> list = dl.GetParkingLot();
            return list;
        }
    }
}
