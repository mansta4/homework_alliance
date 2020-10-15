using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeworkApplication
{
    class Program
    {
        private const string prefix = "..\\..\\..\\";
        private const string FILE_STANDARD_CONFIG = "Base_Config.txt";
        private static List<DataLayer> dataLayers;
        private static string[] configIds = { "ordersPerHour", "orderLinesPerOrder", "inboundStrategy", "powerSupply", "numberOfAisles", "resultStartTime", "resultInterval" };
        private static char[] separators = { ':', '/' };

        static void Main(string[] args)
        {
            Console.WriteLine("Enter commands:\n read - Read config text files" +
                "\n print - Show generated data layers\n exit - terminate program\n");
            string input = String.Empty;
            while (input != "exit")                                                  // The condition that leaves the loop;
            {
                input = Console.ReadLine();
                if (input == "read")                                                // Read data from text files;
                {
                    dataLayers = new List<DataLayer>();
                    DirectoryInfo dinfo = new DirectoryInfo("..\\..\\..\\");
                    FileInfo[] files = dinfo.GetFiles("*.txt");
                    if (files.Any(x => x.Name == FILE_STANDARD_CONFIG))             // Reading the standard config file; also there could be a list of files that we must read first;
                    {
                        ReadConfigurationFile(FILE_STANDARD_CONFIG);
                    }
                    else                                                            // Write to the console if the 'FILE_STANDARD_CONFIG' is missing;
                    {
                        Console.WriteLine("Missing standard config file");
                    }
                    foreach (FileInfo f in files)                                   // Read all files that were found in the program directory;
                    {
                        if (f.Name != FILE_STANDARD_CONFIG)
                        {
                            ReadConfigurationFile(f.Name);
                        }
                    }
                    Console.WriteLine();
                }
                else if (input == "print")                                          // Write generated data to the console; 
                {
                    PrintLayers(dataLayers);
                }
                else if (input != "exit")                                                               // If input command is unknown;
                {
                    Console.WriteLine("Unknown command\n");
                }
            }
        }

        /// <summary>
        /// Read specified file and save it to the list.
        /// </summary>
        /// <param name="file">File name.</param>
        static private void ReadConfigurationFile(String file)
        {
            Console.WriteLine(string.Format("Reading file: {0}", file));
            using (var reader = new StreamReader(prefix + file))
            {
                string line = String.Empty;
                string configId = String.Empty;
                DataLayer data;
                if (dataLayers.Count == 0)
                {
                    data = new DataLayer();
                }
                else
                {
                    data = CopyDataLayer(dataLayers[dataLayers.Count - 1]);
                }
                while ((line = reader.ReadLine()) != null)
                {
                    string trimmed = String.Concat(line.Where(c => !Char.IsWhiteSpace(c)));

                    if ((configId = ContainsAny(trimmed, configIds)) != String.Empty)
                    {
                        string[] value = trimmed.Split(separators);
                        switch (configId)
                        {
                            case "ordersPerHour":
                                data.SetOrdersPerHour(int.Parse(value[1]));
                                break;

                            case "orderLinesPerOrder":
                                data.SetOrderLinesPerOrder(int.Parse(value[1]));
                                break;

                            case "inboundStrategy":
                                if (value[1] == InboundStrategy.optimized.ToString())
                                    data.SetInboundStrategy(InboundStrategy.optimized);
                                else if (value[1] == InboundStrategy.random.ToString())
                                    data.SetInboundStrategy(InboundStrategy.random);
                                else
                                    data.SetInboundStrategy(InboundStrategy.NA);
                                break;

                            case "powerSupply":
                                if (value[1] == PowerSupply.normal.ToString())
                                    data.SetPowerSupply(PowerSupply.normal);
                                else if (value[1] == PowerSupply.big.ToString())
                                    data.SetPowerSupply(PowerSupply.big);
                                else
                                    data.SetPowerSupply(PowerSupply.NA);
                                break;

                            case "numberOfAisles":
                                data.SetNumberOfAisles(int.Parse(value[1]));
                                break;

                            case "resultStartTime":
                                data.SetResultStartTime(string.Format("{0}:{1}:{2}", value[1], value[2], value[3]));
                                break;

                            case "resultInterval":
                                data.SetResultInterval(int.Parse(value[1]));
                                break;
                        }
                    }
                }
                dataLayers.Add(data);
                //TODO check if everything is alright
            }
            Verificate(dataLayers);
            Console.WriteLine("Data layer for file: {0} was generated", file);
        }

        /// <summary>
        /// Check if the line contains any of the words from the array
        /// </summary>
        /// <param name="line">The string that is currently being processed.</param>
        /// <param name="words">An array of searched words.</param>
        /// <returns></returns>
        private static string ContainsAny(string line, params string[] words)
        {
            foreach (string word in words)
            {
                if (line.Contains(word))
                {
                    return word;
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Create a copy of the list element
        /// </summary>
        /// <param name="dataLayer">List element</param>
        /// <returns></returns>
        private static DataLayer CopyDataLayer(DataLayer dataLayer)
        {
            DataLayer data = new DataLayer(dataLayer.GetOrdersPerHour(), dataLayer.GetOrderLinesPerOrder(), dataLayer.GetInboundStrategy(),
                dataLayer.GetPowerSupply(), dataLayer.GetNumberOfAisles(), dataLayer.GetResultStartTime(), dataLayer.GetResultInterval());
            return data;
        }

        /// <summary>
        /// Function for values verification
        /// </summary>
        /// <param name="dataLayers"></param>
        private static void Verificate(List<DataLayer> dataLayers)
        {
            foreach (DataLayer d in dataLayers)
            {
                if (d.GetNumberOfAisles() >= 5 && d.GetPowerSupply() == PowerSupply.normal)
                {
                    Console.WriteLine("Error: if the number of aisles is set to 5 or higher, 'power supply' must be set to 'big'");
                }
                else if (d.GetNumberOfAisles() < 5 && d.GetPowerSupply() == PowerSupply.big)
                {
                    Console.WriteLine("Error: if the number of aisles is set to 4 or lower, 'power supply' must be set to 'normal'");
                }
            }
        }

        /// <summary>
        /// Print generated data layers to the console
        /// </summary>
        /// <param name="dataLayers">List of data layers</param>
        private static void PrintLayers(List<DataLayer> dataLayers)
        {
            int n = 0;
            Console.WriteLine("Displaying generated layer data");
            foreach (DataLayer d in dataLayers)
            {
                string ordersPerHour = d.GetOrdersPerHour() == 0 ? "Error" : d.GetOrdersPerHour().ToString();
                string orderLinesPerOrder = d.GetOrderLinesPerOrder() == 0 ? "Error" : d.GetOrderLinesPerOrder().ToString();
                string inboundStrategy = d.GetInboundStrategy() == InboundStrategy.NA ? "Error" : d.GetInboundStrategy().ToString();
                string powerSupply = d.GetPowerSupply() == 0 ? "Error" : d.GetPowerSupply().ToString();
                string numberOfAisles = d.GetNumberOfAisles() == 0 ? "Error" : d.GetNumberOfAisles().ToString();
                string resultStartTime = d.GetResultStartTime() == String.Empty ? "Error" : d.GetResultStartTime().ToString();
                string resultInterval = d.GetResultInterval() == 0 ? "Error" : d.GetResultInterval().ToString();

                Console.WriteLine(string.Format("Layer {0}", n));
                Console.WriteLine(string.Format("| {0,-6} | {1,-6} | {2,-6} | {3,-6} | {4,-6} | {5,-6} | {6,-6} |",
                    ordersPerHour, orderLinesPerOrder, inboundStrategy, powerSupply, numberOfAisles, resultStartTime, resultInterval));
                n++;
            }
            Console.WriteLine();
        }
    }
}
