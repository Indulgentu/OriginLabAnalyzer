using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OriginLabAnalyzer.Classes;

namespace OriginLabAnalyzer
{
    class DataParser
    {
        public String PathIn { get; set; }
        public String PathOut { get; set; }
        public String fName { get; private set; }
        public bool isLi = true;
        public Dictionary<String, String[]> HeaderItems { get; private set; } = new Dictionary<string, string[]>();
        private double[] FirstTen;
        private double HRes;
        private double HOffset;
        private CSVObject Options { get; set; }

        public DataParser(String PIn, String POut, CSVObject Opts)
        {
            PathIn = PIn;
            if (PathIn != null && POut != null && File.Exists(PathIn))
            {
                fName = Path.GetFileName(PathIn).Split('.')[0];
                isLi = fName.Contains("li");

                PathOut = POut + ((isLi) ? fName.Split(new[] { "li" }, StringSplitOptions.None)[0] : fName.Split(new[] { "mZ" }, StringSplitOptions.None)[0]) + @"\" + fName + @"\";
                Options = Opts;
                ReadData();
            }
        }

        public DataParser(String PIn)
        {
            PathIn = PIn;
            if (PathIn != null && File.Exists(PathIn))
            {
                fName = Path.GetFileName(PathIn).Split(new char[] {'.'})[0];
                isLi = fName.Contains("li");
                PathOut = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\" + ((isLi) ? fName.Split(new[] { "li" }, StringSplitOptions.None)[0] : fName.Split(new[] { "mZ" }, StringSplitOptions.None)[0]) + @"\" + fName + @"\";
                Options = new CSVObject(fName, PIn);
                ReadData();
            }
        }

        private void ReadData()
        {
            String[] Lines = File.ReadAllLines(PathIn).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (Lines.Length < 15)
            {
                throw new Exception("File corrupted or invalid format.");
            }

            int HeaderSize = Int32.Parse(Lines[0].Split(',')[1]); // Header size must always be on the first line!

            if (HeaderSize < 15)
            {
                throw new Exception("File corrupted or invalid format.");
            }

            for (int i = 0; i < HeaderSize; i++)
            {
                HeaderItems.Add(Regex.Replace(Lines[i].Split(',')[0], @"\s+", "").Trim('"').ToLower(), Lines[i].Replace(Lines[i].Split(',')[0].Trim(' ').Trim('"'), "").Replace('"', ' ').Replace(" ", "").ToLower().Split(','));
            }

            int BlockSize = Int32.Parse(GetHeaderItem("blocksize")[1]);
            if (Lines.Length < BlockSize)
            {
                throw new Exception("File corrupted or invalid format.");
            }

            String[] Columns = { "Time", "Current", "Voltage", "WirePos", "PowerIn", "PowerCol", "PowerExp", "Eexp_Pt", "Eexp_sumtrap", "Eexp_sum", "Eexp_trap" };
            String[] Units = { GetHeaderItem("hunit")[1], "A", "V", "mm", "W", "W", "W", "J", "J", "J", "J" };
            String[] Dates = new string[Columns.Length];
            String[] Times = new string[Columns.Length];
            HOffset = double.Parse(GetHeaderItem("hoffset")[1]);
            HRes = double.Parse(GetHeaderItem("hresolution")[1]);

            String tempPath;
            int fileNo = 0;
            String[] TempLines = new string[BlockSize];

            for (int i = 0; i < Lines.Length - HeaderSize; i++)
            {
                if (i % BlockSize == 0)
                {
                    fileNo++;
                    tempPath = (isLi) ? PathOut + fName + "_" + fileNo + ".txt" : PathOut + fName + ".txt";

                    String[] Names = new string[Columns.Length];
                    for (int j = 0; j < Names.Length; j++)
                    {
                        Names[j] = fName + "_" + fileNo;
                        Dates[j] = isLi ? GetHeaderItem("date" + fileNo)[1] : GetHeaderItem("date")[1];
                        Times[j] = isLi ? GetHeaderItem("time" + fileNo)[1] : GetHeaderItem("time")[1];
                    }

                    String[] Temp = Lines[(i + HeaderSize)..(i + BlockSize + HeaderSize)];
                    FirstTen = new double[(Temp.Length / 100) * 10];
                    for (int j = 0; j < (Temp.Length / 100) * 10; j++)
                    {
                        FirstTen[j] = Double.Parse(Temp[j].Split(',')[Array.IndexOf(GetHeaderItem("tracename"), "ch3")]);
                    }


                    double[] PcolTimes = new double[Temp.Length];
                    double[] PcolValues = new double[Temp.Length];
                    double[] PExpValues = new double[Temp.Length];

                    for (int k = 0; k < Temp.Length; k++)
                    {
                        String[] CurrLine = Temp[k].Split(',');
                       // String V_Case = isLi ? Temp[k].Split(',')[Array.IndexOf(GetHeaderItem("tracename"), "ch4")] : Temp[k].Split(',')[Array.IndexOf(GetHeaderItem("tracename"), "ch1")];
                        double Voltage = isLi ? Double.Parse(CurrLine[Array.IndexOf(GetHeaderItem("tracename"), "ch4")]) : Double.Parse(CurrLine[Array.IndexOf(GetHeaderItem("tracename"), "ch1")]);
                        //String Time = (isLi || Temp[k].Split(',')[0].Trim(' ') == "") ? (HOffset + (k) * HRes).ToString("0.00000000") : Double.Parse(Temp[k].Split(',')[0]).ToString("0.0000000");
                        double Time = (isLi || CurrLine[0].Trim(' ') == "") ? (HOffset + (k) * HRes) : Double.Parse(CurrLine[0]);
                        //String Current = GetCorrectCurrent(Double.Parse(Temp[k].Split(',')[Array.IndexOf(GetHeaderItem("tracename"), "ch3")])).ToString();
                        double Current = GetCorrectCurrent(Double.Parse(CurrLine[Array.IndexOf(GetHeaderItem("tracename"), "ch3")]));
                        //String Voltage = Double.Parse(V_Case, System.Globalization.NumberStyles.Float).ToString();
                        //String WirePos = isLi ? Double.Parse(Temp[k].Split(',')[Array.IndexOf(GetHeaderItem("tracename"), "ch1")]).ToString() : "0";
                        double WirePos = isLi ? Double.Parse(CurrLine[Array.IndexOf(GetHeaderItem("tracename"), "ch1")]) : 0;
                        //String Pin = GetInputPower(Double.Parse(Current), Double.Parse(Voltage)).ToString("0.00000000");
                        double Pin = GetInputPower(Current, Voltage);
                        //String PCol = GetPowerCol(Double.Parse(Current), Double.Parse(Voltage)).ToString();
                        double PCol = GetPowerCol(Current, Voltage);
                        //String PExp = GetPexp(Double.Parse(PCol)).ToString("0.000000");
                        double PExp = GetPexp(PCol);
                        /*PcolTimes[k] = Double.Parse(Time);
                        PcolValues[k] = GetPowerCol(Double.Parse(Current), Double.Parse(Voltage));
                        PExpValues[k] = GetPexp(GetPowerCol(Double.Parse(Current), Double.Parse(Voltage)));*/
                        PcolTimes[k] = Time;
                        PcolValues[k] = PCol;
                        PExpValues[k] = PExp;

                        if (k == Temp.Length - 1)
                        {
                            double[] ExpP = GenP(PcolValues, PcolTimes, PExpValues);
                            Temp[0] = Temp[0] + " 0 0 " + ExpP[0] + " " + ExpP[1];
                            Temp[1] = Temp[1] + " 0 0 " + ExpP[0] + " " + ExpP[1];
                            Temp[2] = Temp[2] + " 0 0 " + ExpP[2] + " " + ExpP[3];
                        }
                        Temp[k] = Time + " " + Current + " " + Voltage + " " + WirePos + " " + Pin + " " + PCol + " " + PExp + ((k < 3) ? "" : " 0 0 0 0");
                    }

                    WriteData(tempPath, String.Join(" ", Columns) + Environment.NewLine + String.Join(" ", Units) + Environment.NewLine + String.Join(" ", Dates) + Environment.NewLine + String.Join(" ", Times) + Environment.NewLine + String.Join(" ", Names) + Environment.NewLine);
                    WriteData(tempPath, Temp);
                }
            }
            Lines = null;
            GC.Collect();
        }

        private String[] GetHeaderItem(String key)
        {
            try
            {
                if (HeaderItems.ContainsKey(key))
                {
                    return HeaderItems[key];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        private double GetCorrectCurrent(double i)
        {
            double i_Exp = Options.GetOption("i_experim");
            double i_Corr;
            double i_Size = FirstTen.Length;

            if (i_Size < 10)
            {
                i_Corr = i;
            }
            else
            {
                double i_Mean = FirstTen.Average();

                if (i_Mean > i_Exp)
                {
                    i_Corr = i - (i_Mean - i_Exp);
                }
                else if (i_Mean < i_Exp && i_Mean > 0)
                {
                    if (i_Mean > i_Exp / 2)
                    {
                        i_Corr = i + (i_Exp - i_Mean);
                    }
                    else
                    {
                        i_Corr = i - i_Mean;
                    }
                }
                else
                {
                    i_Corr = i - i_Mean;
                }
            }

            return i_Corr;
        }

        private double GetInputPower(double I, double U)
        {
            return I * U;
        }

        private double GetPowerCol(double I, double U)
        {
            double U_K = Options.GetOption("u_k");
            double P_Col_Fact = Options.GetOption("p_col_fact");
            double U_Col = U - U_K;
            return U_Col * I * P_Col_Fact;
        }

        private double GetPexp(double pCol)
        {
            double p_exp_fact = Options.GetOption("p_exp_fact");
            return pCol * p_exp_fact;
        }

        private double[] GenP(double[] Values, double[] Times, double[] PExp)
        {
            double Exp_Min = Options.GetOption("p_exp_min"); 
            double Exp_M_Negative = Options.GetOption("Pexp_c1neg_dt");
            double Time_Interval = Options.GetOption("dt_int");
            double TimeStep = HRes;
            double Interval = Math.Floor(Time_Interval / TimeStep);
            double Max_Negative = Math.Floor(Exp_M_Negative / TimeStep);
            List<int> ConsecVals = new List<int>();

            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] >= Exp_Min)
                {
                    ConsecVals.Add(i);
                }
            }

            if (ConsecVals.Count < 0)
            {
                return null;
            }
            List<int> TempVals = new List<int>();
            for (int j = 1; j < ConsecVals.Count; j++)
            {
                if (ConsecVals[j] - ConsecVals[j - 1] <= Max_Negative)
                {
                    TempVals.Add(ConsecVals[j]);
                }
                else
                {
                    TempVals.Add(0);
                }
            }

            int Time_Initial_ID = 0;
            int Time_Final_ID = 0;
            double[] Result = new double[4];

            for (int i = 0; i < TempVals.Count; i++)
            {
                if (TempVals[i] == 0 || i == TempVals.Count - 1)
                {
                    if (Time_Initial_ID == 0 || Time_Final_ID == 0)
                    {
                        Time_Initial_ID = 0;
                        Time_Final_ID = 0;
                        continue;
                    }

                    if (i == TempVals.Count - 1 && TempVals[i] != 0)
                    {
                        Time_Final_ID = TempVals[i];
                    }

                    double t1 = Times[Time_Initial_ID];
                    double t2 = Times[Time_Final_ID];

                    double dt = 0;

                    if (t1 < 0 && t2 < 0)
                    {
                        dt = Math.Abs(t1 - t2);
                    }
                    else
                    {
                        dt = t2 - t1;
                    }

                    if (dt > Time_Interval)
                    {
                        double T_Int_Initial = t1;
                        int T_Int_Init_ID = Time_Initial_ID;
                        double T_Tnt_Final = t1 + Time_Interval;
                        int T_Int_Final_ID = Array.IndexOf(Times, Array.FindLast(Times, n => n <= T_Tnt_Final)) + 1;

                        double[] PExp2 = PExp[T_Int_Init_ID..T_Int_Final_ID];
                        double Trapz = 0;

                        for (int j = 1; j < PExp2.Length; j++)
                        {
                            double x = TimeStep;
                            Trapz += x * (PExp2[j - 1] + PExp2[j]) / 2;
                        }

                        for (int j = 0; j < PExp2.Length; j++)
                        {
                            PExp2[j] = PExp2[j] * TimeStep;
                        }
                        double Exp_Sum = PExp2.Sum();
                        Result[0] = T_Int_Initial;
                        Result[1] = T_Tnt_Final;
                        Result[2] = Exp_Sum;
                        Result[3] = Trapz;
                    }
                    Time_Final_ID = 0;
                     Time_Initial_ID = 0;
                }
                else if (TempVals[i] > 0 && Time_Initial_ID == 0)
                {
                    Time_Initial_ID = TempVals[i];
                }
                else
                {
                    Time_Final_ID = TempVals[i];
                }
            }

            return Result;


        }

        private void WriteData(String @path, String[] Data)
        {
            if (!Directory.Exists(PathOut))
            {
                try
                {
                    Directory.CreateDirectory(PathOut);
                }
                catch
                {
                    throw new Exception("Could not create directory. Path: " + PathOut);
                }
            }
            try
            {
                File.AppendAllLines(@path, Data);
            }
            catch
            {
                throw new Exception("Could not write data to file. Path: " + path );
            }
        }

        private void WriteData(String @path, String Data)
        {
            if (!Directory.Exists(PathOut))
            {
                try
                {
                    Directory.CreateDirectory(PathOut);
                }
                catch 
                {
                    throw new Exception("Could not create directory. Path: " + PathOut);
                }
            }
            try
            {
                File.WriteAllText(@path, Data);
            }
            catch 
            {
                throw new Exception("Could not write data to file. Path: " + path);
            }
        }

    }
}
