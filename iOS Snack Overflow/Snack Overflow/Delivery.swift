//
//  Delivery.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class Delivery: NSObject {
    
    var Address:Address?
    var DeliverDate:Date?
    var DeliveryId:Int?
    var DeliveryTypeID:String?
    var OrderID:Int?
    var Packages = [Package]()
    var StatusId:String?
    var RouteId:Int?
    
}

func StringToDate(dateString:String?)-> Date
{
    var result = Date()
    let dateFormatter = DateFormatter()
    dateFormatter.dateFormat = "yyyy-MM-dd'T'hh:mm:ss"
    result = dateFormatter.date(from: dateString ?? "") ?? Date()
    return result
}

func cutTimeOffDate(dateString:String?) -> String{
    let indexToCutFrom = dateString?.index(dateString!.endIndex, offsetBy: -15)
    let incomingString = dateString?.substring(to: indexToCutFrom!)
    return incomingString!
}
