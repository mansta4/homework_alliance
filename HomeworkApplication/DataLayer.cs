using System;
using System.Collections.Generic;
using System.Text;

namespace HomeworkApplication
{
    enum PowerSupply { NA, normal, big }
    enum InboundStrategy { NA, random, optimized }

    class DataLayer
    {
        private int ordersPerHour = 0;
        private int orderLinesPerOrder = 0;
        private InboundStrategy inboundStrategy = InboundStrategy.NA;
        private PowerSupply powerSupply = PowerSupply.NA;
        private int numberOfAisles = 0;
        private String resultStartTime = String.Empty;
        private int resultInterval = 0;

        // Constructors;
        public DataLayer() { }
        public DataLayer(int ordersPerHour, int orderLinesPerOrder, InboundStrategy inboundStrategy, PowerSupply powerSuppy, int numberOfAisles, String resultStartTime, int resultInterval)
        {
            this.ordersPerHour = ordersPerHour;
            this.orderLinesPerOrder = orderLinesPerOrder;
            this.inboundStrategy = inboundStrategy;
            this.powerSupply = powerSuppy;
            this.numberOfAisles = numberOfAisles;
            this.resultStartTime = resultStartTime;
            this.resultInterval = resultInterval;
        }

        // Set, get functions
        public int GetOrdersPerHour()
        {
            return this.ordersPerHour;
        }
        public void SetOrdersPerHour(int value)
        {
            this.ordersPerHour = value;
        }
        public int GetOrderLinesPerOrder()
        {
            return this.orderLinesPerOrder;
        }
        public void SetOrderLinesPerOrder(int value)
        {
            this.orderLinesPerOrder = value;
        }
        public InboundStrategy GetInboundStrategy()
        {
            return inboundStrategy;
        }
        public void SetInboundStrategy(InboundStrategy value)
        {
            inboundStrategy = value;
        }
        public PowerSupply GetPowerSupply()
        {
            return powerSupply;
        }
        public void SetPowerSupply(PowerSupply value)
        {
            powerSupply = value;
        }
        public int GetNumberOfAisles()
        {
            return this.numberOfAisles;
        }
        public void SetNumberOfAisles(int value)
        {
            this.numberOfAisles = value;
        }
        public String GetResultStartTime()
        {
            return this.resultStartTime;
        }
        public void SetResultStartTime(String date)
        {
            this.resultStartTime = date;
        }
        public void SetResultInterval(int value)
        {
            this.resultInterval = value;
        }
        public int GetResultInterval()
        {
            return resultInterval;
        }
    }
}
