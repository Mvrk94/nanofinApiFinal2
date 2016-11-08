﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models.DTOEnvironment;
using NanofinAPI.Models;
using Extreme.Statistics;
using Extreme.Statistics.TimeSeriesAnalysis;

namespace NanofinAPI.Controllers
{
    public class ReportsMasterController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public List<productsalesandtarget> TargetProgrees(int productProvider, int numMonths)
        {
            return db.productsalesandtargets.ToList();
        }

        [HttpGet]
        public List<productswithpurchas> getProductList()
        {
            return (from c in db.productswithpurchases select c).ToList();
        }

        #region Monthly Sales
        [HttpGet]
        public DTOcompareProducts getMonthyProductSales(int productID)
        {
            var toreturn = new DTOcompareProducts();
            var pastSales = (from c in db.productsalespermonths where c.Product_ID == productID
                             select c.sales.Value).ToList();

            toreturn.name = db.products.Find(productID).productName;
            toreturn.previouse = Array.ConvertAll(pastSales.ToArray(), x => (double)x);

            return toreturn;
        }

        [HttpGet]
        public ProductForCast getProductSalesPredictions( int productID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new ProductForCast();
            var pastSales = (from c in db.productsalespermonths where c.Product_ID== productID select c.sales.Value).ToList();

            toreturn.productID = productID;
            toreturn.name = db.products.Find(productID).productName;
            toreturn.previouse = Array.ConvertAll(pastSales.ToArray(), x => (double)x);          

            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);
            
            return toreturn;
        }


        [HttpGet]
        public overallForeCast getPPMonthlySalesForecast(int productProvider, int numPredictions,int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.salespermonths select c.sales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

            return toreturn;
        }

        
        [HttpGet]
        public List<DTOlastmonthprovincesale> get_PP_ProvincialSales()
        {
            var sales = (from c in db.lastmonthprovincesales select c).ToList();
            var toreturn = new List<DTOlastmonthprovincesale>();

            foreach( var temp  in  sales)
            {
                toreturn.Add(new DTOlastmonthprovincesale(temp));
            }

            return toreturn;
        }


       [HttpGet]
       public List<DTOmonthlylocationsale> GetMonthlyProductsalesperlocation(int productID , int locationID)
        {
            DateTime current = DateTime.Now.AddMonths(-1);
            var list = (from c in db.monthlylocationsales where c.Product_ID == productID && c.transactionLocation == locationID select c).ToList(); ;
            var locals = db.locations;
            var toreturn = new List<DTOmonthlylocationsale>();

            foreach (var temp in list)
            {
                toreturn.Add(new DTOmonthlylocationsale(temp));
            }
            
            return toreturn;
        }
    #endregion

        #region Product sales Perlocation
        [HttpGet]
        public overallForeCast PredictLocationSales( int  productID, int locationID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.monthlylocationsales where c.transactionLocation == locationID select c.sales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

            return toreturn;
        }


        [HttpGet]
        public List<DTOmonthlyproductsalesperlocation> getProductLocationExpenditure(int productID)
        {

            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            var list = (from c in db.monthlyproductsalesperlocations where c.purchaseDate >= date && c.Product_ID == productID select c).ToList();

            var toreturn = new List<DTOmonthlyproductsalesperlocation>();

            foreach (var temp in list)
            {
                toreturn.Add(new DTOmonthlyproductsalesperlocation(temp));
            }

            return toreturn;
        }
        #endregion


        #region Geo Reports

        [HttpGet]
        public List<monlthlocationsalessum> getCurrentMonthlyLocationSalesSum()
        {
            var lowerDate = DateTime.Now;
            var upperDate = DateTime.Now;

            var currentDay = (lowerDate.Day * -1);
            lowerDate = lowerDate.AddDays(currentDay);

            var list = (from c in db.monlthlocationsalessums where c.purchaseDate > lowerDate && c.purchaseDate <= upperDate select c).ToList();

            return list;
        }
        

        [HttpGet]
        public List<monthlylocationsale> getCurrentMonthLocationProductSalesDistribution(int locationID)
        {
            var lowerDate = DateTime.Now;
            var upperDate = DateTime.Now;

            var currentDay = (lowerDate.Day * -1);
            lowerDate = lowerDate.AddDays(currentDay);

            var toreturn = (from c in db.monthlylocationsales where c.datum > lowerDate && c.transactionLocation == locationID  && c.datum < upperDate  orderby c.sales descending select c).ToList();
            
            return toreturn;
        }

        [HttpGet]
        public double []  getAllLocationSales(int locationID)
        {
            var purchases = (from c in db.monlthlocationsalessums where c.Location_ID == locationID select c.sales).ToArray();
            return Array.ConvertAll<decimal?,double>(purchases, x => (double)x);
        }

        [HttpGet]
        public List<monthlylocationsale> getProductSalesThisMonth(int productID)
        {

            var lowerDate = DateTime.Now;
            var upperDate = DateTime.Now;

            var currentDay = (lowerDate.Day * -1);
            lowerDate = lowerDate.AddDays(currentDay);

            var toreturn = (from c in db.monthlylocationsales where c.datum > lowerDate where c.Product_ID == productID && c.datum < upperDate select c).ToList();

            return toreturn;
        }

        #endregion

        #region demograpgic product purchase information
        [HttpGet]
        public List<demographicconsumerproductlocationlastmonthsale> getDemographicProductLocationLastMonthSales(int productID, int locationsID)
        {
            return (from c in db.demographicconsumerproductlocationlastmonthsales where c.Product_ID == productID && c.transactionLocation == locationsID orderby  c.numConsumers descending select c).ToList();
        }

        [HttpGet]
        public List<demographicconsumerproductlocationlastmonthsale> getDemographicLocationLastMonthSales(int locationsID)
        {
            return (from c in db.demographicconsumerproductlocationlastmonthsales where c.transactionLocation == locationsID orderby c.numConsumers descending select c).ToList();
        }

        #endregion
        
        #region Insurance Type
        [HttpGet]
          public List<lastmonthinsurancetypesale>  getLastMonthInsuranceTypeSales()
        {
            var toreturn = new List<lastmonthinsurancetypesale>();
            toreturn = db.lastmonthinsurancetypesales.ToList();

            return toreturn;
        }
        
        [HttpGet]
        public overallForeCast PredictInsuranceTypeSales(int insuranceTypeID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.insuranceproducttypemonthlysales where c.InsuranceType_ID == insuranceTypeID select c.sales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

            return toreturn;
        }


        public List<insuranceproducttypemonthlysale> getinsurancelist()
        {
            return db.insuranceproducttypemonthlysales.ToList();
        }

        [HttpGet]
        public List<provincialinsurancetypesale> getProvincialProductType()
        {
            return (from c in db.provincialinsurancetypesales select c).ToList();
        }

        #endregion


        [HttpGet]
        public List<currentmonthdailysale> getCurrentMonthDailySales()
        {
            return db.currentmonthdailysales.ToList();
        }

        [HttpPost]
        public Boolean processApplicationNativeImp(int activeProductID)
        {
            var toDeactivate  = db.activeproductitems.Find(activeProductID);
            toDeactivate.activeProductItemPolicyNum = "PP_IM_" + activeProductID.ToString();
            db.SaveChanges();
            return true;
        }

        #region Utils
        private double []  generateTimeData(int size)
        {
            double[] timeS = new double[size ];
            var counter = 0;
            for (var i = 0; i < size; i++)
                timeS[i] = counter++;

            return timeS;
        }


       

        #endregion

    }

}
