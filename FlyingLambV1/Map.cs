using Flattiverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlyingLambV1
{
    class Map
    {

        
       
        Dictionary<string, Unit> mapUnits = new Dictionary<string, Unit>();
        List<string> deletedUnits = new List<string>(); //[K]
        ReaderWriterLock listLock = new ReaderWriterLock();
        public int tick;
        
     

        public void Insert(List<Unit> units)
        {
          
            listLock.AcquireWriterLock(100);

           


            foreach (Unit u in units)
            {

                mapUnits[u.Name] = u;
                
                

            }


            
            listLock.ReleaseWriterLock();
        }

        public List<Unit> Units
        {
            get
            {
                listLock.AcquireReaderLock(100);
                List<Unit> units = new List<Unit>(mapUnits.Values);
                listLock.ReleaseReaderLock();
                return units;
            }
        }

        internal void RemoveOutdatedUnits()
        {
            List<string> toDelete = new List<string>();
            foreach(string name in mapUnits.Keys)
            {
                Unit u = mapUnits[name];
                Tag tag = u.Tag  as Tag;
                if (tag.ScannedAt < tick - 5)
                    toDelete.Add(name);
            }

            foreach (string n in toDelete)
                mapUnits.Remove(n);
        }
    }
}
