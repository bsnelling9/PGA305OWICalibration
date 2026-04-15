using Ivi.Visa.FormattedIO;
using NationalInstruments.Visa;
using System;
using System.Linq;

namespace PGA305OWICalibration.Instruments
{
    public class VisaHelper
    {
        public static string GetFirstResourceName()
        {
            using (var rm = new ResourceManager())
            {
                var resources = rm.Find("?*").ToList();

                if (resources.Count == 0)
                    return "No VISA instruments found";

                var firstResource = resources[0];

                try
                {
                    using (var session = (MessageBasedSession)rm.Open(firstResource))
                    {
                        var vi = new MessageBasedFormattedIO(session);
                        vi.WriteLine("*IDN?");
                        string idn = vi.ReadLine().Trim();
                        return idn;
                    }
                }
                catch
                {
                    return firstResource + " → <Unable to query>";
                }
            }
        }
    }
}