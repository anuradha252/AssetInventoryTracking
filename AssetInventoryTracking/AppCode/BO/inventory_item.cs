
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace BO.AssetInventoryTracking
{
	[Serializable()]
	public class inventory_item
	{
		public inventory_item()
		{
			_inventoryID = -1;
			_length_of_warranty = -1;
			_make = "";
			_model = "";
			_date_purchased = null;
			_percent_change_cost = -1;
			_status_of_item = "up";
            _name = "";
            _cost = 0;

		}
		private int _inventoryID;
		private int _length_of_warranty;
		private string _make;
        private string _name;
        private decimal _cost;
        private string _model;
		private DateTime? _date_purchased;
		private int _percent_change_cost;
		private string _status_of_item;
		public int inventoryID {
			get { return _inventoryID; }
			set { _inventoryID = value; }
		}
		public int length_of_warranty {
			get { return _length_of_warranty; }
			set { _length_of_warranty = value; }
		}
		public string make
        {
            get { return _make; }
            set { _make = value; }

        }
            public string name  
        {
            get { return _name; }
            set { _name = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public string model {
			get { return _model; }
			set { _model = value; }
		}
		public DateTime? date_purchased {
			get { return _date_purchased; }
			set { _date_purchased = value; }
		}
		public int percent_change_cost {
			get { return _percent_change_cost; }
			set { _percent_change_cost = value; }
		}
		public string status_of_item {
			get { return _status_of_item; }
			set { _status_of_item = value; }
		}
        public List<BO.AssetInventoryTracking.inventory_item> GetAllinventory_item()
        {
            return DAL.AssetInventoryTracking.inventory_item.Instance.GetAllinventory_item();
        }
        public List<BO.AssetInventoryTracking.inventory_item> Searchinventory_item(string name, string ID, string make, string model, string lengthwarranty, string cost, string status, string fromdate, string todate)
        {
            return DAL.AssetInventoryTracking.inventory_item.Instance.Searchinventory_item(name,ID,make,model,lengthwarranty, cost,status,fromdate,todate);
        }

        public BO.AssetInventoryTracking.inventory_item GetByIDinventory_item(int inventory_itemID)
        {
            return DAL.AssetInventoryTracking.inventory_item.Instance.GetByIDinventory_item(inventory_itemID);
        }
        public int Save()
        {
            int inventory_itemID = -1;
            if ((this.inventoryID == -1))
            {
                inventory_itemID = DAL.AssetInventoryTracking.inventory_item.Instance.Addinventory_item(this);
            }
            else
            {
                inventory_itemID = DAL.AssetInventoryTracking.inventory_item.Instance.Updateinventory_item(this);
            }
            return inventory_itemID;
        }

        public int Deleteinventory_item(int inventory_itemID)
        {
            return DAL.AssetInventoryTracking.inventory_item.Instance.Deleteinventory_item(inventory_itemID);
        }
    }
}

