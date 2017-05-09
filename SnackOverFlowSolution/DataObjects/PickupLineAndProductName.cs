using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PickupLineAndProductName
    {
        PickupLine _pickupLine;
        public PickupLineAndProductName()
        {
            _pickupLine = new PickupLine();
        }

        public bool? PickupStatus {
            get {
                return _pickupLine.PickupStatus;
            }
            set {
                _pickupLine.PickupStatus = value;
            }
        }
        public int? Quantity
        {
            get
            {
                return _pickupLine.Quantity;
            }
            set
            {
                _pickupLine.Quantity = value;
            }
        }
        public int? ProductId
        {
            get
            {
                return _pickupLine.ProductId;
            }
            set
            {
                _pickupLine.ProductId = value;
            }
        }
        public int? PickupId
        {
            get
            {
                return _pickupLine.PickupId;
            }
            set
            {
                _pickupLine.PickupId = value;
            }
        }
        public int? PickupLineId
        {
            get
            {
                return _pickupLine.PickupLineId;
            }
            set
            {
                _pickupLine.PickupLineId = value;
            }
        }
        public string ProductName { get; set; }
    }
}
