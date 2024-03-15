using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Parts
{
    internal class PartsDAL : BaseDataAccessLayer
    {
        internal string GetDecentralizedPartNumber()
        {
            string partNumber = string.Empty;
            string query = @"select top 1 partnumber  from part_tb 
                        where isnull(part_tb.supplierentityid,0) <> 0
                        order by isactive desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    partNumber = reader.GetString(0);
                }
            }
            return partNumber;
        }

        internal bool DeleteDecentralizedPart(string partNumber)
        {
            string query = @"select count(*) from Part_tb where partnumber=@partNumber ";
            int partCount = -1;

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@partNumber", partNumber),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    partCount = reader.GetInt32(0);
                }
            }

            if (partCount == 1)
            {
                query = @"
                delete  from pricedetail_tb  from pricedetail_tb d inner join price_tb r on d.priceid =r.priceid inner join part_tb p on r.partid=p.partid where p.partnumber=@partNumber
                delete from price_tb from price_tb r inner join part_tb p on r.partid=p.partid where p.partnumber=@partNumber
                delete from Part_tb where partnumber= @partNumber";

                sp = new()
                {
                    new SqlParameter("@partNumber", partNumber),
                };

                if (ExecuteNonQuery(query, sp, false) < 1)
                {
                    return false;
                }
            }
            return false;
        }

        internal string GetPartNumber()
        {
            string partNumber = string.Empty;
            string query = @"SELECT top 1 Part_tb.[PartNumber] FROM [Part_tb] (NOLOCK) ORDER BY partid DESC";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    partNumber = reader.GetString(0);
                }
            }
            return partNumber;
        }


        internal Part GetPartDetails(string partNumber)
        {
            Part part = null;
            string query = @"select * from Part_tb where partnumber=@partNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@partNumber", partNumber),
            };
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    part = new Part();
                    part.PartId = reader.GetInt32(reader.GetReaderIndex("partId"));
                    part.PartNumber = reader.GetStringValue("partNumber");
                    part.LongDescription = reader.GetStringValue("partLongDescription");
                    part.UnitOfMeasure = reader.GetStringValue("unitOfMeasure");
                }
            }
            return part;
        }
    }
}
