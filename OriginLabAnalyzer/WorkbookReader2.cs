﻿using System;
using System.IO;

namespace OriginLabAnalyzer
{
    class WorkbookReader2
    {
        private static Origin.IOApplication App;
        private Origin.WorksheetPage Page;
        private Origin.WorksheetPages Pages;
        private Origin.Layers Layers;
        private Origin.Worksheet Wks;
        public int Columns = 0;
        public int Rows = 0;
        public String[] Long_Names;
        public String[] Units;
        public String[] Comments;
        public int MaxRows = 0;

        public WorkbookReader2(String @path, String name)
        {
            InitWorkBook(path, name);
        }
        
        public WorkbookReader2() { 

        }

        public double[,] ParseWorkbook(String @path)
        {
            String TestFile = path;

            try
            {
                double[,] Data = null;
                String[] Lines = File.ReadAllLines(TestFile);

                String[] T;

                if(Lines.Length <= 6)
                {
                    Console.WriteLine("Empty file or incorrect file format: " + path + ". Skipping...");
                    return null;
                }

                for (int i = 0; i < Lines.Length; i++)
                {
                    T = Lines[i].Split(" ".ToCharArray());

                    if (i < 5)
                    {
                        switch (i)
                        {
                            case 0:
                                Long_Names = T;
                                Columns = Long_Names.Length;
                                Rows = Lines.Length - 5; // Data rows start at line 5
                                MaxRows = (MaxRows >= 6) ? MaxRows : Rows;
                                Data = new double[Rows, Columns];
                                break;
                            case 1:
                                Units = T;
                                break;
                            case 2:
                                Comments = T;
                                break;
                            default:
                                break;

                        }
                        continue;
                    }

                    for(int j = 0; j < Columns; j++)
                    {
                        Data[i-5, j] = Double.Parse(T[j]);
                    }
                }

                return Data;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Cannot find file. Exception: " + ex.Message);
            }
            return null;
        }

        public void InitWorkBook(String @path, String name)
        {
            bool NewLayer = true;
            Double[,] Data = ParseWorkbook(@path);

            if (Data != null)
            {
                try
                {
                    if (App == null)
                    {
                        App = new Origin.Application();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                App.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOW_BRING_TO_FRONT;
                App.CanClose = true;
                Pages = App.WorksheetPages;

                Page = (Pages.Count <= 0) ? Pages[App.CreatePage((int)Origin.PAGETYPES.OPT_WORKSHEET, "Book", "W", 2)] : Pages["Book"];
                Layers = Page.Layers;

                if (Layers[name] != null)
                {
                    Wks = (Origin.Worksheet)Layers[name];
                    NewLayer = false;
                }
                else
                {
                    Wks = (Origin.Worksheet)Layers.Add(name, 0, null, 0, null);
                }

                //Wks = Layers[name] != null ? (Origin.Worksheet)Layers[name] : (Origin.Worksheet)Layers.Add(name, 0, null, 0, null);
                Wks.Activate();
                if (Wks == null) { Console.WriteLine("Cannot create worksheet for some reason."); return; }

                if (NewLayer) {
                    for (int i = 0; i < Columns; i++)
                    {
                        Wks.Columns.Add(Long_Names[i]);
                        Wks.Columns[i].LongName = Long_Names[i];
                        Wks.Columns[i].Units = Units[i];
                        Wks.Columns[i].Comments = Comments[i];
                    }
                }
                Wks.SetData(Data, -1);
                /*if (Wks.Columns.Count >= 11)
                {
                    Wks.Columns[12].Destroy();
                    Wks.Columns[11].Destroy();
                }*/
                //Wks.Columns[13].Destroy();
            }
        }

        public void GenerateGraphs()
        {

            for (int i = 1; i < App.Pages["Book"].Layers.Count; i++)
            {
                Origin.GraphPage gp = (App.GraphPages["Graph"] != null) ? App.GraphPages["Graph"] : App.GraphPages.Add(@"Content\aolo.otpu");

                if (gp != null)
                {
                    Origin.Worksheet CurrentWorksheet = (Origin.Worksheet)App.Pages["Book"].Layers[i];
                    for (int j = 0; j < gp.Layers.Count; j++)
                    {
                        Origin.GraphLayer gl = (Origin.GraphLayer)gp.Layers[j];
                        if (gl != null)
                        {
                            Origin.DataRange dr = null;

                            switch (j)
                            {
                                case 0:
                                    dr = CurrentWorksheet.NewDataRange(-1, 0, MaxRows, 1);
                                    break;
                                case 1:
                                    dr = CurrentWorksheet.NewDataRange(-1, 2, MaxRows, 2);
                                    break;
                                case 2:
                                    dr = CurrentWorksheet.NewDataRange(-1, 4, MaxRows, 6);
                                    break;
                                default: break;
                            }
                            //gl.Execute("speedmode sm:=3");
                            gl.DataPlots.Add(dr, Origin.PLOTTYPES.IDM_PLOT_LINE);



                            // Setup the Y axis to auto adjust the scale to fit any data
                            // points that are less than or greater than the scale's range.
                            gl.Execute("layer.disp = layer.disp | hex(1000);");
                            gp.Name = CurrentWorksheet.Name;
                        }
                    }
                }
            }
        }
    }
}
