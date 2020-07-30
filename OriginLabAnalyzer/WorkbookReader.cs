using System;
using System.IO;

namespace OriginLabAnalyzer
{
    class WorkbookReader
    {
        public static Origin.IOApplication App;
        private Origin.WorksheetPage Page;
        private Origin.WorksheetPages Pages;
        private Origin.Layers Layers;
        private Origin.Worksheet Wks;
        public int Columns = 0;
        public int Rows = 0;
        public String[] Long_Names;
        public String[] Units;
        public String[] Comments;

        public WorkbookReader(String @path, String name)
        {
            InitWorkBook(path, name);
        }
        
        public WorkbookReader() { 

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
                    T = Lines[i].Split(' ');

                    if (i < 5)
                    {
                        switch (i)
                        {
                            case 0:
                                Long_Names = T;
                                Columns = Long_Names.Length;
                                Rows = Lines.Length - 5; // Data rows start at line 5
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

                Pages = App.WorksheetPages;
                Page = (Pages.Count <= 0) ? Pages[App.CreatePage((int)Origin.PAGETYPES.OPT_WORKSHEET, "Book", "W", 2)] : Pages["Book"];
                Layers = Page.Layers;
                Wks = (Origin.Worksheet)Layers.Add(name, 0, @"C:\Users\FG-EGA\source\repos\OriginLabAnalyzer\OriginLabAnalyzer\Content\booky.otwu", 0, null);

                Wks.Activate();
                if (Wks == null) { Console.WriteLine("Cannot create worksheet for some reason."); return; }

                Wks.SetData(Data, -1);
            }
        }

        public void GenerateGraphs()
        {
            /*App.RootFolder.Folders.Add("Graphs");
            App.RootFolder.Folders["Graphs"];*/

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
                                    dr = CurrentWorksheet.NewDataRange(-1, 0, CurrentWorksheet.Rows, 1);
                                    break;
                                case 1:
                                    dr = CurrentWorksheet.NewDataRange(-1, 2, CurrentWorksheet.Rows, 2);
                                    break;
                                case 2:
                                    dr = CurrentWorksheet.NewDataRange(-1, 4, CurrentWorksheet.Rows, 6);
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
