using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginLabAnalyzer.Classes
{
    public class CSVObject
    {
        public String FileName;
        public String Path;
        public String PathOut;

        private Dictionary<String, String[]> Options = new Dictionary<String, String[]> 
        {
             { "u_k", new String[]{ "8.6", "Cathode voltage drop" } },
             { "p_col_fact", new String[]{ "0.44", "Remained power from input left in arc column" } },
             { "p_exp_fact", new String[]{ "0.55", "Power used from arc column used for gas expansion" } },
             { "p_exp_min", new String[]{ "190e-03", "Minimum required level of power to create expansion" } },
             { "dt_int", new String[]{ "200e-06", "Time interval for integration" } },
             { "Pexp_c1neg_dt", new String[]{ "4e-06", "Time interval in which power is allowed to be under minimum level" } },
             { "i_experim", new String[]{  "60e-03", "Imposed experimental current" } }
        };

        public CSVObject(String FName, String P)
        {
            FileName = FName;
            Path = P;
        }

        public CSVObject(String FName, String P, String POut)
        {
            FileName = FName;
            Path = P;
            PathOut = POut;
        }

        public void SetOption(String Option, String Value)
        {
            if(Options.ContainsKey(Option))
            {
                Options[Option][0] = Value;
            }
        }

        public Double GetOption(String Option)
        {
            try
            {
                if (Options.ContainsKey(Option))
                {
                    return Double.Parse(Options[Option][0]);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public String GetDescription(String Option)
        {
            if (Options.ContainsKey(Option))
            {
                return Options[Option][1];
            }
            return null;
        }
    }
}
