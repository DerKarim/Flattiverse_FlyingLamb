using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flattiverse;
using System.Threading;
using System.Drawing;


namespace FlyingLambV1
{
    public class Controller
    {
        Connector connector;
        UniverseGroup universeGroup;
        
        

        Ship ship;
        Boolean running;
        private readonly Map map = new Map();

        int xImpulse, yImpulse, direction; //Antrieb
        float scanAngle = 0;    //Scan soll bei 0 anfangen
        public float drawX { get; set; }
        public float drawY { get; set; }

        /*/////  P R O P E R T Y S  /////*/
        public Boolean ShipReady { get { return (ship != null); } }     //  True sobald das Raumschiff erreichbar ist
        public List<Unit> Units { get { return map.Units; } }       //  Liste der (gescannten?) Units aus dem Directory der Klasse Map
        public float ShipRadius { get { return ship.Radius; } }         //  Radius des Raumschiffes
        public float ShipEnergyMax { get { return ship.EnergyMax; } }   //  Maximale Energie des Raumschiffes
        public float ShipEnergyLive { get { return ship.Energy; } }       //  Aktuelle Energie des Raumschiffes
        public UniverseGroup univerGroupType { get { return universeGroup; } }
        public int ShotsAvailable { get { return (int)ship.WeaponProductionStatus; } } //Verfügbare Schüsse des Schiffs
        public Vector VecDir
        {
            get
            {



                Vector direction = Vector.FromXY(drawX, drawY);
                
                float maxTime = ship.WeaponShot.Time.Limit; // maximale Laufzeit eines Schusses
                float maxSpeed = ship.WeaponShot.Speed.Limit; // maximale Geschwindigkeit eines Schusses

                float shootLimit = maxTime * maxSpeed;

                float needTime = direction.Length / maxSpeed;
                direction.Length = maxSpeed;

                return direction;


            }
        }

        public float ShootLimit
        {
            get
            {



               

                float maxTime = ship.WeaponShot.Time.Limit; // maximale Laufzeit eines Schusses
                float maxSpeed = ship.WeaponShot.Speed.Limit; // maximale Geschwindigkeit eines Schusses

                float shootLimit = maxTime * maxSpeed;

               

                return shootLimit;


            }
        }


        //Aktuelle Spieler im Universum ohne eigenen Spieler
        public List<Player> PlayerInUniverse
        {
            get
            {
                List<Player> PlayerInUniverseWithoutOwnPlayer = universeGroup.Players.List;

                PlayerInUniverseWithoutOwnPlayer.Remove(connector.Player);
                return PlayerInUniverseWithoutOwnPlayer;
            }
        }

        //Abholen einer Nachricht
        public List<FlattiverseMessage> Messages
        {
            get
            {
                messageLock.AcquireReaderLock(100);
                List<FlattiverseMessage> listCopy
                    = new List<FlattiverseMessage>(messages);
                messageLock.ReleaseReaderLock();
                return listCopy;
            }
        }

        //Nachrichten-Events Abfangen
        public delegate void FlattiverseChanged();
        public event FlattiverseChanged NewMessageEvent;
        public event FlattiverseChanged NewScanEvent;

        List<FlattiverseMessage> messages = new List<FlattiverseMessage>();
        ReaderWriterLock messageLock = new ReaderWriterLock();

        public void Connect()
        {
            connector = new Connector("kabeit00@hs-esslingen.de", "Karim1996");
        }

        //Listet Alle Universume auf.
        public void ListUniverses()
        {
            foreach (UniverseGroup ug in connector.UniverseGroups)
            {
                Console.WriteLine("{0} - {1} - {2} - max. {3} ships",
                    ug.Name,
                    ug.Description,
                    ug.Difficulty,
                    ug.MaxShipsPerPlayer
                );

                foreach (Universe universe in ug.Universes)
                    System.Console.WriteLine("     Universe: {0}", universe.Name);

                foreach (Team team in ug.Teams)
                    System.Console.WriteLine("     Team: {0}", team.Name);
            }
        }

        //Universum Beitreten
        public void Enter(string universeGroupName, string teamName)
        {
            universeGroup = connector.UniverseGroups[universeGroupName];
            Team team = universeGroup.Teams[teamName];

            universeGroup.Join("Karim", team);

            ship = universeGroup.RegisterShip("Flixbus", "Flixbus");

            Thread thread = new Thread(Run);
            thread.Name = "MainLoop";
            thread.Start();
        }
        //Nebenläufigkeit Hauptschleife
        private void Run()
        {
            ship.Continue();
            running = true;
            UniverseGroupFlowControl flowControl = universeGroup.GetNewFlowControl();

            while (running)
            {
                //Nachrichten lesen
                FlattiverseMessage message;
                bool messagesReceived = false;
                messageLock.AcquireWriterLock(100);

                while (connector.NextMessage(out message))
                {
                    messages.Add(message);
                    messagesReceived = true;
                }
                messageLock.ReleaseWriterLock();

                if (messagesReceived && NewMessageEvent != null)
                    NewMessageEvent();

                Scan();
                Move();

                flowControl.Commit();
                flowControl.Wait();

                //Schiff wird nochmal gespawnt bei tot
                if (!ship.IsAlive)
                    ship.Continue();
            }
            connector.Close();
        }

        public void Scan()
        {
            /*Angles: http://www2.hs-esslingen.de/~melcher/flattiverse/images/angles.svg*/
            
            scanAngle += ship.ScannerDegreePerScan; //Addiert den möglichen Scanbereich hinzu um den gesamten kreis abzudecken 

            List<Unit> scannedUnits = ship.Scan(scanAngle, ship.ScannerArea.Limit); 
            map.tick++;

            foreach (Unit u in scannedUnits)
            {
                Tag tag = new Tag(map.tick);
                u.Tag = tag;
            }

            map.RemoveOutdatedUnits();
            map.Insert(scannedUnits);

            if (NewScanEvent != null)
            {
                NewScanEvent();
            }
        }
        //Antrieb
        public void Impulse(int x, int y)
        {
            xImpulse += x;
            yImpulse += y;
        }
        //Bewegung
        public void Move()
        {
            if (xImpulse > 0) //Rechts
            {
                if (yImpulse > 0)  //Rechts + Oben (Diagonal)
                    direction = 315;
                else if (yImpulse < 0) // Rechts + Unten (Diagonal)
                    direction = 45;
                else
                    direction = 0; // Rechts Pur
            }
            else if (xImpulse < 0) //Links
            {
                if (yImpulse > 0) // Links + Oben (Diagonal)
                    direction = 225;
                else if (yImpulse < 0) //Links + Unten
                    direction = 135;
                else
                    direction = 180; //Links pur
            }
            else
            {
                if (yImpulse > 0 && xImpulse == 0) // Oben Pur
                    direction = 270;
                else if (yImpulse < 0 && xImpulse == 0) // Unten Pur
                {
                    direction = 90;
                }
            }
            if (xImpulse != 0 || yImpulse != 0)
            {
                Vector acceleration = Vector.FromAngleLength(direction, ship.EngineAcceleration.Limit);
                ship.Move(acceleration); //Beschleunigung des Schiffs im nächsten Schritt um den angegeben Vektor Wert.
            }

            //TODO: Bewegungsalgorithmus einbauen

            xImpulse = 0;
            yImpulse = 0;
        }

        //Universe Chat einbinden
        public void UniverseChat(String message)
        {
            universeGroup.Chat(message);
        }

        //Player Chat einbinden
        public void PlayerChat(String player,String message)
        {
            universeGroup.Players[player].Chat(message);
           
        }

      //  //Imagehandler nimmt bild von URL falls gebraucht
      //  public Image imageHandler(String imageName)
      //  {
      //      switch (imageName)
      //      {
      //          case "Space":
      //              HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://www2.hs-esslingen.de/~kabeit00/Daten/Flattiverse/images/spacebg.jpg");
      //              HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
      //              Stream stream = httpWebReponse.GetResponseStream();
      //              return Image.FromStream(stream);
      //              
      //          default:
      //              return null;
      //              
      //
      //
      //      }
      //  }

        //Shot 
        public void ShootAt(float x, float y)
        {
            Vector direction = Vector.FromXY(x, y);

            float maxTime = ship.WeaponShot.Time.Limit; // maximale Laufzeit eines Schusses
            float maxSpeed = ship.WeaponShot.Speed.Limit; // maximale Geschwindigkeit eines Schusses

            float shootLimit = maxTime * maxSpeed;


            float needTime = direction.Length / maxSpeed;
            direction.Length = maxSpeed;

            ship.Shoot(direction, (int)needTime);

        }

        

        





        public void Disconnect()
        {
            running = false;
            
        }

    }
}