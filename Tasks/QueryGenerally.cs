using EasyRent.Data;
using EasyRent.Models;
using EasyRent.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KigooProperties.Tasks
{
    public class QueryGenerally
    {

        private readonly ApplicationDbContext db;
        

        public QueryGenerally( ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<Property> GetPropertieszz(GeneralQuery generalQuery)
        {
            var result = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).AsQueryable();
            if (generalQuery != null)
            {
                if (!string.IsNullOrEmpty(generalQuery.Keyword))
                {
                    result = result.Where(m => m.Address.Contains(generalQuery.Keyword)
                    || m.Address.Contains(generalQuery.Keyword)
                    || m.GoogleMapAddress.Contains(generalQuery.Keyword)
                    || m.Description.Contains(generalQuery.Keyword)
                    || m.PropertyMode.Name.Contains(generalQuery.Keyword)
                    );
                }

                if (!string.IsNullOrEmpty(generalQuery.Country))
                {
                    result = result.Where(m => m.Address.Contains(generalQuery.Country)
                    || m.Address.Contains(generalQuery.Country)
                    || m.GoogleMapAddress.Contains(generalQuery.Country)

                    );
                }

                if (!string.IsNullOrEmpty(generalQuery.NumberOfBathrooms.ToString()))
                {
                    if (generalQuery.NumberOfBathrooms > 1 && generalQuery.NumberOfBathrooms < 4)
                    {
                        result = result.Where(m => m.bathrooms == generalQuery.NumberOfBathrooms);
                    }
                    else if (generalQuery.NumberOfBathrooms == 4)
                    {
                        result = result.Where(m => m.bathrooms > 3);
                    }
                    else
                    {

                    }
                }




                if (!string.IsNullOrEmpty(generalQuery.NumberofBedrooms.ToString()))
                {
                    if (generalQuery.NumberofBedrooms > 0 && generalQuery.NumberofBedrooms < 4)
                    {
                        result = result.Where(m => m.BedRooms == generalQuery.NumberofBedrooms);
                    }
                    else if (generalQuery.NumberofBedrooms == 4)
                    {
                        result = result.Where(m => m.BedRooms > 3);
                    }

                }



                if (!string.IsNullOrEmpty(generalQuery.PropertyStatus.ToString()))
                {
                    result = result.Where(m => m.PropertyModeId == generalQuery.PropertyStatus);
                }


                if (!string.IsNullOrEmpty(generalQuery.PropertyType.ToString()))
                {
                    result = result.Where(m => m.PropertyTypeId == generalQuery.PropertyType);
                }



            }



            return result;
        }





    }
}
