using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaviServer.Data;
using NaviServer.Models;
using Newtonsoft.Json.Linq;

namespace NaviServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        [HttpPost]
        [Route("MoveShip")]
        public bool MoveShip([FromBody] JObject data)
        {
            uint shipID = data["shipID"].ToObject<uint>();
            float targetX = data["targetX"].ToObject<float>();
            float targetY = data["targetY"].ToObject<float>();

            using var db = new NaviContext();
            
            Ship ship = db.Ship.Find(new object[] { shipID });
            if (ship == null)
                return false;

            Coordinates targetCoords = new Coordinates()
            {
                PosX = targetX,
                PosY = targetY
            };
            db.Coordinates.Add(targetCoords);
            db.Movement.Add(new Movement()
            {
                ShipId = shipID,
                CoordinatesFromId = ship.CoordinatesId,
                CoordinatesTo = targetCoords
            });

            db.SaveChanges();
            return true;
        }
    }
}
