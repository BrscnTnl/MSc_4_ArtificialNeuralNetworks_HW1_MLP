using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class DataPrep
    {
        public List<ForestFire> ForestFireTrainSet = new List<ForestFire>();
        public double[][] ForestFireTrainSetArray;
        public double[][] ForestFireTestSetArray;

        public List<ForestFire> ForestFireTestSet = new List<ForestFire>();
        private List<ForestFire> ForestFireAllSet = new List<ForestFire>();
        private List<ForestFire> ForestFireOriginalSet = new List<ForestFire>();
        public List<ForestFire> ForestFireOriginalSetTransformed = new List<ForestFire>();





        public DataPrep()
        {


        }

        public void Initialize()
        {
            ExtractData();
            TransformData();
            ForestFireOriginalSetTransformed = NormalizeData(ForestFireOriginalSetTransformed);
            SeperateTestData(ForestFireOriginalSetTransformed);
        }



        private void ExtractData()
        {


            var reader = new StreamReader(File.OpenRead(@"D:\forestfires_data_trn.csv"));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                //line = line.Replace(".", ",");
                var values = line.Split(';');

                ForestFire newForestFire = new ForestFire();

                newForestFire.X = double.Parse(values[0]);
                newForestFire.Y = double.Parse(values[1]);
                newForestFire.month = (values[2]);
                newForestFire.day = (values[3]);
                newForestFire.FFMC = double.Parse(values[4], CultureInfo.InvariantCulture);
                newForestFire.DMC = double.Parse(values[5], CultureInfo.InvariantCulture);
                newForestFire.DC = double.Parse(values[6], CultureInfo.InvariantCulture);
                newForestFire.ISI = double.Parse(values[7], CultureInfo.InvariantCulture);
                newForestFire.temp = double.Parse(values[8], CultureInfo.InvariantCulture);
                newForestFire.RH = double.Parse(values[9], CultureInfo.InvariantCulture);
                newForestFire.wind = double.Parse(values[10], CultureInfo.InvariantCulture);
                newForestFire.rain = double.Parse(values[11], CultureInfo.InvariantCulture);
                newForestFire.area = double.Parse(values[12], CultureInfo.InvariantCulture);
                newForestFire.isTest = false;
                ForestFireOriginalSet.Add(newForestFire);


            }


            var reader2 = new StreamReader(File.OpenRead(@"D:\forestfires_data_val.csv"));
            while (!reader2.EndOfStream)
            {
                var line = reader2.ReadLine();
                //line = line.Replace(".", ",");
                var values = line.Split(';');

                ForestFire newForestFire = new ForestFire();

                newForestFire.X = double.Parse(values[0]);
                newForestFire.Y = double.Parse(values[1]);
                newForestFire.month = (values[2]);
                newForestFire.day = (values[3]);
                newForestFire.FFMC = double.Parse(values[4], CultureInfo.InvariantCulture);
                newForestFire.DMC = double.Parse(values[5], CultureInfo.InvariantCulture);
                newForestFire.DC = double.Parse(values[6], CultureInfo.InvariantCulture);
                newForestFire.ISI = double.Parse(values[7], CultureInfo.InvariantCulture);
                newForestFire.temp = double.Parse(values[8], CultureInfo.InvariantCulture);
                newForestFire.RH = double.Parse(values[9], CultureInfo.InvariantCulture);
                newForestFire.wind = double.Parse(values[10], CultureInfo.InvariantCulture);
                newForestFire.rain = double.Parse(values[11], CultureInfo.InvariantCulture);
                newForestFire.area = double.Parse(values[12], CultureInfo.InvariantCulture);
                newForestFire.isTest = true;
                //double.Parse("0.5e10", CultureInfo.InvariantCulture)
                ForestFireOriginalSet.Add(newForestFire);


            }


        }


        private void TransformData()
        {


            int AutoID = 0;

            foreach (ForestFire newForestFire in ForestFireOriginalSet)
            {
                double tmp_Month_jan=0,tmp_Month_feb = 0, tmp_Month_mar = 0, tmp_Month_apr = 0,
                tmp_Month_may = 0, tmp_Month_jun = 0, tmp_Month_jul = 0, tmp_Month_aug = 0,
                tmp_Month_sep = 0, tmp_Month_oct = 0, tmp_Month_nov = 0, tmp_Month_dec = 0;

                double tmp_WeekDay_mon = 0, tmp_WeekDay_tue = 0, tmp_WeekDay_wed = 0, tmp_WeekDay_thu = 0,
                       tmp_WeekDay_fri = 0, tmp_WeekDay_sat = 0, tmp_WeekDay_sun = 0;

                switch (newForestFire.month)
                {
                    case "jan": tmp_Month_jan = 1; break;
                    case "feb": tmp_Month_feb = 1; break;
                    case "mar": tmp_Month_mar = 1; break;
                    case "apr": tmp_Month_apr = 1; break;
                    case "may": tmp_Month_may = 1; break;
                    case "jun": tmp_Month_jun = 1; break;
                    case "jul": tmp_Month_jul = 1; break;
                    case "aug": tmp_Month_aug = 1; break;
                    case "sep": tmp_Month_sep = 1; break;
                    case "oct": tmp_Month_oct = 1; break;
                    case "nov": tmp_Month_nov = 1; break;
                    case "dec": tmp_Month_dec = 1; break;
                    default:tmp_Month_jan = 0; break;

                }

                switch (newForestFire.day)
                {
                    case "mon": tmp_WeekDay_mon = 1; break;
                    case "tue": tmp_WeekDay_tue = 1; break;
                    case "wed": tmp_WeekDay_wed = 1; break;
                    case "thu": tmp_WeekDay_thu = 1; break;
                    case "fri": tmp_WeekDay_fri = 1; break;
                    case "sat": tmp_WeekDay_sat = 1; break;
                    case "sun": tmp_WeekDay_sun = 1; break;
                 
                    default: tmp_WeekDay_mon = 0; break;

                }



                ForestFireOriginalSetTransformed.Add(
                        new ForestFire()
                        {

                            ID = AutoID,
                            X = newForestFire.X,
                            Y = newForestFire.Y,
                            isTest = newForestFire.isTest,
                            month = newForestFire.month,
                            day = newForestFire.day,
                            FFMC = newForestFire.FFMC,
                            DMC = newForestFire.DMC,
                            DC = newForestFire.DC,
                            ISI = newForestFire.ISI,
                            temp = newForestFire.temp,
                            RH = newForestFire.RH,
                            wind = newForestFire.wind,
                            rain = newForestFire.rain,
                            area = newForestFire.area,

                            CD_Month_jan = tmp_Month_jan,
                            CD_Month_feb = tmp_Month_feb,
                            CD_Month_mar = tmp_Month_mar,
                            CD_Month_apr = tmp_Month_apr,
                            CD_Month_may = tmp_Month_may,
                            CD_Month_jun = tmp_Month_jun,
                            CD_Month_jul = tmp_Month_jul,
                            CD_Month_aug = tmp_Month_aug,
                            CD_Month_sep = tmp_Month_sep,
                            CD_Month_oct = tmp_Month_oct,
                            CD_Month_nov = tmp_Month_nov,
                            CD_Month_dec = tmp_Month_dec,

                            CD_WeekDay_mon = tmp_WeekDay_mon,
                            CD_WeekDay_tue = tmp_WeekDay_tue,
                            CD_WeekDay_wed = tmp_WeekDay_wed,
                            CD_WeekDay_thu = tmp_WeekDay_thu,
                            CD_WeekDay_fri = tmp_WeekDay_fri,
                            CD_WeekDay_sat = tmp_WeekDay_sat,
                            CD_WeekDay_sun = tmp_WeekDay_sun

                        }
                    );
                AutoID++;

            }

        }



        private void SeperateTestData(List<ForestFire> ForestFires)
        {
            ForestFireTrainSet = ForestFires.Where(x => x.isTest == false).ToList();
            ForestFireTestSet = ForestFires.Where(x => x.isTest == true).ToList();
            ForestFireTrainSetArray = new double[ForestFireTrainSet.Count][];
            ForestFireTestSetArray = new double[ForestFireTestSet.Count][];
            //for (int i = 0; i < ForestFireTrainSet.Count; i++)
            //{
            //    ForestFireTrainSetArray[i][0] = 1.0;
            //    ForestFireTrainSetArray[i][1] = ForestFireTrainSet.ElementAt(i).X;
            //    ForestFireTrainSetArray[i][2] = ForestFireTrainSet.ElementAt(i).Y;
            //    ForestFireTrainSetArray[i][3] = ForestFireTrainSet.ElementAt(i).FFMC;
            //    ForestFireTrainSetArray[i][4] = ForestFireTrainSet.ElementAt(i).DMC;
            //    ForestFireTrainSetArray[i][5] = ForestFireTrainSet.ElementAt(i).DC;
            //    ForestFireTrainSetArray[i][6] = ForestFireTrainSet.ElementAt(i).ISI;
            //    ForestFireTrainSetArray[i][7] = ForestFireTrainSet.ElementAt(i).temp;
            //    ForestFireTrainSetArray[i][8] = ForestFireTrainSet.ElementAt(i).RH;
            //    ForestFireTrainSetArray[i][9] = ForestFireTrainSet.ElementAt(i).wind;
            //    ForestFireTrainSetArray[i][10] = ForestFireTrainSet.ElementAt(i).rain;
            //    ForestFireTrainSetArray[i][11] = ForestFireTrainSet.ElementAt(i).CD_Month_jan;
            //    ForestFireTrainSetArray[i][12] = ForestFireTrainSet.ElementAt(i).CD_Month_feb;
            //    ForestFireTrainSetArray[i][13] = ForestFireTrainSet.ElementAt(i).CD_Month_mar;
            //    ForestFireTrainSetArray[i][14] = ForestFireTrainSet.ElementAt(i).CD_Month_apr;
            //    ForestFireTrainSetArray[i][15] = ForestFireTrainSet.ElementAt(i).CD_Month_may;
            //    ForestFireTrainSetArray[i][16] = ForestFireTrainSet.ElementAt(i).CD_Month_jun;
            //    ForestFireTrainSetArray[i][17] = ForestFireTrainSet.ElementAt(i).CD_Month_jul;
            //    ForestFireTrainSetArray[i][18] = ForestFireTrainSet.ElementAt(i).CD_Month_aug;
            //    ForestFireTrainSetArray[i][19] = ForestFireTrainSet.ElementAt(i).CD_Month_sep;
            //    ForestFireTrainSetArray[i][20] = ForestFireTrainSet.ElementAt(i).CD_Month_oct;
            //    ForestFireTrainSetArray[i][21] = ForestFireTrainSet.ElementAt(i).CD_Month_nov;
            //    ForestFireTrainSetArray[i][22] = ForestFireTrainSet.ElementAt(i).CD_Month_dec;
            //    ForestFireTrainSetArray[i][23] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_mon;
            //    ForestFireTrainSetArray[i][24] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_tue;
            //    ForestFireTrainSetArray[i][25] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_wed;
            //    ForestFireTrainSetArray[i][26] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_thu;
            //    ForestFireTrainSetArray[i][27] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_fri;
            //    ForestFireTrainSetArray[i][28] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_sat;
            //    ForestFireTrainSetArray[i][29] = ForestFireTrainSet.ElementAt(i).CD_WeekDay_sun;
            //}

            //for (int i = 0; i < ForestFireTestSet.Count; i++)
            //{
            //    ForestFireTestSetArray[i][0] = 1.0;
            //    ForestFireTestSetArray[i][1] = ForestFireTestSet.ElementAt(i).X;
            //    ForestFireTestSetArray[i][2] = ForestFireTestSet.ElementAt(i).Y;
            //    ForestFireTestSetArray[i][3] = ForestFireTestSet.ElementAt(i).FFMC;
            //    ForestFireTestSetArray[i][4] = ForestFireTestSet.ElementAt(i).DMC;
            //    ForestFireTestSetArray[i][5] = ForestFireTestSet.ElementAt(i).DC;
            //    ForestFireTestSetArray[i][6] = ForestFireTestSet.ElementAt(i).ISI;
            //    ForestFireTestSetArray[i][7] = ForestFireTestSet.ElementAt(i).temp;
            //    ForestFireTestSetArray[i][8] = ForestFireTestSet.ElementAt(i).RH;
            //    ForestFireTestSetArray[i][9] = ForestFireTestSet.ElementAt(i).wind;
            //    ForestFireTestSetArray[i][10] = ForestFireTestSet.ElementAt(i).rain;
            //    ForestFireTestSetArray[i][11] = ForestFireTestSet.ElementAt(i).CD_Month_jan;
            //    ForestFireTestSetArray[i][12] = ForestFireTestSet.ElementAt(i).CD_Month_feb;
            //    ForestFireTestSetArray[i][13] = ForestFireTestSet.ElementAt(i).CD_Month_mar;
            //    ForestFireTestSetArray[i][14] = ForestFireTestSet.ElementAt(i).CD_Month_apr;
            //    ForestFireTestSetArray[i][15] = ForestFireTestSet.ElementAt(i).CD_Month_may;
            //    ForestFireTestSetArray[i][16] = ForestFireTestSet.ElementAt(i).CD_Month_jun;
            //    ForestFireTestSetArray[i][17] = ForestFireTestSet.ElementAt(i).CD_Month_jul;
            //    ForestFireTestSetArray[i][18] = ForestFireTestSet.ElementAt(i).CD_Month_aug;
            //    ForestFireTestSetArray[i][19] = ForestFireTestSet.ElementAt(i).CD_Month_sep;
            //    ForestFireTestSetArray[i][20] = ForestFireTestSet.ElementAt(i).CD_Month_oct;
            //    ForestFireTestSetArray[i][21] = ForestFireTestSet.ElementAt(i).CD_Month_nov;
            //    ForestFireTestSetArray[i][22] = ForestFireTestSet.ElementAt(i).CD_Month_dec;
            //    ForestFireTestSetArray[i][23] = ForestFireTestSet.ElementAt(i).CD_WeekDay_mon;
            //    ForestFireTestSetArray[i][24] = ForestFireTestSet.ElementAt(i).CD_WeekDay_tue;
            //    ForestFireTestSetArray[i][25] = ForestFireTestSet.ElementAt(i).CD_WeekDay_wed;
            //    ForestFireTestSetArray[i][26] = ForestFireTestSet.ElementAt(i).CD_WeekDay_thu;
            //    ForestFireTestSetArray[i][27] = ForestFireTestSet.ElementAt(i).CD_WeekDay_fri;
            //    ForestFireTestSetArray[i][28] = ForestFireTestSet.ElementAt(i).CD_WeekDay_sat;
            //    ForestFireTestSetArray[i][29] = ForestFireTestSet.ElementAt(i).CD_WeekDay_sun;
            //}


        }


        private List<ForestFire> NormalizeData(List<ForestFire> ForestFires)
        {

            double X_Mean, Y_Mean, FFMC_Mean, DMC_Mean, DC_Mean, ISI_Mean, temp_Mean, RH_Mean, wind_Mean, rain_Mean, area_Mean;

            double X_StdDev, Y_StdDev, FFMC_StdDev, DMC_StdDev, DC_StdDev, ISI_StdDev, temp_StdDev, RH_StdDev, wind_StdDev, rain_StdDev, area_StdDev;


            X_Mean = ForestFires.Select(x => x.X).ToList().Sum() / ForestFires.Count;
            Y_Mean = ForestFires.Select(x => x.Y).ToList().Sum() / ForestFires.Count;
            FFMC_Mean = ForestFires.Select(x => x.FFMC).ToList().Sum() / ForestFires.Count;
            DMC_Mean = ForestFires.Select(x => x.DMC).ToList().Sum() / ForestFires.Count;
            DC_Mean = ForestFires.Select(x => x.DC).ToList().Sum() / ForestFires.Count;
            ISI_Mean = ForestFires.Select(x => x.ISI).ToList().Sum() / ForestFires.Count;
            temp_Mean = ForestFires.Select(x => x.temp).ToList().Sum() / ForestFires.Count;
            RH_Mean = ForestFires.Select(x => x.RH).ToList().Sum() / ForestFires.Count;
            wind_Mean = ForestFires.Select(x => x.wind).ToList().Sum() / ForestFires.Count;
            rain_Mean = ForestFires.Select(x => x.rain).ToList().Sum() / ForestFires.Count;
            area_Mean = ForestFires.Select(x => x.area).ToList().Sum() / ForestFires.Count;

            X_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.X - X_Mean, 2)) / ForestFires.Count);
            Y_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.Y - Y_Mean, 2)) / ForestFires.Count);
            FFMC_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.FFMC - FFMC_Mean, 2)) / ForestFires.Count);
            DMC_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.DMC - DMC_Mean, 2)) / ForestFires.Count);
            DC_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.DC - DC_Mean, 2)) / ForestFires.Count);
            ISI_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.ISI - ISI_Mean, 2)) / ForestFires.Count);
            temp_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.temp - temp_Mean, 2)) / ForestFires.Count);
            RH_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.RH - RH_Mean, 2)) / ForestFires.Count);
            wind_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.wind - wind_Mean, 2)) / ForestFires.Count);
            rain_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.rain - rain_Mean, 2)) / ForestFires.Count);
            area_StdDev = Math.Sqrt(ForestFires.Sum(x => Math.Pow(x.area - area_Mean, 2)) / ForestFires.Count);
           

            ForestFires.ToList().ForEach(x => x.X = Convert.ToSingle(((x.X) - X_Mean) / X_StdDev));
            ForestFires.ToList().ForEach(x => x.Y = Convert.ToSingle(((x.Y) - Y_Mean) / Y_StdDev));
            ForestFires.ToList().ForEach(x => x.FFMC = Convert.ToSingle(((x.FFMC) - FFMC_Mean) / FFMC_StdDev));
            ForestFires.ToList().ForEach(x => x.DMC = Convert.ToSingle(((x.DMC) - DMC_Mean) / DMC_StdDev));
            ForestFires.ToList().ForEach(x => x.DC = Convert.ToSingle(((x.DC) - DC_Mean) / DC_StdDev));
            ForestFires.ToList().ForEach(x => x.ISI = Convert.ToSingle(((x.ISI) - ISI_Mean) / ISI_StdDev));
            ForestFires.ToList().ForEach(x => x.temp = Convert.ToSingle(((x.temp) - temp_Mean) / temp_StdDev));
            ForestFires.ToList().ForEach(x => x.RH = Convert.ToSingle(((x.RH) - RH_Mean) / RH_StdDev));
            ForestFires.ToList().ForEach(x => x.wind = Convert.ToSingle(((x.wind) - wind_Mean) / wind_StdDev));
            ForestFires.ToList().ForEach(x => x.rain = Convert.ToSingle(((x.rain) - rain_Mean) / rain_StdDev));
            ForestFires.ToList().ForEach(x => x.area = Convert.ToSingle(((x.area) - area_Mean) / area_StdDev));
           
            return ForestFires;
        }


    }
}
