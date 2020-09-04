using System;
using System.Collections.Generic;
using System.IO;
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
                    return Helper.ParseDouble(Options[Option][0]);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public String[] GetOptionsArray()
        {
            List<String> T = new List<string>();
            foreach(KeyValuePair<string, string[]> Option in Options)
            {
                T.Add(Option.Key + ":" + Option.Value[0]);
            }
            return T.ToArray();
        }

        public String GetDescription(String Option)
        {
            if (Options.ContainsKey(Option))
            {
                return Options[Option][1];
            }
            return null;
        }

        public CSVObject LoadOptionFile()
        {
            Console.WriteLine(System.IO.Path.GetDirectoryName(Path) + System.IO.Path.GetDirectoryName(Path).Substring(System.IO.Path.GetDirectoryName(Path).LastIndexOf(@"\")) + "_options.txt");
            if (File.Exists(System.IO.Path.GetDirectoryName(Path) + System.IO.Path.GetDirectoryName(Path).Substring(System.IO.Path.GetDirectoryName(Path).LastIndexOf(@"\")) + "_options.txt"))
            {
                String[] Lines = File.ReadAllLines(System.IO.Path.GetDirectoryName(Path) + System.IO.Path.GetDirectoryName(Path).Substring(System.IO.Path.GetDirectoryName(Path).LastIndexOf(@"\")) + "_options.txt");

                if (Lines.Length < Options.Keys.Count)
                {
                    //Return current object without modifications
                    return this;
                }

                foreach (string Line in Lines)
                {
                    String[] LSplit = Line.Split(':');
                    String Key = LSplit[0];
                    String Value = LSplit[1];
                    Console.WriteLine(Key + " " + Value);
                    SetOption(Key, Value);
                }
            }
            return this;
        }
    }
}
