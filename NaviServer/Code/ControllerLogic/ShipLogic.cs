using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaviServer.Code.ControllerPOD;
using NaviServer.Data;
using NaviServer.Models;

namespace NaviServer.Code.ControllerLogic
{
    public class ShipLogic
    {
        public static bool MoveShip(ShipMovement shipMovement)
        {
            using var db = new NaviContext();

            Ship ship = db.Ship.Find(new object[] { shipMovement.ShipID });
            if (ship == null)
                return false;

            Coordinates targetCoords = new Coordinates()
            {
                PosX = shipMovement.TargetX,
                PosY = shipMovement.TargetY
            };
            db.Coordinates.Add(targetCoords);
            db.Movement.Add(new Movement()
            {
                ShipId = shipMovement.ShipID,
                CoordinatesFromId = ship.CoordinatesId,
                CoordinatesTo = targetCoords
            });

            db.SaveChanges();
            return true;
        }
    }
}
