using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAQueue
{
    /// <summary>
    /// Generates a stream of randomly arriving customers.
    /// </summary>
    public class CustomerGenerator
    {
        private int[] customerArrivals;
        private readonly int minDuration;
        private readonly int maxDuration;
        private Random rand;

        /// <summary>
        /// Creates the generator and initializes the random number generator for populating the customer queues.
        /// </summary>
        /// <param name="minDuration">The minimum amount of time that it will take to process a customer's request.</param>
        /// <param name="maxDuration">The maximum amount of time that it will take to process a customer's request.</param>
        /// <param name="avgPerSlot">The average number of customers that will arrive per time slot.</param>
        /// <param name="totalTime">Total number of time slots for which customers are to be generated.</param>
        /// <param name="seed">If present, the this value is passed as the seed to the random number generator.  If the seed is negative, no seed is passed.</param>
        public CustomerGenerator(int minDuration, int maxDuration, double avgPerSlot, int totalTime, int seed = -1)//changed seed to 1 (orig -1)
        {
            if (seed < 0)
                rand = new Random();
            else
                rand = new Random(seed);

            customerArrivals = new int[totalTime];
            this.minDuration = minDuration;
            this.maxDuration = maxDuration;
            initializeArrivals(avgPerSlot, totalTime);
        }

        /// <summary>
        /// Initializes an array containing the number of customers that will be generated for each time slot.
        /// </summary>
        /// <param name="avgPerSlot">The average number of customers that will arrive per time slot.</param>
        /// <param name="slots">Total number of time slots for which customers are to be generated.</param>
        private void initializeArrivals(double avgPerSlot, int slots)
        {
            for (int i = 0; i < slots * avgPerSlot; i++)
            {
                int slot = rand.Next(slots);
                customerArrivals[slot]++;
            }
        }

        /// <summary>
        /// Returns a Queue of customers that are generated for the given time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot for which the customers are to be generated.</param>
        /// <returns>The Queue of customers.  This queue may be empty.</returns>
        public Queue<Customer> GetCustomers(int timeSlot)
        {
            Queue<Customer> customers = new Queue<Customer>();

            // Make certain we haven't gone beyond the time; this could happen whlle
            // the queue of waiting customers is emptied
            if (timeSlot < customerArrivals.Length)
            {
                // Our pre-generated array contains the number of customers for any given timeslot
                int numArrivals = customerArrivals[timeSlot];

                while (numArrivals > 0)
                {
                    int duration = rand.Next(maxDuration - minDuration + 1) + minDuration;
                    Customer customer = new Customer(timeSlot, duration);
                    customers.Enqueue(customer);
                    numArrivals--;
                }
            }
            return customers;
        }
    }

}