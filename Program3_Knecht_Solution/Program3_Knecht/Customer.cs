using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAQueue
{

    /// <summary>
    /// Keeps track of the arrival time, the time the service of the customer begins and the expected duration of the transaction, i.e. how long it will take to perform the customer's request.
    /// </summary>
    public class Customer
    {
        // The time (i.e. minutes from the start of the simulation) that the customer arrives
        public readonly int ArrivalTime;

        // The number of minutes that it will take to service this customer
        public readonly int TransactionDuration;

        // The time (minutes from the start) that the customer actually gets to see a Teller; depending on your
        // implementation, this may not be needed.
        public int ServiceTime { get; set; }

        // The constructor is called from the generator; you never need to create a customer
        public Customer(int arrival, int duration)
        {
            ArrivalTime = arrival;
            TransactionDuration = duration;
        }

        //public Queue<Customer> FindShortestLine(Queue<Customer>[] line, List<Teller> counter)
        //{
        //    for (int i = 0; i < counter.Count; i++)
        //    {
        //        int people = line[i].Count;
        //        if (line[i].Count == 0)
        //        {
        //            return line[i];
        //        }
        //    }
        //    return null;
        //}

        // For debugging purposes
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Customer [arrivalTime=");
            builder.Append(ArrivalTime);
            builder.Append(", transactionDuration=");
            builder.Append(TransactionDuration);
            builder.Append(", serviceTime=");
            builder.Append(ServiceTime);
            builder.Append("]");
            return builder.ToString();
        }
    }

}
