﻿using System;
using System.IO;

namespace MeleeMover
{
    public class Logger
    {
        static string filePath = $"{MeleeMover.ModDirectory}/MeleeMover.log";
        public static void LogError(Exception ex)
        {
            if (MeleeMover.DebugLevel >= 1)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    var prefix = "[SustainableEvasion @ " + DateTime.Now.ToString() + "]";
                    writer.WriteLine("Message: " + ex.Message + "<br/>" + Environment.NewLine + "StackTrace: " + ex.StackTrace + "" + Environment.NewLine);
                    writer.WriteLine("----------------------------------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        public static void LogLine(String line)
        {
            if (MeleeMover.DebugLevel >= 2)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    var prefix = "[MeleeMover @ " + DateTime.Now.ToString() + "]";
                    writer.WriteLine(prefix + line);
                }
            }
        }
    }
}