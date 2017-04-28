//
//  Pickup.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

/// Eric Walton
/// 2017/04/23
/// Description: Pickup object
class Pickup: NSObject {
 
    var DriverId:Int?
    var EmployeeId:Int?
    var PickupId:Int?
    var PickupLineList = [PickupLine]()
    var SupplierId:Int?
    var WarehouseId:Int?
    var Address:Address?
    
}
