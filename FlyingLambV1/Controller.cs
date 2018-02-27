using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flattiverse;
using System.Threading;




namespace FlyingLambV1
{
    public class Controller
    {
        Connector connector;
        UniverseGroup universeGroup;
        Ship ship;
        Boolean running;
        View view;


        int xImpulse, yImpulse, direction; //Antrieb

        Map map = new Map();

        public Boolean ShipReady
        {
            get {
                /*
                if (ship != null)
                    return true;*/

               return (ship != null);
            }

            //set { } //Aktuell Read Only Property
        }

        public List<Unit> Units
        {
            get
            {
                return map.Units;
            }
        }

        public float ShipRadius
        {
            get
            {
                
                return ship.Radius;
            }
           
        }

        public float ShipEnergyMax
        {
            get
            {
                return ship.EnergyMax;
            }
        }
        public float ShipEnergyLive
        {
            get
            {
                return ship.Energy;
            }
        }

        

        

        //Nachrichten-Events Abfangen
        public delegate void FlattiverseChanged();
        public event FlattiverseChanged NewMessageEvent;
        public event FlattiverseChanged NewScanEvent; //[K]
       

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

            ship = universeGroup.RegisterShip("FlyingLamb", "Flying Lamb");

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
                Move(); //Antrieb [K]
                
                flowControl.Commit();
                flowControl.Wait();

                if (!ship.IsAlive)
                    ship.Continue();
            }
            connector.Close();
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

        public void Scan()
        {
            float scanAngle = 0;
            

            scanAngle += ship.ScannerDegreePerScan;

            ship.Scan(scanAngle, 300); //Todo: 300 durch Maximalwert des Scans ersetzen.

            List<Unit> scannedUnits = ship.Scan(scanAngle, 300); //vllt. ship.ScannerArea.Limit
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

            if (NewMessageEvent != null)
            {
                NewMessageEvent();
            }
        }
        //Antrieb
        public void Impulse(int x, int y)
        {
            xImpulse += x;
            yImpulse += y;
        }

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
                //[K]
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
                else if(yImpulse < 0 && xImpulse == 0) // Unten Pur
                {
                    direction = 90;
                }
             }

            if (xImpulse != 0 || yImpulse != 0)
            {
                Vector acceleration =
                    Vector.FromAngleLength(direction,
                    ship.EngineAcceleration.Limit);
                ship.Move(acceleration);
            }

            xImpulse = 0;
            yImpulse = 0;
        }

        
       






        public void Disconnect()
        {
            running = false;
            
        }

    }
}