using System;
using System.Net.NetworkInformation;


namespace MrDAL.Domains.Shared.DataSync.Common;

public static class NetworkConnectionHandler
{
    public static bool IsInternetAvailable()
    {
        try
        {
            using (var ping = new Ping())
            {
                // You can change this to a known reliable IP address, like a public DNS server
                // or the API host you plan to call.
                string targetHost = "www.google.com";

                // Ping the target host with a timeout of 1000 milliseconds (1 second).
                PingReply reply = ping.Send(targetHost, 1000);

                // Check if the ping was successful and the status is "Success."
                if (reply.Status == IPStatus.Success)
                {
                    return true; // Internet connection is available.
                }
            }
        }
        catch (Exception)
        {
            //throw new Exception("Unable to Sync Data. Please check your internet connection!!!");
            return false;
            // An exception occurred while attempting to check the internet connection.
            // You can log or handle the exception as needed.
        }

        return false; // Internet connection is not available.
    }

}