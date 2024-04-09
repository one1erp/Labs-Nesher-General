
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    namespace Common
    {

        //Ashi Todo
        public static class Logger
        {
            public static void WriteLogFile(Exception exception)
            {
                try
                {

                    var fullPath = GetFullPath("LogPath", "Log");


                    using (FileStream file = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                    {
                        var streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine(DateTime.Now);
                        streamWriter.WriteLine("Message");
                        streamWriter.WriteLine(exception.Message);
                        streamWriter.WriteLine("InnerException");
                        streamWriter.WriteLine(exception.InnerException);
                        streamWriter.WriteLine("StackTrace");
                        streamWriter.WriteLine(exception.StackTrace);
                        if (exception.InnerException != null)
                        {
                            streamWriter.WriteLine("InnerException.Message");
                            streamWriter.WriteLine(exception.InnerException.Message);
                        }
                        streamWriter.WriteLine();
                        streamWriter.WriteLine("///////////////////////////////////////////");
                        streamWriter.WriteLine();
                        streamWriter.Close();
                    }
                }
                catch
                {
                }


            }

            public static void WriteLogFile(string query)
            {
                try
                {

                    var fullPath = GetFullPath("LogPath", "Log");

                    using (FileStream file = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                    {
                        var streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));

                        streamWriter.WriteLine(query);
                        streamWriter.WriteLine();
                        streamWriter.Close();
                    }
                }
                catch
                {
                }


            }

            private static string GetFullPath(string keyName, string fileName)
            {


                try
                {

                    string assemblyPath = Assembly.GetExecutingAssembly().Location;
                    ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                    map.ExeConfigFilename = assemblyPath + ".config";
                    Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                    var appSettings = cfg.AppSettings;

          
                    string path = Path.Combine(ConfigurationManager.AppSettings[keyName], Environment.UserName.MakeSafeFilename('_'));
                    //string path = Path.Combine(appSettings.Settings[keyName].Value, Environment.UserName.MakeSafeFilename('_'));
                    string logFile = fileName + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fullPath = Path.Combine(path, logFile);
                    return fullPath;
                }
                catch (Exception e)
                {

                    return null;
                }
            }
            public static void WriteLogFile(string errorMessage, bool isFromXmlProcessor)
            {
                try
                {
                    var fullPath = GetFullPath("LogPath", "Log");

                    using (FileStream file = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                    {
                        var streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine(DateTime.Now);
                        if (isFromXmlProcessor)
                        {
                            streamWriter.WriteLine("Xml Processor response retrieve error : ");

                        }
                        streamWriter.WriteLine("Error Message : ");
                        streamWriter.WriteLine(errorMessage);
                        streamWriter.WriteLine();
                        streamWriter.WriteLine("///////////////////////////////////////////");
                        streamWriter.WriteLine();
                        streamWriter.Close();
                    }
                }
                catch
                {
                }


            }

            public static void WriteQueries(string query)
            {
                try
                {

                    var fullPath = GetFullPath("QueriesPath", "Queries");
                    //     var fullPath = @"C:\Queries\logq.txt";// GetFullPath("LogPath", "Log");

                    using (FileStream file = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                    {
                        var streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));

                        streamWriter.WriteLine(query);
                        streamWriter.WriteLine();
                        streamWriter.Close();
                    }
                }
                catch
                {
                }


            }

            public static void MyLog(string sss)
            {
                try
                {



                    using (FileStream file = new FileStream("C:\\log.txt", FileMode.Append, FileAccess.Write))
                    {
                        var streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));

                        streamWriter.WriteLine(sss);
                        streamWriter.WriteLine();
                        streamWriter.Close();
                    }
                }
                catch
                {
                }


            }

            public static void WriteEventVieweer()
            {
                string sSource;
                string sLog;
                string sEvent;

                sSource = "Patholab";
                sLog = "Application";
                sEvent = "Sample Event";

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                EventLog.WriteEntry(sSource, sEvent);
                EventLog.WriteEntry(sSource, sEvent,
                    EventLogEntryType.Warning, 234);


            }



            public static void WriteXml(MSXML.DOMDocument objDoc, bool p)
            {
                var fullPath = GetFullPath("XmlPath", "Queries");

                using (FileStream file = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                {
                    var streamWriter = new StreamWriter(file);
                    streamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));
                    streamWriter.WriteLine();
                    streamWriter.Close();
                }
            }
        }
    }


    //public static class Logger
    //{
    //    public static void WriteLogFile(Exception exception)
    //    {
    //        try
    //        {
    //            using (FileStream file = new FileStream(@"C:\Log\LOG.txt", FileMode.Append, FileAccess.Write))
    //            {
    //                var streamWriter = new StreamWriter(file);
    //                streamWriter.WriteLine(DateTime.Now);
    //                streamWriter.WriteLine("Message");
    //                streamWriter.WriteLine(exception.Message);
    //                streamWriter.WriteLine("InnerException");
    //                streamWriter.WriteLine(exception.InnerException);
    //                streamWriter.WriteLine("StackTrace");
    //                streamWriter.WriteLine(exception.StackTrace);
    //                if (exception.InnerException != null)
    //                {
    //                    streamWriter.WriteLine("InnerException.Message");
    //                    streamWriter.WriteLine(exception.InnerException.Message);
    //                }
    //                streamWriter.WriteLine();
    //                streamWriter.WriteLine("///////////////////////////////////////////");
    //                streamWriter.WriteLine();
    //                streamWriter.Close();
    //            }
    //        }
    //        catch
    //        {
    //        }


    //    }


    //    public static void WriteLogFile(string errorMessage, bool isFromXmlProcessor)
    //    {
    //        try
    //        {
    //            using (FileStream file = new FileStream(@"C:\Log.txt", FileMode.Append, FileAccess.Write))
    //            {
    //                var streamWriter = new StreamWriter(file);
    //                streamWriter.WriteLine(DateTime.Now);
    //                if (isFromXmlProcessor)
    //                {
    //                    streamWriter.WriteLine("Xml Processor response retrieve error : ");

    //                }
    //                streamWriter.WriteLine("Error Message : ");
    //                streamWriter.WriteLine(errorMessage);
    //                streamWriter.WriteLine();
    //                streamWriter.WriteLine("///////////////////////////////////////////");
    //                streamWriter.WriteLine();
    //                streamWriter.Close();
    //            }
    //        }
    //        catch
    //        {
    //        }


    //    }

    //    //public static void vv<T>(IQueryable<T> query)
    //    //{

    //    //    try
    //    //    {

    //    //        var q = (System.Data.Objects.ObjectQuery)query;
    //    //        var sql = q.ToTraceString();
    //    //        using (FileStream file = new FileStream(@"C:\Query.txt", FileMode.Append, FileAccess.Write))
    //    //        {
    //    //            var streamWriter = new StreamWriter(file);
    //    //            streamWriter.WriteLine(DateTime.Now);


    //    //            streamWriter.WriteLine(sql);
    //    //            streamWriter.WriteLine("///////////////////////////////////////////");
    //    //            streamWriter.WriteLine();
    //    //            streamWriter.Close();
    //    //        }
    //    //    }
    //    //    catch
    //    //    {
    //    //    }



    //    //}
    //}

