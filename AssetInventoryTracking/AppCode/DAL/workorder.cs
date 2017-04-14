
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using BO.AssetInventoryTracking;
using System.Configuration;
namespace DAL.AssetInventoryTracking
{
	public class workorder
	{
        private static workorder _instance = null;
        public static workorder Instance
        {
            get
            {
                if ((_instance == null))
                {
                    _instance = new workorder();
                }
                return _instance;
            }
        }
        public List<BO.AssetInventoryTracking.workorder> GetAllworkorder()
		{
			List<BO.AssetInventoryTracking.workorder> xworkorderList = new List<BO.AssetInventoryTracking.workorder>();
			string query = "SELECT [workorderID],[date_created],[date_completed],[status],[inventoryID] FROM dbo.[workorder]";
			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["db_AssetInventoryTracking"].ConnectionString)) {
				using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn)) {
					conn.Open();
					using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
						while (reader.Read()) {
							BO.AssetInventoryTracking.workorder xworkorder = new BO.AssetInventoryTracking.workorder();
							if (!object.ReferenceEquals(reader["workorderID"], DBNull.Value)) {
								xworkorder.workorderID = int.Parse(reader["workorderID"].ToString());
							}
							if (!object.ReferenceEquals(reader["date_created"], DBNull.Value)) {
								xworkorder.date_created = DateTime.Parse(reader["date_created"].ToString());
							}
							if (!object.ReferenceEquals(reader["date_completed"], DBNull.Value)) {
								xworkorder.date_completed = DateTime.Parse(reader["date_completed"].ToString());
							}
							if (!object.ReferenceEquals(reader["status"], DBNull.Value)) {
								xworkorder.status = reader["status"].ToString();
							}
							if (!object.ReferenceEquals(reader["inventoryID"], DBNull.Value)) {
								xworkorder.inventoryID = int.Parse(reader["inventoryID"].ToString());
							}
							xworkorderList.Add(xworkorder);
						}
					}
				}
			}
			return xworkorderList;
		}

		public BO.AssetInventoryTracking.workorder GetByIDworkorder(int workorderID)
		{
			BO.AssetInventoryTracking.workorder xworkorder = new BO.AssetInventoryTracking.workorder();
			string query = "SELECT [workorderID],[date_created],[date_completed],[status],[inventoryID] FROM dbo.[workorder] WHERE workorderID=@workorderID";
			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["db_AssetInventoryTracking"].ConnectionString)) {
				using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn)) {
					cmd.Parameters.AddWithValue("@workorderID", workorderID);
					conn.Open();
					using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
						if (reader.Read()) {
							if (!object.ReferenceEquals(reader["workorderID"], DBNull.Value)) {
								xworkorder.workorderID = int.Parse(reader["workorderID"].ToString());
							}
							if (!object.ReferenceEquals(reader["date_created"], DBNull.Value)) {
								xworkorder.date_created = DateTime.Parse(reader["date_created"].ToString());
							}
							if (!object.ReferenceEquals(reader["date_completed"], DBNull.Value)) {
								xworkorder.date_completed = DateTime.Parse(reader["date_completed"].ToString());
							}
							if (!object.ReferenceEquals(reader["status"], DBNull.Value)) {
								xworkorder.status = reader["status"].ToString();
							}
							if (!object.ReferenceEquals(reader["inventoryID"], DBNull.Value)) {
								xworkorder.inventoryID = int.Parse(reader["inventoryID"].ToString());
							}
						}
					}
				}
			}
			return xworkorder;
		}

		public int Addworkorder(BO.AssetInventoryTracking.workorder workorder)
		{
			int workorderID = 0;
			string query = "INSERT INTO dbo.[workorder] ([date_created],[date_completed],[status],[inventoryID]) VALUES (@date_created, @date_completed, @status, @inventoryID) ";
			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["db_AssetInventoryTracking"].ConnectionString)) {
				using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn)) {
					cmd.Parameters.AddWithValue("@date_created", workorder.date_created);
                    if(workorder.date_completed == null)
                    {
                        cmd.Parameters.AddWithValue("@date_completed", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@date_completed", workorder.date_completed);
                    }
					
					cmd.Parameters.AddWithValue("@status", workorder.status);
					if ((workorder.inventoryID == -1)) {
						cmd.Parameters.AddWithValue("@inventoryID", DBNull.Value);
					} else {
						cmd.Parameters.AddWithValue("@inventoryID", workorder.inventoryID);
					}
					conn.Open();
					cmd.ExecuteScalar();
				}
			}
			return workorderID;
		}

		public int Deleteworkorder(int workorderID)
		{
			int RowsAffected = -1;
			string query = " UPDATE dbo.workorder SET IsDeleted = 1 WHERE workorderID=@workorderID ";
			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["db_AssetInventoryTracking"].ConnectionString)) {
				using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn)) {
					cmd.Parameters.AddWithValue("@workorderID", workorderID);
					conn.Open();
					RowsAffected = cmd.ExecuteNonQuery();
				}
			}
			return RowsAffected;
		}

		public int Updateworkorder(BO.AssetInventoryTracking.workorder workorder)
		{
			int RowsAffected = -1;
			string query = "UPDATE dbo.[workorder] SET date_created = @date_created,date_completed = @date_completed,status = @status,inventoryID = @inventoryID WHERE workorderID=@workorderID";
			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["db_AssetInventoryTracking"].ConnectionString)) {
				using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn)) {
					if ((workorder.workorderID == -1)) {
						cmd.Parameters.AddWithValue("@workorderID", DBNull.Value);
					} else {
						cmd.Parameters.AddWithValue("@workorderID", workorder.workorderID);
					}
					cmd.Parameters.AddWithValue("@date_created", workorder.date_created);
					cmd.Parameters.AddWithValue("@date_completed", workorder.date_completed);
					cmd.Parameters.AddWithValue("@status", workorder.status);
					if ((workorder.inventoryID == -1)) {
						cmd.Parameters.AddWithValue("@inventoryID", DBNull.Value);
					} else {
						cmd.Parameters.AddWithValue("@inventoryID", workorder.inventoryID);
					}
					conn.Open();
					RowsAffected = cmd.ExecuteNonQuery();
				}
			}
			return RowsAffected;
		}

	}
}
