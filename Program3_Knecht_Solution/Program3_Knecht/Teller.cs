using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAQueue
{
    public class Teller
    {
        // our queue of customers
        private Queue<Customer> custQ { get; set; }
        // the tick of the clock when the teller becomes free
        private int nextFree = 0;

        // the amount of time the teller is idle
        public int IdleTime { get; private set; }

        // the number of customers served
        public int CustomersServed { get; private set; }

        // the longest time that one of this teller's customers has waited
        public int MaxWaitTime { get; private set; }

        // the total amount of time that all of this teller's customers have waited
        public int TotalWaitTime { get; private set; }

        List<int> WaitList = new List<int>();
        
        // Constructor accepts the queue of customers from which to pull the next customer
        // There are other ways to do this, but this makes part B less painful
        public Teller(Queue<Customer> q)
        {
            custQ = q;
        }
        public Teller() { }//TEST CONSTRUCTOR

        public int GetMaxWaitTime()
        {
            if (WaitList.Count == 0)
            {
                MaxWaitTime = 0;
            }
            else
            {
                MaxWaitTime = WaitList.Max();
            }
            return MaxWaitTime;
        }
        public double GetTellerAvgMinsIdle()
        {
            double answer = 0;
            if (IdleTime == 0)
            {
                return 0;
            }
            answer = CustomersServed / IdleTime;
            return answer;
        }
        public double GetCustomerAvgWaitTime()
        {
            double answer = 0;
            if (TotalWaitTime == 0)
            {
                answer = 0;
            }
            else if (CustomersServed == 0)
            {
                answer = 0;
            }
            else
            {
                answer = TotalWaitTime / CustomersServed;
            }
            return answer;
        }
        /// <summary>
        /// Begins helping a customer if the teller is not busy AND a customer is waiting
        /// </summary>
        /// <param name="now">The time on the bank's clock, in minutes from the start, i.e. timeslot.</param>
        public void ProcessNextCustomer(int now)
        {
            // busy?
            if (now >= nextFree)
            {
                if (custQ.Count != 0)//if theres a customer in line
                {
                    Customer c = custQ.Dequeue();
                    CustomersServed++;
                    c.ServiceTime = now;
                    int waitTime = c.ServiceTime - c.ArrivalTime;
                    WaitList.Add(waitTime);
                    TotalWaitTime += waitTime;
                    nextFree = now + c.TransactionDuration;
                }
                else
                {
                    IdleTime++;
                }

            }
        }
    }
}

