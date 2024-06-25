using BusinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomInformationDAO
    {
        private FuminiHotelManagementContext db = new FuminiHotelManagementContext();

        public List<RoomInformation> GetAllRooms()
        {
            List<RoomInformation> listRoomInfos = new List<RoomInformation>();
            try
            {
                listRoomInfos = db.RoomInformations.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listRoomInfos;
        }

        public List<RoomInformation> GetRoomsByRoomNumber(string number)
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            
            try
            {
                rooms = db.RoomInformations.
                    Where(r => r.RoomNumber.ToLower().Contains(number.ToLower()))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rooms;

        }

        public RoomInformation getRoomById(int id)
        {
            RoomInformation roomInfo = new RoomInformation();
            try
            {
                roomInfo = db.RoomInformations.FirstOrDefault(r => r.RoomId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return roomInfo;
        }


        public void AddRoom(RoomInformation room)
        {

            try
            {
                db.RoomInformations.Add(room);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void updateRoom(RoomInformation room)
        {
            
            try
            {
                db.RoomInformations.Update(room);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteRoom(int id)
        {
            try
            {
                RoomInformation room = db.RoomInformations.FirstOrDefault(r =>r.RoomId == id);
                db.RoomInformations.Remove(room);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
