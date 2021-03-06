//michael shachor 206197733
//Shimon Mizrahi 203375563

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_project_targil_2
{
    class Program
    {
        static Random rand = new Random(DateTime.Now.Millisecond);//random.
        //CreateRandomRequest function: the function create a random request and return them.
        private static GuestRequest CreateRandomRequest()
        {
            GuestRequest gs = new GuestRequest();
            
            DateTime firstDate = new DateTime(GuestRequest.YEAR, 1, 1);//create first date.
            DateTime lastDate = new DateTime(GuestRequest.YEAR, 12, 31);//create last date.
            int numOfDays;//sum of date.
            int dateRange;//range request.

            do
            {
            numOfDays = rand.Next((lastDate - firstDate).Days);
            gs.EntryDate = firstDate.AddDays(numOfDays);

            dateRange = (lastDate - gs.EntryDate).Days;

            } while (dateRange<2);//in case that range lowwer than 2 days like end year for example.


            numOfDays = rand.Next(2,Math.Min(10,(lastDate - gs.EntryDate).Days));
            gs.ReleaseDate = gs.EntryDate.AddDays(numOfDays);


            //Fill randomally the Entry and Release dates of gs.
            return gs;
        }

        
        static void Main(string[] args)
        {
            List<Host> lsHosts;
            lsHosts = new List<Host>()
             {
             new Host(1, rand.Next(1,5)),
             new Host(2, rand.Next(1,5)),
             new Host(3, rand.Next(1,5)),
             new Host(4, rand.Next(1,5)),
             new Host(5, rand.Next(1,5))
             };
            for (int i = 0; i < 100; i++)
            {
                foreach (var host in lsHosts)
                {

                    GuestRequest gs1 = CreateRandomRequest();
                    GuestRequest gs2 = CreateRandomRequest();
                    GuestRequest gs3 = CreateRandomRequest();
                    switch (rand.Next(1, 4))
                    {
                        case 1:
                            host.AssignRequests(gs1);
                            break;
                        case 2:
                            host.AssignRequests(gs1, gs2);
                            break;
                        case 3:
                            host.AssignRequests(gs1, gs2, gs3);
                            break;
                        default:
                            break;
                    }
                }
            }
            //Create dictionary for all units <unitkey, occupancy_percentage>
            Dictionary<long, float> dict = new Dictionary<long, float>();
            foreach (var host in lsHosts)
            {
                //test Host IEnuramble is ok
                foreach (HostingUnit unit in host)
                {
                    dict[unit.HostingUnitKey] = unit.GetAnnualBusyPrecentege();
                }
            }
            //get max value in dictionary
            float maxVal = dict.Values.Max();
            //get max value key name in dictionary
            long maxKey =
           dict.FirstOrDefault(x => x.Value == dict.Values.Max()).Key;
            //find the Host that its unit has the maximum occupancy percentage
            foreach (var host in lsHosts)
            {
                //test indexer of Host
                for (int i = 0; i < host.HostingUnitCollection.Count; i++)
                {
                    if (host[i].HostingUnitKey == maxKey)
                    {
                        //sort this host by occupancy of its units
                        host.SortUnits();
                        //print this host detailes
                        Console.WriteLine("**** Details of the Host with the most occupied unit:\n");
        
                        Console.WriteLine(host);
                        break;
                    }
                }
            }
        }
    }
}



