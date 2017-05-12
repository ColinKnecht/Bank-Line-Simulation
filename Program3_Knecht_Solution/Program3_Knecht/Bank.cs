using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAQueue
{
    class Bank
    {
        public static void Main(string[] args)
        {
            //Queue<int>[] OdafimColors = new Queue<int>[10];
            //for (int i = 0; i < OdafimColors.Lenght; i++)
            //    OdafimColors[i] = new Queue<int>();

            // shortest time to service a customer; must be greater than zero
            int MINIMUM_DURATION = 1;

            // longest time to service a customer; must be greater than the minimum duration
            int MAXIMUM_DURATION = 5;

            // average customers arriving per given minute.  This would mean three customers every four minutes.
            double CUST_PER_MINUTE = 3;//orig .75

            // how long the simulation represents; 120 would equal two hours
            int SIMULATION_DURATION = 120;

            // The number of tellers for this run of the simulation
             int NUM_TELLERS = 12;//orig 15

            // Create the customer generator
            CustomerGenerator frontdoor = new CustomerGenerator(MINIMUM_DURATION, MAXIMUM_DURATION, CUST_PER_MINUTE, SIMULATION_DURATION);

            Queue<Customer>[] separateLines = new Queue<Customer>[NUM_TELLERS];//for a grade of B
            for (int i = 0; i < separateLines.Length; i++)
            {
                separateLines[i] = new Queue<Customer>();
            }
            Queue<Customer> bankLine = new Queue<Customer>();

            // TODO -- Create a List (not a Queue) of Teller objects; the number of
            // tellers in the list is what you should modify to determine how
            // many tellers are necessary.  You'll need to populate the list, of course.

            List<Teller> tellerList = new List<Teller>();
            for (int i=0; i < NUM_TELLERS; i++)//INITIALIZES TELLERS
            {
                Teller t = new Teller(bankLine);
                //Teller t = new Teller(separateLines[i]);
                tellerList.Add(t);
            }

            // Continue looping until we have completed the duration of our simulation
            // timeslot represents the number of minutes since the start of our simulation
            // TODO -- modify the loop so that it continues to run until there is no one left in line

            //for (int timeSlot = 0; timeSlot < SIMULATION_DURATION; timeSlot++)//LUNCH TIME I.E.=TOTAL SIMULATION TIME(120 MINS)
            int timeSlot = 0;
            while (timeSlot < SIMULATION_DURATION)
            {
                // get the queue of arriving customers from the frontdoor
                Queue<Customer> arrivals = frontdoor.GetCustomers(timeSlot);
                // TODO -- in a loop, move all of the customers from the arrival queue to the Queue of waiting customers
                ///        be careful that you do not try to pull too many customers from the Queue
                //for (int i = 0; i < arrivals.Count; i++)
                while (arrivals.Count != 0)
                {
                    Customer c = arrivals.Dequeue();
                    bankLine.Enqueue(c); 

                    //c.FindShortestLine(separateLines, tellerList);//i tried this also for a B; my logic was isnt it the customers job to find the shortest line

                }
                // TODO -- for every teller (think foreach), call ProcessNextCustomer(timeSlot)
                foreach (Teller t in tellerList)
                {
                    t.ProcessNextCustomer(timeSlot);
                }
                timeSlot++;
            }//end simulation loop
            if (bankLine.Count != 0)
            {
                timeSlot++;
                foreach (Teller t in tellerList)
                {
                    t.ProcessNextCustomer(timeSlot);
                }
            }
            // TODO -- print the statistics
            // You'll need to walk through your list of Tellers figuring out longest wait time, total wait time, etc.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("==============SIMULATION SETTINGS==================");
            Console.WriteLine("Minimum Duration: " + MINIMUM_DURATION);
            Console.WriteLine("Maximum Duration: " + MAXIMUM_DURATION);
            Console.WriteLine("Customers Per Minute : " + CUST_PER_MINUTE);
            Console.WriteLine("Simulation Duration: " + SIMULATION_DURATION);
            Console.WriteLine("Number of Tellers : " + NUM_TELLERS);
            Console.WriteLine("=====================================================");
            Console.ResetColor();


            int idleTimeSum = 0;
            foreach (Teller t in tellerList)//gets The Average of Minutes teller was idle
            {
                idleTimeSum += t.IdleTime;
            }
            double avgIdleTellerMins = idleTimeSum / NUM_TELLERS;
            int tellerNum = 0;
            List<int> MaxTellerIdleTime = new List<int>();
            int allCustomersServed = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (Teller t in tellerList)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Teller " + tellerNum );
                Console.WriteLine("Customer Max Wait Time: " + t.GetMaxWaitTime() + " mins");
                Console.WriteLine("Customer Average Wait Time: " + t.GetCustomerAvgWaitTime() + " mins");
                MaxTellerIdleTime.Add(t.IdleTime);
                allCustomersServed += t.CustomersServed;
                Console.WriteLine("Idle Time: " + t.IdleTime);
                tellerNum++;
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================SIMULATION TOTALS=========================");
            Console.WriteLine("Average Minutes Teller Was Idle: " + avgIdleTellerMins);
            Console.WriteLine("Max Minutes Teller Was Idle: " + MaxTellerIdleTime.Max());

            Console.WriteLine("Total Customers Served: " + allCustomersServed + " customers");
            Console.WriteLine("Total Time Taken: " + timeSlot + " minutes");
            Console.WriteLine("==============================================================");
            Console.ResetColor();


            Console.ReadLine();
        }
    }
}
