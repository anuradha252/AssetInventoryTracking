
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace BO.AssetInventoryTracking
{
	[Serializable()]
	public class workorder
	{
		public workorder()
		{
			_workorderID = -1;
			_date_created = null;
			_date_completed = null;
			_status = "";
			_inventoryID = -1;
            _date_modified = null;
		}
		private int _workorderID;
		private DateTime? _date_created;
		private DateTime? _date_completed;
        private DateTime? _date_modified;
        private string _status;
		private int _inventoryID;
		public int workorderID {
			get { return _workorderID; }
			set { _workorderID = value; }
		}
        public DateTime? date_created
        {
            get { return _date_created; }
            set { _date_created = value; }
        }
        public DateTime? date_modified
        {
			get { return _date_modified; }
			set { _date_modified = value; }
		}
		public DateTime? date_completed {
			get { return _date_completed; }
			set { _date_completed = value; }
		}
		public string status {
			get { return _status; }
			set { _status = value; }
		}
		public int inventoryID {
			get { return _inventoryID; }
			set { _inventoryID = value; }
		}
        public List<BO.AssetInventoryTracking.workorder> GetAllworkorder()
        {
            return DAL.AssetInventoryTracking.workorder.Instance.GetAllworkorder();
        }

        public BO.AssetInventoryTracking.workorder GetByIDworkorder(int workorderID)
        {
            return DAL.AssetInventoryTracking.workorder.Instance.GetByIDworkorder(workorderID);
        }
        public int Save()
        {
            int workorderID = -1;
            if ((this.workorderID == -1))
            {
                workorderID = DAL.AssetInventoryTracking.workorder.Instance.Addworkorder(this);
            }
            else
            {
                workorderID = DAL.AssetInventoryTracking.workorder.Instance.Updateworkorder(this);
            }
            return workorderID;
        }

        public int Deleteworkorder(int workorderID)
        {
            return DAL.AssetInventoryTracking.workorder.Instance.Deleteworkorder(workorderID);
        }
    }
   

}

